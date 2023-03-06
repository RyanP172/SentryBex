import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AspNetUser } from '../../interface/aspNetUser';
import { ExchangeBoxRoleRoom } from '../../interface/exchangeBox';
import { Permission } from '../../interface/permission';
import { ShowRoom } from '../../interface/show-room';

@Component({
  selector: 'app-exchange-box',
  templateUrl: './exchange-box.component.html',
  styleUrls: ['./exchange-box.component.css']
})
export class ExchangeBoxComponent implements OnInit {
  @Input() checkboxTitle: string = "";

  @Input() assignedRooms: ShowRoom[] = [];
  @Input() candidateRooms: ShowRoom[] = [];

  @Input() assignedRoles: Permission[] = [];
  @Input() candidateRoles: Permission[] = [];

  @Input() assignedUsers: AspNetUser[] = [];
  @Input() candidateUsers: AspNetUser[] = [];

  @Input() exchangBoxType: number = 0
  @Output("removeRooms") removeRooms: EventEmitter<any> = new EventEmitter();
  @Output("assignRooms") assignRooms: EventEmitter<any> = new EventEmitter();

  @Output("removeRoles") removeRoles: EventEmitter<any> = new EventEmitter();
  @Output("assignRoles") assignRoles: EventEmitter<any> = new EventEmitter();

  @Output("removeUsers") removeUsers: EventEmitter<any> = new EventEmitter();
  @Output("assignUsers") assignUsers: EventEmitter<any> = new EventEmitter();

  filterTextForAssignedEmployees: string = "";
  filterTextForCandidateEmployees: string = "";

  receiveFilterForAssignedEmployee($event: string) {
    this.filterTextForAssignedEmployees = $event;
  }
  receiveFilterForCandidateEmployee($event: string) {
    this.filterTextForCandidateEmployees = $event;
  }

  placeHolder: string = "Type in email..."


 /* exchangeBoxItemRoleRoom: ExchangeBoxRoleRoom = {
    itemsFromCandidateRooms: [],
    itemsFromAssignedRooms: [],
    itemsFromAssignedRoles: [],
    itemsFromCandidateRoles: []
  };*/

  

  ngOnInit()
  {
    console.log(this.exchangBoxType)
    console.log("from reusable", this.candidateRooms)
  }
}
