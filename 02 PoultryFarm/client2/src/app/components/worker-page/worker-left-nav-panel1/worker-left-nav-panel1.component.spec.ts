import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkerLeftNavPanel1Component } from './worker-left-nav-panel1.component';

describe('WorkerLeftNavPanel1Component', () => {
  let component: WorkerLeftNavPanel1Component;
  let fixture: ComponentFixture<WorkerLeftNavPanel1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkerLeftNavPanel1Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkerLeftNavPanel1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
