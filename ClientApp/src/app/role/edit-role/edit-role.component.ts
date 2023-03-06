import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { __param } from 'tslib';
import { AspNetUser } from '../../interface/aspNetUser';
import { ExchangeBoxUser } from '../../interface/exchangeBoxUser';
import { RoleService } from '../../service/role/role.service';

@Component({
  selector: 'app-edit-role',
  templateUrl: './edit-role.component.html',
  styleUrls: ['./edit-role.component.css']
})
export class EditRoleComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private roleServices: RoleService)
  {

  }

  /*exchangeBoxItem: ExchangeBoxUser = {
    itemsFromAssignedUsers: [],
    itemsFromCandidateUsers: []
  }*/

  roleId: string = "";

  filterTextForAssignedEmployees: string = "";
  filterTextForCandidateEmployees: string = "";

  receiveFilterForAssignedEmployee($event: string) {
    this.filterTextForAssignedEmployees = $event;
  }
  receiveFilterForCandidateEmployee($event: string) {
    this.filterTextForCandidateEmployees = $event;
  }

  placeHolder: string = "Please type in email..."


  @Input() assignedUserList: AspNetUser[] = [];
  @Input() candidateUserList: AspNetUser[] = [];

  @Input() exchangeBoxItem: ExchangeBoxUser = {
    itemsFromAssignedUsers: [],
    itemsFromCandidateUsers: []
  }


  roleInfo: any = {}

  statusDesc: any = {
    status: 0,
    description: ''
  }

  ngOnInit(): void
  {
    this.route.paramMap.subscribe({
      next: (param) => {
        const roleId = param.get('id');
        if (roleId)
        {
          this.roleServices.getRole(roleId)
            .subscribe({
              next: (res) => {
                this.roleInfo = res;
                this.roleId = this.roleInfo[0].roleId
                this.exchangeBoxItem.itemsFromAssignedUsers = this.roleInfo[0].relateUsers
                this.GetAspNetUserList(this.exchangeBoxItem.itemsFromAssignedUsers);
              }
            })
        }
      }
    })
    

  }

  GetAspNetUserList(assignedUserList: AspNetUser[])
  {
    var filteredCandidateUserList: AspNetUser[] = [];
    var assignedUserList: AspNetUser[] = assignedUserList;
    this.roleServices.getAspUserList().subscribe({
      next: (res:any ) =>
      {
        const responseBody = res;
        filteredCandidateUserList = responseBody.response;

        if (assignedUserList.length > 0)
        {
          for (let i = 0; i < assignedUserList.length; i++)
          {
            for (let j = 0; j < filteredCandidateUserList.length; j++) {
              if (assignedUserList[i].id === filteredCandidateUserList[j].id) {
                filteredCandidateUserList.splice(j, 1)
              }
            }
          }
        }
        this.exchangeBoxItem.itemsFromAssignedUsers = assignedUserList;
        this.exchangeBoxItem.itemsFromCandidateUsers = filteredCandidateUserList;
        console.log("assigned: ", this.assignedUserList)
        console.log("candidate: ", this.candidateUserList)
      }
    })
  }

  assignUsersToRole(): void
  {
    const selectedItems: string[] = this.exchangeBoxItem.itemsFromCandidateUsers.filter((item: { selected: boolean; }) => item.selected).map((item: { id: string; }) => item.id);
    if (selectedItems.length > 0)
    {
      if (confirm('Are you sure you want to assign the selected employees to this role?'))
      {
        this.roleServices.assignUsersToRole(this.roleId, selectedItems).subscribe({
          next: (res: any) =>
          {
            console.log(res)
            if (res.status == 200)
            {
              this.exchangeBoxItem.itemsFromAssignedUsers = this.exchangeBoxItem.itemsFromAssignedUsers.concat(this.exchangeBoxItem.itemsFromCandidateUsers.filter((item: { selected: any; }) => item.selected));
              this.exchangeBoxItem.itemsFromCandidateUsers = this.exchangeBoxItem.itemsFromCandidateUsers.filter((item: { selected: boolean | undefined; }) => !item.selected);

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
    console.log("I am called from assign user")
  }

  removeUsersFromRole(): void
  {
    const selectedItems: string[] = this.exchangeBoxItem.itemsFromAssignedUsers.filter((item: { selected: boolean; }) => item.selected).map((item: { id: string; }) => item.id);
    if (selectedItems.length > 0)
    {
      if (confirm('Are you sure you want to remove the selected employees from this role?'))
      {
        this.roleServices.removeUsersFromRole(this.roleId, selectedItems).subscribe({
          next: (res:any) =>
          {
            console.log(res);
            if (res.status == 200)
            {
              this.exchangeBoxItem.itemsFromCandidateUsers = this.exchangeBoxItem.itemsFromCandidateUsers.concat(this.exchangeBoxItem.itemsFromAssignedUsers.filter((item: { selected: any; }) => item.selected));
              this.exchangeBoxItem.itemsFromAssignedUsers = this.exchangeBoxItem.itemsFromAssignedUsers.filter((item: { selected: boolean | undefined; }) => !item.selected);

            }
          }
        })
      }
    }
    console.log("I am called from remove user")
  }


}
