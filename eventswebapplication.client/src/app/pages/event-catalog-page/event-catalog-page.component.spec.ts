import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventCatalogPageComponent } from './event-catalog-page.component';

describe('EventCatalogPageComponent', () => {
  let component: EventCatalogPageComponent;
  let fixture: ComponentFixture<EventCatalogPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EventCatalogPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EventCatalogPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
