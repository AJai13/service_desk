import { TestBed } from '@angular/core/testing';

import { CentroDeCustosService } from './centro-de-custos.service';

describe('CentroDeCustosService', () => {
  let service: CentroDeCustosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CentroDeCustosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
