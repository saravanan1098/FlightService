import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PnrsearchComponent } from './pnrsearch.component';

describe('PnrsearchComponent', () => {
  let component: PnrsearchComponent;
  let fixture: ComponentFixture<PnrsearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PnrsearchComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PnrsearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
