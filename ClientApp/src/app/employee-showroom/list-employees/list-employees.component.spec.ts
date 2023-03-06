import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeShowroomComponent } from './list-employees.component';

describe('EmployeeShowroomComponent', () => {
  let component: EmployeeShowroomComponent;
  let fixture: ComponentFixture<EmployeeShowroomComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeShowroomComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeShowroomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
