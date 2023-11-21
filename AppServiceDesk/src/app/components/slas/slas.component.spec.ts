import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlasComponent } from './slas.component';

describe('SlasComponent', () => {
  let component: SlasComponent;
  let fixture: ComponentFixture<SlasComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SlasComponent]
    });
    fixture = TestBed.createComponent(SlasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
