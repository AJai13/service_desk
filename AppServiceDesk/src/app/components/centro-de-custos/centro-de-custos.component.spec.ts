import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CentroDeCustosComponent } from './centro-de-custos.component';

describe('CentroDeCustosComponent', () => {
  let component: CentroDeCustosComponent;
  let fixture: ComponentFixture<CentroDeCustosComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CentroDeCustosComponent]
    });
    fixture = TestBed.createComponent(CentroDeCustosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
