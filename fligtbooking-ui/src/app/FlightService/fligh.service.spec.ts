import { TestBed } from '@angular/core/testing';

import { FlighService } from './fligh.service';

describe('FlighService', () => {
  let service: FlighService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FlighService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
