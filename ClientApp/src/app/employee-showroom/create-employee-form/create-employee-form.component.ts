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
    accountFk: 0,
    code: '',
    isContractor: false,
    contractorTypeFk: '',
    defaultShowroomFk: 0,
    maxLeadCount: 0,
    monthlyBudget: 0,
  }
}
