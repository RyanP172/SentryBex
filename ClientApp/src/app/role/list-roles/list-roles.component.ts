import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { faPenToSquare, faUserGroup } from '@fortawesome/free-solid-svg-icons';
import { Permission } from '../../interface/permission';

import { RoleService } from '../../service/role/role.service';


@Component({
  selector: 'app-list-roles',
  templateUrl: './list-roles.component.html',
  styleUrls: ['./list-roles.component.css']
})
export class ListRolesComponent implements OnInit {
  faPen = faPenToSquare;
  userGroup = faUserGroup;

  @Input() roles: Permission[] | undefined;
  inputValue: string = ""

  @Input() ticked: boolean = false;
  @Input() formIndex: number = 0;
  constructor(private roleServices: RoleService)
  {

  }

  clicked = (value: boolean, index: number) =>
  {
    console.log(this.ticked)
    this.formIndex = index
    return this.ticked = value;
  }

  ngOnInit(): void {
    this.GetRoles();
  }

  GetRoles(): void
  {
    this.roleServices.getRoles().subscribe(
      (res) => { this.roles = res; console.log(this.roles) },
      (error: any) => console.log(error),
      () => console.log("List roles done")
    )
  }

  AddNewRole(): void
  {
    /*console.log('Submitted value:', this.inputValue);*/
    if (this.inputValue === "" || null) {
      alert("Role name cannot be empty")
    }
    else
    {
      if (confirm("Confirm to add this new role?")) {
        this.roleServices.addNewRole(this.inputValue).subscribe({
          next: (res: any) => {
            const resInfo = JSON.parse(res)
            if (resInfo.status == 200) {
              this.roles?.push(resInfo.response);
            }
          },
          error: msg => {
            const errorInfo = JSON.parse(msg.error)
            console.log(errorInfo)
            if (errorInfo.status === 400) {
              alert(errorInfo.message)
            }
            else {
              console.log(msg)
            }

          }
        })
      }
    }
  }
  ChangeRoleName(form: NgForm, index: number, roleId: string)
  {
    console.log("form valid? ", form.valid)
    if (form.valid) {
      if (confirm("Confirm to change the role name?")) {
        console.log("test", form.value, index, roleId)
        const roleName = form.value.roleNameForChange;
        this.roleServices.changeRoleName(roleId, roleName).subscribe({
          next: (res: any) => {
            const resInfo = JSON.parse(res)
            if (resInfo.status == 200) {
              alert(resInfo.message);
            }
          },
          error: msg => {
            const errorInfo = JSON.parse(msg.error)
            console.log(errorInfo)
            if (errorInfo.status === 400) {
              alert(errorInfo.message)
            }
            else {
              console.log(msg)
            }

          }
        })

      }

    } 
  }
}
