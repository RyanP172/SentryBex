import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { catchError, Observable, of, throwError } from 'rxjs';
import { Employee } from '../../interface/employee';
import { ShowRoom } from '../../interface/show-room';
import { environment } from '../../../environments/environment';
import { CreateEmployee } from '../../interface/createEmployee';



@Injectable({
  providedIn: 'root'
})
export class EmployeeShowroomService {

  /*apiUrl: 'https://localhost:7042'*/
  private apiUrl = environment.apiUrl;


  constructor(private http: HttpClient) {  }

  validateEmail(email: string) {
    return this.http.get<boolean>(`${this.apiUrl}/api/account`+{email})
  }

  createEmployee(createEmployee: CreateEmployee): Observable<CreateEmployee> {
    const httpOptions: Object = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: 'json'
    };    
    return this.http.post<CreateEmployee>(`${this.apiUrl}/api/employee`, createEmployee, httpOptions);
  }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.apiUrl}/api/employee`);
  }
  getEmployee(id: string): Observable<Employee> {
    return this.http.get<Employee>(`${this.apiUrl}/api/employee/` + id)
  }

  updateEmployee(id: number, updatedEmployee: Employee): Observable<Employee> {
    return this.http.patch<Employee>(`${this.apiUrl}/api/employee/` + id, updatedEmployee);

  }

  updateShowRoom(id: number, updatedShowRoom: ShowRoom): Observable<ShowRoom> {
    console.log(updatedShowRoom)
    return this.http.patch<ShowRoom>(`${this.apiUrl}/api/showroom/` + id, updatedShowRoom);
  }

  getShowRooms(): Observable<ShowRoom[]> {
    return this.http.get<ShowRoom[]>(`${this.apiUrl}/api/showroom`);
  }

  getShowRoom(id: number): Observable<ShowRoom> {
    return this.http.get<ShowRoom>(`${this.apiUrl}/api/showroom/` + id)
  }

  assignShowRooms(id: number, ids: number[]): Observable<Employee>
  {
    return this.http.post<Employee>(`${this.apiUrl}/api/employee/` + id, ids);
  }
  removeShowRooms(id: number, ids: number[]): Observable<Employee>
  {
    return this.http.delete<Employee>(`${this.apiUrl}/api/employee/` + id, { body: ids })
      .pipe(
        catchError((error: HttpErrorResponse): Observable<any> => {
          if (error.status === 200) {
            // Handle the error here
            return of({
              status: 200,
              description: "Selected rooms have been removed"
            });
          }
          else if (error.status === 400) {

            return of({
              status: 400,
              description: "Employees should have at least one show room. "
            });
          }
          else {
            // Throw a custom error message
            return throwError(`HTTP Error: ${error.status} - ${error.message}`);
          }
        })
      )
  }
  disableEpeEmployee(id: number, status:any): Observable<Employee>
  {
    const httpOptions: Object = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: 'text'
  };

    return this.http.post<Employee>(`${this.apiUrl}/api/employee/${id}` + '/status', JSON.stringify(status), httpOptions );
  }
}
