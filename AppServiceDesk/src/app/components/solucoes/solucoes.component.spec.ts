import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolucoesComponent } from './solucoes.component';

describe('SolucoesComponent', () => {
  let component: SolucoesComponent;
  let fixture: ComponentFixture<SolucoesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SolucoesComponent]
    });
    fixture = TestBed.createComponent(SolucoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
