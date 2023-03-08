import { Component, OnInit, ViewChild } from '@angular/core';
import { CreateEmployee } from '../../interface/createEmployee';
import { Router } from '@angular/router';
import { NgForm, FormGroup, FormBuilder, FormControl    } from '@angular/forms';
import { Validators, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { EmployeeShowroomService } from '../../service/employee-showroom/employee-showroom.service';

@Component({
  selector: 'app-create-employee-form',
  templateUrl: './create-employee-form.component.html',
  styleUrls: ['./create-employee-form.component.css']
})
export class CreateEmployeeFormComponent implements OnInit {
 

  statusDesc: any = {
    status: 0,
    description: ''
  }
  constructor(
    private router: Router,
    private employeeShowroomService: EmployeeShowroomService,
    private fb: FormBuilder,  
    

  ) { }

  ngOnInit(): void {

  }



  createEmployeeOnSubmit() {

    if (confirm('Please confirm the information again')) {
      console.log("employee info", this.employee);
      this.employeeShowroomService.createEmployee(this.employee).subscribe({
        next: (response) => {
          console.log('Employee created successfully:', response);
          alert('Employee created successfully')
          this.router.navigate(['/fetch-employee-showroom/employees/']);

        },
        error: msg => {
          console.log(msg.error);
          debugger;
          this.statusDesc.status = msg.error.status;
          if (msg.error.title == "One or more validation errors occurred.") {
            this.statusDesc.description = msg.error.title + " There is an error while creating a new employee";          }
          else { this.statusDesc.description = msg.error.message + ". There is an error while creating a new employee"; }
          alert(this.statusDesc.description);
          
          console.log(this.statusDesc)
        }
      });
    }

  }

  onCancel() {
    this.router.navigate(['/fetch-employee-showroom/employees/']);
  }

 

  checkEmailValid(email: any) {
 
  }

  checkEmail(email: any) {

  }

  employee: CreateEmployee = {

    firstName: '',
    middleName: '',
    lastName: '',
    dob: '',
    code: '',
    isContractor: true,
    contractorTypeId: '',
    defaultShowroomFk: 12,
    maxLeadCount: 5,
    monthlyBudget: 5000,
    companyId: 3,
    email: '',
    samAccountName: '',
    //password: '',
    //passwordSalt: '',
    status: 'A',
  }


}
