import { TestBed } from '@angular/core/testing';

import { SlasService } from './slas.service';

describe('SlasService', () => {
  let service: SlasService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SlasService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
