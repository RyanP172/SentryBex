import { Component, Input, OnInit } from '@angular/core';
import { ShowRoom } from '../../interface/show-room';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeShowroomService } from '../../service/employee-showroom/employee-showroom.service';
import { Employee } from '../../interface/employee';


@Component({
  selector: 'app-edit-showrooms',
  templateUrl: './edit-showrooms.component.html',
  styleUrls: ['./edit-showrooms.component.css']
})
export class EditShowroomsComponent {

  @Input() epeEmployee: Employee[] = []
  
  @Input() showRoom: ShowRoom = {
      id: 0,
      companyFk: 0,
      name: '',
      shopCode: '',
      orderPrefix: 0,
      defaultConsultantFk: 0,
      monthlyBudget: 0,
      state: '',
    loadDay: 0,
    selected: false
  }





  statusDesc: any = {
    status: 0,
    description:''
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private employeeShowRoomService: EmployeeShowroomService
  )
  {

  }

  ngOnInit(): void
  {
    this.route.paramMap.subscribe({
      next: (param) => {
        const showRoomId = param.get('id');
        if (showRoomId) {
          this.employeeShowRoomService.getShowRoom(Number(showRoomId)).subscribe({
            next: res => {
              this.showRoom = res;
              console.log(this.showRoom);
            }
          })
        }
      }
    });
    this.employeeShowRoomService.getEmployees().subscribe({
      next: res =>
      {
        this.epeEmployee = res;
      }
    });
  }

  updateShowRoom()
  {
    this.employeeShowRoomService.updateShowRoom(this.showRoom.id, this.showRoom).subscribe({
      
      next: (res) => {
        if (this.showRoom.id == res.id)
        {

          this.statusDesc.status = 200;
          this.statusDesc.description = "Update Success! Redirecting"
          setTimeout(() => {
            this.router.navigate(['fetch-employee-showroom/showrooms']);
          }, 2000);
        }
        
      },
      error: msg =>
      {
        
        this.statusDesc.status = msg.error.status;
        this.statusDesc.description = msg.error.title + " Please make sure your input data is in right format.";
        console.log(this.statusDesc)
      }
    })
  }
}
