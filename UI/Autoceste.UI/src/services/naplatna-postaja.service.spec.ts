import { TestBed } from '@angular/core/testing';

import { NaplatnaPostajaService } from './naplatna-postaja.service';

describe('NaplatnaPostajaService', () => {
  let service: NaplatnaPostajaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NaplatnaPostajaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
