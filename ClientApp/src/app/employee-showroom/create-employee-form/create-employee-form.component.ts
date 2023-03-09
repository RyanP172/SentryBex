import { Component, OnInit, ViewChild } from '@angular/core';
import { CreateEmployee } from '../../interface/createEmployee';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Validators, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { EmployeeShowroomService } from '../../service/employee-showroom/employee-showroom.service';
//import { isEmailExist } from '../../custom-validator/check-email-exist';
@Component({
  selector: 'app-create-employee-form',
  templateUrl: './create-employee-form.component.html',
  styleUrls: ['./create-employee-form.component.css']
})
export class CreateEmployeeFormComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;

  statusDesc: any = {
    status: 0,
    description: ''
  }
  constructor(
    private router: Router,
    private employeeShowroomService: EmployeeShowroomService,
    private fb: FormBuilder,
  ) {
    //this.registerForm = new FormGroup({});
    this.registerForm = this.fb.group({});
  }
  // This way use FormGroup
  /*  ngOnInit(): void {
      this.registerForm = new FormGroup({
        'firstname': new FormControl(null, [Validators.required, Validators.maxLength(80), Validators.pattern('^[a-zA-Z_]+( [a-zA-Z_]+)*$')]),
        'email': new FormControl(null, [Validators.required, Validators.email]),
        'midlename': new FormControl(null, [Validators.maxLength(80), Validators.pattern('^[a-zA-Z_]+( [a-zA-Z_]+)*$')]),
        'lastname': new FormControl(null, [Validators.required, Validators.maxLength(80), Validators.pattern('^[a-zA-Z_]+( [a-zA-Z_]+)*$')]),
        'samaccount': new FormControl(null, Validators.required),
        'dob': new FormControl(null, Validators.required),
        'contractortype': new FormControl(null),
        'code': new FormControl(null),
        'iscontractor': new FormControl(true),
        'showroom': new FormControl(1),
        'companyid': new FormControl(3),
        'maxleadcount': new FormControl(5),
        'monthlybudget': new FormControl(5000),
        'status': new FormControl('A')
  
      });
    }*/


  // This way use FormBuider
  ngOnInit() {
    this.registerForm = this.fb.group({
      firstname: ['', [Validators.required, Validators.maxLength(20), Validators.pattern('^[-a-zA-Z-()]+(\s+[-a-zA-Z-()]+)*$')]],
      email: ['', [Validators.maxLength(80) ,Validators.required, Validators.email]],      
      middlename: ['', [Validators.maxLength(20), Validators.pattern('^[-a-zA-Z-()]+(\s+[-a-zA-Z-()]+)*$')]],
      lastname: ['', [Validators.required, Validators.maxLength(20), Validators.pattern('^[-a-zA-Z-()]+(\s+[-a-zA-Z-()]+)*$')]],
      samaccount: ['', [Validators.required, Validators.maxLength(40), Validators.pattern('^[-a-zA-Z.@!#$%&0-9-()]+(\s+[-a-zA-Z.@!#$%&0-9-()]+)*$')]],
      //dob: ['', [Validators.required, Validators.pattern('^[0-9]{2}[\/][0-9]{2}[\/][0-9]{4}*$')]],
      dob: ['', Validators.required],
      //contractortype: [''],
      code: ['', [Validators.maxLength(10), Validators.pattern('^[-a-zA-Z.@!#$%&0-9-()]+(\s+[-a-zA-Z.@!#$%&0-9-()]+)*$')]],
      iscontractor: [''],
      showroom: [],
      companyid: [],
      maxleadcount: [],
      monthlybudget: [],
      status: ['A'],
    })

  }
  get f() { return this.registerForm.controls; };
  createEmployeeOnSubmit() {
    console.log(this.registerForm);

    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      alert('Form is not valid. Please correct infomation')
      return;
    }
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
          if (msg.error.title == "One or more validation errors occurred.") {
            this.statusDesc.description = msg.error.title + " There is an error while creating a new employee";
          }
          else { this.statusDesc.description = msg.error.message + ". There is an error while creating a new employee"; }
          alert(this.statusDesc.description);
          console.log(this.statusDesc)
        }
      });
    }

  }
  onCancel() {
    this.submitted = false;
    this.router.navigate(['/fetch-employee-showroom/employees/']);

  }
  onClear() {
    this.submitted = false;
    this.registerForm.reset();
  }

  getEmail() {
    console.log(this.registerForm.controls['email']);
    return this.registerForm.controls['email'];
  }
  checkEmailValid(email: any) {
  }

  employee: CreateEmployee = {
    firstName: '',
    middleName: '',
    lastName: '',
    dob: '',
    code: '',
    isContractor: true,
    //contractorTypeId: '',
    defaultShowroomFk: 12,
    maxLeadCount: 5,
    monthlyBudget: 5000,
    companyId: 3,
    email: '',
    samAccountName: '',
    //password: '',
    //passwordSalt: '',
    status: '',
  }
}
