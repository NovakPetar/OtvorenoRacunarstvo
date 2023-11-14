import { TestBed } from '@angular/core/testing';

import { AutocestaService } from './autocesta.service';

describe('AutocestaService', () => {
  let service: AutocestaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AutocestaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
