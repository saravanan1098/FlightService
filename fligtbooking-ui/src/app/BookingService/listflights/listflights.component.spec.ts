import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListflightsComponent } from './listflights.component';

describe('ListflightsComponent', () => {
  let component: ListflightsComponent;
  let fixture: ComponentFixture<ListflightsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListflightsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListflightsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
