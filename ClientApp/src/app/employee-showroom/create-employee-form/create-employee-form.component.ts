import { Component, OnInit } from '@angular/core';
import { CreateEmployee } from '../../interface/createEmployee';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { EmployeeShowroomService } from '../../service/employee-showroom/employee-showroom.service';

@Component({
  selector: 'app-create-employee-form',
  templateUrl: './create-employee-form.component.html',
  styleUrls: ['./create-employee-form.component.css']
})
export class CreateEmployeeFormComponent implements OnInit {

  //emailControl = new FormControl('', {
  //  validators: [Validators.required, Validators.email],
  //  asyncValidators: [this.emailExistsValidator()],
  //  updateOn: 'blur'
  //});
  statusDesc: any = {
    status: 0,
    description: ''
  }
  constructor(
    private router: Router,
    private employeeShowroomService: EmployeeShowroomService,
    private formBuilder: FormBuilder,

  ) { }
  ngOnInit(): void {
    console.log(this.employee);
  }


  //emailExistsValidator(): AsyncValidatorFn {

  //}

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
          this.statusDesc.status = msg.error.status;
          this.statusDesc.description = msg.error.message  + ". There is an error while creating a new employee";
          alert(msg.error.message);
          console.log(this.statusDesc)
        }
      });
    }

  }

  onCancel() {
    this.router.navigate(['/fetch-employee-showroom/employees/']);
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
    password: '',
    passwordSalt: '',
    status: '',
  }


}
