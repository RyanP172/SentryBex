import { TestBed } from '@angular/core/testing';

import { EmployeeShowroomService } from './employee-showroom.service';

describe('EmployeeShowroomService', () => {
  let service: EmployeeShowroomService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmployeeShowroomService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
