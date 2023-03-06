import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListShowroomsComponent } from './list-showrooms.component';

describe('ListShowroomsComponent', () => {
  let component: ListShowroomsComponent;
  let fixture: ComponentFixture<ListShowroomsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListShowroomsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListShowroomsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
