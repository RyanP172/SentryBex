import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginBody } from '../../interface/loginbody';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class AuthorisationService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient)
  {
  }

  userLogin(loginBody: LoginBody): Observable<LoginBody> {
    return this.http.post<LoginBody>(`${this.apiUrl}/auth/login`, loginBody);
  }

  logout() {
    localStorage.removeItem("token");
  }

}
