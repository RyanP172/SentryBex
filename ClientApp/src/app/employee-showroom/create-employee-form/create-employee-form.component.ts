import { Component } from '@angular/core';
import { CreateEmployee } from '../../interface/createEmployee';

@Component({
  selector: 'app-create-employee-form',
  templateUrl: './create-employee-form.component.html',
  styleUrls: ['./create-employee-form.component.css']
})
export class CreateEmployeeFormComponent {
  createEmployee() {

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
