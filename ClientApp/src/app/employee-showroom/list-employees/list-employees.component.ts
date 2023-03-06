import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Inject, Input } from '@angular/core';
import { Observable, subscribeOn } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeShowroomService } from '../../service/employee-showroom/employee-showroom.service';
import { faMagnifyingGlass, faUserPen, faUserSlash, faXmarkCircle, faCheckCircle } from '@fortawesome/free-solid-svg-icons';
import { Employee } from '../../interface/employee';

@Component({
  selector: 'app-employee-showroom',
  templateUrl: './list-employees.component.html',
  styleUrls: ['./list-employees.component.css']
})
export class EmployeeShowroomComponent implements OnInit {

  @Input() employees: Employee[] = [];

  @Input() pageSize: number = 15;

  currentPage = 1;

  get totalPages() {
    return Math.ceil(this.employees.length / this.pageSize);
  }

  get pages() {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  changePage(newPage: number) {
    this.currentPage = newPage;
  }

  pageBack()
  {
    if (this.currentPage > 1)
      
      this.currentPage = this.currentPage - 1;
    console.log(this.currentPage)
  }

  pageTop()
  {
    if (this.currentPage != 1)

      this.currentPage = 1;
    console.log(this.currentPage)
  }

  pageEnd()
  {
    if (this.currentPage != this.totalPages)

      this.currentPage = this.totalPages;
    console.log(this.currentPage)
  }

  pageForward() {
    if (this.currentPage < this.totalPages - 1)
      console.log(this.currentPage)
      this.currentPage = this.currentPage + 1;
  }
  get pagedEmployees() {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.employees.slice(startIndex, endIndex);
  }


  filterText: string = '';
  sortField = 'name';
  sortDirection = 'asc';

  faUserPen = faUserPen;
  faUserSlahsh = faUserSlash;
  faXmark = faXmarkCircle;
  faCheck = faCheckCircle;
  glass = faMagnifyingGlass;
  
  statusDesc: any = {
    status: 0,
    description: ''
  }

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


   

  constructor(
    private employeeShowRoomService: EmployeeShowroomService,
    private route: ActivatedRoute,
    private router: Router,
  )
  {

  }

  ngOnInit(): void {
    this.GetEmployees();
  }

  GetEmployees(): void
  {
    this.employeeShowRoomService.getEmployees().subscribe(
      (res) => {
        /*res.forEach((data: Employee) =>
        {
          if (data.account.status == 'A')
          {
            this.employees.push(data);
          }
        })*/
        this.employees = res;
       console.log(this.employees)
      }
,
      (error: any) => console.log(error),
      () => console.log('List employees done'),
    );
  }

  changeEpeEmployeeStatus(id: number, status: any): void {
    if (status == 'I') {
      if (confirm('Are you sure to disable this employee?')) {
        console.log("id", id)
        console.log("status", status)
        this.employeeShowRoomService.disableEpeEmployee(id, status).subscribe({
          next: (res: any) => {
            const status = JSON.parse(res)

            if (status.status == 200) {
              /*this.employees = this.employees.filter(employee => employee.id != id)*/
              const employeeIndex = this.employees.findIndex(emp => emp.id == id)
              if (employeeIndex != -1)
              {
                this.employees[employeeIndex].account.status = (this.employees[employeeIndex].account.status = "I");
              }
              alert("Employee has been disabled")
            }
            else {
              alert("Failed to change employee status")
            }
          },
          error: msg => {
            alert("Failed to change employee status")
            this.statusDesc.status = msg.error.status;
            this.statusDesc.description = msg.error.title + " Please make sure your input data is in right format.";
            console.log(msg)
          }
        });
      }
    }
    else if (status == 'A')
    {
      if (confirm('Re-activate this employee?')) {
        console.log("id", id)
        console.log("status", status)
        this.employeeShowRoomService.disableEpeEmployee(id, status).subscribe({
          next: (res: any) => {
            const status = JSON.parse(res)

            if (status.status == 200) {
              /*this.employees = this.employees.filter(employee => employee.id != id)*/
              const employeeIndex = this.employees.findIndex(emp => emp.id == id)
              if (employeeIndex != -1) {
                this.employees[employeeIndex].account.status = (this.employees[employeeIndex].account.status = "A");
              }
              alert("Employee has been re-activated")
            }
            else {
              alert("Failed to change employee status")
            }
          },
          error: msg => {
            alert("Failed to change employee status")
            this.statusDesc.status = msg.error.status;
            this.statusDesc.description = msg.error.title + " Please make sure your input data is in right format.";
            console.log(msg)
          }
        });
      }
    }
    
    
  }
  
}
