import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginBody } from '../interface/loginbody';
import { AuthorisationService } from '../service/auth/authorisation.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  myForm!: FormGroup;
  loading = false;
  submitted = false;

  loginBody: LoginBody = {
      email: '',
      password: ''
  }
  statusDesc: any = {
    status: 0,
    description: ''
  }

  validLogin: boolean = true;
  validEmail: boolean = true;
  validPassword: boolean = true;
  message:string = ""

  constructor(private authService: AuthorisationService, private fb: FormBuilder) {

  }

  ngOnInit()
  {
    this.myForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$')]],
    });
  }

  validateEmail(email: string): boolean {
    // Email validation regex
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    return emailRegex.test(email);
  }

  onSubmit()
  {
    if (this.validateEmail(this.loginBody.email)) {
      if (this.loginBody.password !== '') {
        this.validEmail = true;
        this.loading = true;
        this.authService.userLogin(this.loginBody).subscribe({
          next: (res: any) => {
            console.log(res)
            localStorage.setItem('token', res.token)
            this.statusDesc.status = res.status;
            this.statusDesc.description = res.description
            this.message = res.description
            console.log(this.statusDesc)
            this.validLogin = true;
            this.validPassword = true;
            this.loading = false;
          },
          error: msg => {
            this.loading = false;
            this.validLogin = false;
            this.validPassword = true;
            console.log(msg)
            this.statusDesc.status = msg.error.status;
            this.statusDesc.description = msg.error.description
            this.message = msg.error.description
            console.log(this.statusDesc)
          }
        })
      } else {

        //reset status
        this.validLogin= true;
        
        this.validPassword = false;
        this.message = "Password cannot be empty"
      }
    } else
    {
      this.validPassword = true;
      this.validEmail = false;
      this.message = "Your email format is not correct."
    }


  }
}
