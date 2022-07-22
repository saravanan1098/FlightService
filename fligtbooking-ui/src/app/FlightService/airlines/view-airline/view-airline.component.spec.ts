import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAirlineComponent } from './view-airline.component';

describe('ViewAirlineComponent', () => {
  let component: ViewAirlineComponent;
  let fixture: ComponentFixture<ViewAirlineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewAirlineComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewAirlineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
