import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkerTemplateComponent } from './worker-template.component';

describe('WorkerTemplateComponent', () => {
  let component: WorkerTemplateComponent;
  let fixture: ComponentFixture<WorkerTemplateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkerTemplateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkerTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
