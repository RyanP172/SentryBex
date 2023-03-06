import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { filter } from 'rxjs';

@Component({
  selector: 'app-input-filter',
  templateUrl: './input-filter.component.html',
  styleUrls: ['./input-filter.component.css']
})
export class InputFilterComponent implements OnInit {
  @Output() messageEvent = new EventEmitter<string>();
  filterText: string = "";

  @Input() placeHolder: string = "";


  sendFilter()
  {
    this.messageEvent.emit(this.filterText);
  }

  glass = faMagnifyingGlass;


  ngOnInit()
  {
    
  }
}
