import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboutCompanyPageComponent } from './about-company-page.component';

describe('AboutCompanyPageComponent', () => {
  let component: AboutCompanyPageComponent;
  let fixture: ComponentFixture<AboutCompanyPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AboutCompanyPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AboutCompanyPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
