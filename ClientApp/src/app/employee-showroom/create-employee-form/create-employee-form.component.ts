import { Component, OnInit } from '@angular/core';
import { CreateEmployee } from '../../interface/createEmployee';
import { Router } from '@angular/router';
import { FormGroup,FormBuilder, FormControl, Validators, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
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
    //const employee: CreateEmployee = {
    //  firstName: this.employeeForm.value.firstName,
    //  middleName: this.employeeForm.value.email,
    //  lastName: this.employeeForm.value.phone,
    //  dob: this.employeeForm.value.skills
    //};

    //this.employeeShowroomService.createEmployee(this.employee)

    console.log("employee info", this.employee)
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
    samAccount: '',
    password: '',
    passwordSalt: '',
    status: '',
  }
}
