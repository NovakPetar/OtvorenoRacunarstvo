import { TestBed } from '@angular/core/testing';

import { OpenDataService } from './open-data.service';

describe('OpenDataService', () => {
  let service: OpenDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OpenDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
