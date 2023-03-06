import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditShowroomsComponent } from './edit-showrooms.component';

describe('EditShowroomsComponent', () => {
  let component: EditShowroomsComponent;
  let fixture: ComponentFixture<EditShowroomsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditShowroomsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditShowroomsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
