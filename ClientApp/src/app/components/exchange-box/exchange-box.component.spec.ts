import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExchangeBoxComponent } from './exchange-box.component';

describe('ExchangeBoxComponent', () => {
  let component: ExchangeBoxComponent;
  let fixture: ComponentFixture<ExchangeBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExchangeBoxComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExchangeBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
