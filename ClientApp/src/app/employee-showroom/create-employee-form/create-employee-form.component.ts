import { Component } from '@angular/core';
import { CreateEmployee } from '../../interface/createEmployee';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-employee-form',
  templateUrl: './create-employee-form.component.html',
  styleUrls: ['./create-employee-form.component.css']
})
export class CreateEmployeeFormComponent {
  constructor(private router: Router) { }

  createEmployee() {

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
    samaccountname: '',
    password: '',
    passwordsalt: '',
    status: '',
  }
}
