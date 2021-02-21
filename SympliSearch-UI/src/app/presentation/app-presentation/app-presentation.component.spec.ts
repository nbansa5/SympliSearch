import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppPresentationComponent } from './app-presentation.component';

describe('AppPresentationComponent', () => {
  let component: AppPresentationComponent;
  let fixture: ComponentFixture<AppPresentationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppPresentationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppPresentationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
