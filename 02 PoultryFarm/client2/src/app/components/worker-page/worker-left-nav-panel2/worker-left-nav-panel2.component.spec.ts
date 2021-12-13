import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkerLeftNavPanel2Component } from './worker-left-nav-panel2.component';

describe('WorkerLeftNavPanel2Component', () => {
  let component: WorkerLeftNavPanel2Component;
  let fixture: ComponentFixture<WorkerLeftNavPanel2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkerLeftNavPanel2Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkerLeftNavPanel2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
