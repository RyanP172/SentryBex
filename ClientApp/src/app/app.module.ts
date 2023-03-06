import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ReactiveFormsModule } from '@angular/forms';
import { EmployeeShowroomComponent } from './employee-showroom/list-employees/list-employees.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { EditEmployeeComponent } from './employee-showroom/edit-employee/edit-employee.component';
import { ListShowroomsComponent } from './employee-showroom/list-showrooms/list-showrooms.component'
import { EditShowroomsComponent } from './employee-showroom/edit-showrooms/edit-showrooms.component';
import { ListRolesComponent } from './role/list-roles/list-roles.component';
import { EditRoleComponent } from './role/edit-role/edit-role.component';
import { FilterPipeEmployee } from './employee-showroom/list-employees/name-filter.pipe';
import { FilterPipeShowRoom } from './employee-showroom/list-showrooms/filter.pipe';
import { OrderByTablePipe } from './employee-showroom/list-employees/table-orderby.pipe';
import { InputFilterComponent } from './components/input-filter/input-filter.component';
import { FilterPipeAspUser } from './role/edit-role/email-filter';
import { ExchangeBoxComponent } from './components/exchange-box/exchange-box.component';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    EmployeeShowroomComponent,
    EditEmployeeComponent,
    ListShowroomsComponent,
    EditShowroomsComponent,
    ListRolesComponent,
    EditRoleComponent,
    FilterPipeEmployee,
    FilterPipeShowRoom,
    FilterPipeAspUser,
    OrderByTablePipe,
    InputFilterComponent,
    ExchangeBoxComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent },
      { path: 'fetch-employee-showroom/employees', component: EmployeeShowroomComponent },
      { path: 'fetch-employee-showroom/showrooms', component: ListShowroomsComponent },
      { path: 'fetch-role/roles', component: ListRolesComponent },
      { path: 'fetch-role/roles/edit/:id', component: EditRoleComponent },
      { path: 'fetch-employee-showroom/employees/edit/:id', component: EditEmployeeComponent },
      { path: 'fetch-employee-showroom/showrooms/edit/:id', component: EditShowroomsComponent },
    ]),
    FontAwesomeModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
