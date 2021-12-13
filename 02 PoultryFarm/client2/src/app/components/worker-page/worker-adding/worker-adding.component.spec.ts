import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkerAddingComponent } from './worker-adding.component';

describe('WorkerAddingComponent', () => {
  let component: WorkerAddingComponent;
  let fixture: ComponentFixture<WorkerAddingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkerAddingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkerAddingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
