import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, filter, Observable, throwError } from 'rxjs';
import { Employee } from '../../interface/employee';
import { ExchangeBoxRoleRoom } from '../../interface/exchangeBox';
import { Permission } from '../../interface/permission';
import { ShowRoom } from '../../interface/show-room';
import { EmployeeShowroomService } from '../../service/employee-showroom/employee-showroom.service';
import { RoleService } from '../../service/role/role.service';
enum tabPage {
  Employee = "Employee View/Edit",
  ShowRoom = "Show Room Management",
  Permission = "Permission Manage"
}


@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

  exchangeBoxItem: ExchangeBoxRoleRoom = {
    itemsFromCandidateRooms: [],
    itemsFromAssignedRooms: [],
    itemsFromAssignedRoles: [],
    itemsFromCandidateRoles: []
  };

  //Initial value
  employee: Employee = {
    id: 0,
    firstName: '',
    middleName: undefined,
    lastName: '',
    dob: '',
    created: '',
    modified: '',
    accountFk: 0,
    code: '',
    isContractor: false,
    contractorTypeFk: '',
    defaultShowroomFk: 0,
    maxLeadCount: 0,
    monthlyBudget: 0,
    showRooms: [],
    permissions: [],
    netUser: {
      "id": "",
      "userName": ""
    },
    account: {
      "id": 0,
      "userName": "",
      "samAccountName": "",
      "password": null,
      "passwordSalt": null,
      "created": "",
      "resetPwdGuid": "",
      "resetPwdDatetime": null,
      "modified": "",
      "status": ""
    }
  };

  userUUid: string = "";

  menuList: any = [
    {
      id: 1,
      title: tabPage.Employee,
      checked: true
    },
    {
      id: 2,
      title: tabPage.ShowRoom,
      checked: false
    },
    {
      id: 3,
      title: tabPage.Permission,
      checked: false
    }
  ];

  selectedList: number = 1;

  statusDesc: any = {
    status: 0,
    description: ''
  }
  roles: Permission[] = [];
  showRooms: ShowRoom[] = [];
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private employeeShowRoomService: EmployeeShowroomService,
    private roleService: RoleService
  ) {

  }

  //Initial value ends


  //page initialisation when it's loaded
  ngOnInit(): void {
    this.selectedList = this.selectedList;


    this.route.paramMap.subscribe({
      next: (param) => {
        const employeeId = param.get('id');
        if (employeeId) {
          //call api for single record
          this.employeeShowRoomService.getEmployee(employeeId)
            .subscribe({
              next: (res) => {
                this.employee = res;
                this.exchangeBoxItem.itemsFromAssignedRoles = res.permissions;
                this.exchangeBoxItem.itemsFromAssignedRooms = res.showRooms;
                this.GetShowRooms(this.exchangeBoxItem.itemsFromAssignedRooms);
                this.GetRolesList(this.exchangeBoxItem.itemsFromAssignedRoles)
                this.userUUid = res.netUser.id
                console.log(this.employee)
                
              },

            });
        }
      }
    })
  }

  openMenuList(menuList: number) {
    this.selectedList = menuList;
  }

  GetRolesList(itemsFromAssignedRoles: Permission[]): void
  {
    var filteredCandidateRole: Permission[] = [];
    var signedRole: Permission[] = itemsFromAssignedRoles;
    this.roleService.getRoles().subscribe({
      next: (res: Permission[]) => {
        filteredCandidateRole = res
        if (signedRole.length > 0) {
          for (let i = 0; i < signedRole.length; i++)
          {
            for (let j = 0; j < filteredCandidateRole.length; j++)
            {
              if (signedRole[i].id === filteredCandidateRole[j].id)
              {
                filteredCandidateRole.splice(j, 1)
              }
            }
          }
        }
        this.exchangeBoxItem.itemsFromAssignedRoles = itemsFromAssignedRoles
        this.exchangeBoxItem.itemsFromCandidateRoles = filteredCandidateRole
      }
    })
  }

  GetShowRooms(itemsFromAssignedRooms: ShowRoom[]): void {
    var filteredCandidateRoom: ShowRoom[] = [];
    var signedRoom: ShowRoom[] = itemsFromAssignedRooms;
    this.employeeShowRoomService.getShowRooms().subscribe(
      (res) => {
        this.showRooms = res;
        filteredCandidateRoom = res;
        if (signedRoom.length > 0) {
          for (let i = 0; i < signedRoom.length; i++) {
            for (let j = 0; j < filteredCandidateRoom.length; j++) {
              if (signedRoom[i].id === filteredCandidateRoom[j].id) {
                filteredCandidateRoom.splice(j, 1);

              }
            }
          }
        }
        this.exchangeBoxItem.itemsFromAssignedRooms = itemsFromAssignedRooms
        this.exchangeBoxItem.itemsFromCandidateRooms = filteredCandidateRoom
        console.log("signed: ", signedRoom);
        
      },

      (error: any) => console.log(error),
      () => console.log('show rooms list for employee')
    );
  }

  updateEmployee() {
    if (confirm('Update information confirm?'))
    {
      console.log("employee info", this.employee)
      this.employeeShowRoomService.updateEmployee(this.employee.id, this.employee).subscribe({

        next: (res: any) => {
          
          if (this.employee.id == res.updateEmployee.id) {
            this.statusDesc.status = res.status;
            this.statusDesc.description = res.description
            /*setTimeout(() => {
              this.router.navigate(['fetch-employee-showroom/employees']);
            }, 2000);*/
          }
        },
        error: msg => {
          console.log(msg)
          this.statusDesc.status = msg.error.status;
          this.statusDesc.description = msg.error.title + " Please make sure the user already login for first time";
          console.log(this.statusDesc)
        }
      })
    }
  }

  assignRolesToUserSubmit(): void
  {
    
    const selectedItem: string[] = this.exchangeBoxItem.itemsFromCandidateRoles.filter((item: { selected: boolean; }) => item.selected).map((item: { id: string; }) => item.id);
    
    if (selectedItem.length > 0)
    {
      if (confirm('Are you sure you want to assign the selected roles to this user?'))
      {
        this.roleService.assignRoles(this.userUUid, selectedItem).subscribe({
          next: (res: any) =>
          {
            console.log(res)
            if (res.status == 200)
            {
              this.exchangeBoxItem.itemsFromAssignedRoles = this.exchangeBoxItem.itemsFromAssignedRoles.concat(this.exchangeBoxItem.itemsFromCandidateRoles.filter((item: { selected: any; }) => item.selected));
              this.exchangeBoxItem.itemsFromCandidateRoles = this.exchangeBoxItem.itemsFromCandidateRoles.filter((item: { selected: boolean | undefined; }) => !item.selected);
            }
          },
          error: msg => {
            this.statusDesc.status = msg.error.status;
            this.statusDesc.description = msg.error.title + " Please make sure your input data is in right format.";
            console.log(this.statusDesc)
          }
        })
      }
    }
  }

  removeRolesFromUserSubmit(): void {
    const selectedItem: string[] = this.exchangeBoxItem.itemsFromAssignedRoles.filter((item: { selected: boolean; }) => item.selected).map((item: { id: string; }) => item.id);

    if (selectedItem.length > 0) {
      if (confirm('Are you sure you want to remove the selected rooms from this user?')) {
        this.roleService.removeRoles(this.userUUid, selectedItem)
          .subscribe({
          next: (res: any) => {
            console.log("test",res)
              if (res.statusCode == 200) {
              this.exchangeBoxItem.itemsFromCandidateRoles = this.exchangeBoxItem.itemsFromCandidateRoles.concat(this.exchangeBoxItem.itemsFromAssignedRoles.filter((item: { selected: any; }) => item.selected));
              this.exchangeBoxItem.itemsFromAssignedRoles = this.exchangeBoxItem.itemsFromAssignedRoles.filter((item: { selected: boolean | undefined; }) => !item.selected);
              }
              if (res.status == 400)
              {
                alert(res.description)
              }
          },
          error: msg => {
            console.log(msg)
            if(msg)
            console.log(this.statusDesc)
          }
        })
      }
    }
  }

  assignRoomsToUserSubmit(): void {

    const selectedItems: number[] = this.exchangeBoxItem.itemsFromCandidateRooms.filter((item: { selected: boolean; }) => item.selected).map((item: { id: number; }) => item.id);
    if (selectedItems.length > 0) {
      if (confirm('Are you sure you want to assign the selected rooms to this user?')) {
        this.exchangeBoxItem.itemsFromAssignedRooms = this.exchangeBoxItem.itemsFromAssignedRooms.concat(this.exchangeBoxItem.itemsFromCandidateRooms.filter((item: { selected: any; }) => item.selected));
        this.exchangeBoxItem.itemsFromCandidateRooms = this.exchangeBoxItem.itemsFromCandidateRooms.filter((item: { selected: boolean | undefined; }) => !item.selected);
        this.employeeShowRoomService.assignShowRooms(this.employee.id, selectedItems).subscribe({
          next: (res:any )=> {
            /*console.log("response", res)*/
            if (res.success) {
              //TODO: for response
            }

          },
          error: msg => {
            this.statusDesc.status = msg.error.status;
            this.statusDesc.description = msg.error.title + " Please make sure your input data is in right format.";
            console.log(this.statusDesc)
          }
        })
        console.log(selectedItems);
        console.log("works")
      }

    }

  }

  removeRoomsFromUserSubmit(): void {

    const selectedItems: number[] = this.exchangeBoxItem.itemsFromAssignedRooms.filter((item: { selected: boolean; }) => item.selected).map((item: { id: number; }) => item.id);
    if (selectedItems.length > 0)
    {
      if (confirm('Are you sure you want to remove the selected rooms from this user?'))
      {
        
        this.employeeShowRoomService.removeShowRooms(this.employee.id, selectedItems).subscribe({

          next: (res: any) => {
            console.log("delete", res)
            if (res.statusCode == 200) {
              //TODO: for response
              this.exchangeBoxItem.itemsFromCandidateRooms = this.exchangeBoxItem.itemsFromCandidateRooms.concat(this.exchangeBoxItem.itemsFromAssignedRooms.filter((item: { selected: any; }) => item.selected));
              this.exchangeBoxItem.itemsFromAssignedRooms = this.exchangeBoxItem.itemsFromAssignedRooms.filter((item: { selected: boolean | undefined; }) => !item.selected);
            }
            if (res.status == 400)
            {
              alert(res.description)
            }
          },
          error: msg => {
            console.log(msg)
            alert(msg.error.message)
            console.log(this.statusDesc)
          }
        })
        console.log("works")
      }

    }

  }
  

}
