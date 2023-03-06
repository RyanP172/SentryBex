import { Component, Input, OnInit } from '@angular/core';
import { ShowRoom } from '../../interface/show-room';
import { EmployeeShowroomService } from '../../service/employee-showroom/employee-showroom.service';
import { faMagnifyingGlass, faPenToSquare } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-list-showrooms',
  templateUrl: './list-showrooms.component.html',
  styleUrls: ['./list-showrooms.component.css']
})
export class ListShowroomsComponent implements OnInit {
  @Input() showRooms: ShowRoom[] = [];

  @Input() pageSize: number = 15;

  currentPage = 1;

  get totalPages() {
    return Math.ceil(this.showRooms.length / this.pageSize);
  }

  get pages() {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  changePage(newPage: number) {
    this.currentPage = newPage;
  }

  pageBack() {
    if (this.currentPage > 1)

      this.currentPage = this.currentPage - 1;
    console.log(this.currentPage)
  }

  pageForward() {
    if (this.currentPage < this.totalPages - 1)
      console.log(this.currentPage)
    this.currentPage = this.currentPage + 1;
  }
  get pagedShowRooms() {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.showRooms.slice(startIndex, endIndex);
  }

  pageTop() {
    if (this.currentPage != 1)

      this.currentPage = 1;
    console.log(this.currentPage)
  }

  pageEnd() {
    if (this.currentPage != this.totalPages)

      this.currentPage = this.totalPages;
    console.log(this.currentPage)
  }

  filterText: string = '';

  constructor(private employeeShowRoomService: EmployeeShowroomService) { }

  faShowRoomPen = faPenToSquare
  glass = faMagnifyingGlass;
  ngOnInit(): void {
    this.GetShowRooms();
  }

  GetShowRooms(): void
  {
    this.employeeShowRoomService.getShowRooms().subscribe(
      (res) => { this.showRooms = res; console.log(this.showRooms) },

      (error: any) => console.log(error),
      () => console.log('List show rooms done')
    );
  }

}
