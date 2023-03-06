import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AspNetUser } from '../../interface/aspNetUser';
import { Permission } from '../../interface/permission';


@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private apiUrl = environment.apiUrl;


  constructor(private http: HttpClient)
  {

  }

  getRoles(): Observable<Permission[]>
  {
    return this.http.get<Permission[]>(`${this.apiUrl}/api/role`);
  }

  getRole(id: string): Observable<Permission> {
    return this.http.get<Permission>(`${this.apiUrl}/api/role/` + id)
  }

  assignRoles(userId: string, roleIds: string[]): Observable<Permission>
  {
    return this.http.post<Permission>(`${this.apiUrl}/api/role/aspuser/` + userId +'/aspuserrole', roleIds);
  }
  removeRoles(userId: string, roleIds: string[]): Observable<Permission> {
    return this.http.delete<Permission>(`${this.apiUrl}/api/role/aspuser/` + userId + '/aspuserrole', { body: roleIds })
      .pipe(
        catchError((error: HttpErrorResponse): Observable<any> => {
          if (error.status === 200) {
            // Handle the error here
            return of({
              status: 200,
              description: "Selected roles has been removed"
            }); 
          }
          else if(error.status === 400) {
            
            return of({
              status: 400,
              description: "Employee should have at least one role. It also maybe the roles are not exist or the roles are not belong to this user"
            }); 
          }
          else {
            // Throw a custom error message
            return throwError(`HTTP Error: ${error.status} - ${error.message}`);
          }
        })
      )
  }

  addNewRole(roleName: string): Observable<Permission>
  {
    const httpOptions: Object = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: 'text'
    };
    return this.http.post<Permission>(`${this.apiUrl}/api/role`, JSON.stringify(roleName), httpOptions);
  }

  changeRoleName(roleId: string, roleName: string): Observable<Permission>
  {
    const httpOptions: Object = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: 'application/json'
    };
    return this.http.patch<Permission>(`${this.apiUrl}/api/role/` + roleId, JSON.stringify(roleName), httpOptions);
  }

  getAspUserList(): Observable<AspNetUser[]>
  {
    return this.http.get<AspNetUser[]>(`${this.apiUrl}/api/role/aspuser`); 
  }

  assignUsersToRole(roleId: string, userIds: string[]): Observable<AspNetUser[]>
  {
    return this.http.post<AspNetUser[]>(`${this.apiUrl}/api/role/${roleId}/aspuser`, userIds);
  }
  removeUsersFromRole(roleId: string, userIds: string[]): Observable<AspNetUser[]>
  {
    return this.http.delete<AspNetUser[]>(`${this.apiUrl}/api/role/${roleId}/aspuser`, { body: userIds });
  }

}
