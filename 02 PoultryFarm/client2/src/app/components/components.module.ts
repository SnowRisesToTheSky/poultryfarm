import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { WorkersComponent } from './worker-page/workers/workers.component';
import { WorkerTemplateComponent } from './worker-page/worker-template/worker-template.component';
import {RouterModule} from "@angular/router";
import { WorkerAddingComponent } from './worker-page/worker-adding/worker-adding.component';
import {ReactiveFormsModule} from "@angular/forms";
import { WorkerLeftNavPanel1Component } from './worker-page/worker-left-nav-panel1/worker-left-nav-panel1.component';
import { WorkerLeftNavPanel2Component } from './worker-page/worker-left-nav-panel2/worker-left-nav-panel2.component';
import { PaginationComponent } from './pagination/pagination.component';
import {routes} from "../routing";



@NgModule({
  declarations: [
    HomeComponent,
    WorkersComponent,
    WorkerTemplateComponent,
    WorkerAddingComponent,
    WorkerLeftNavPanel1Component,
    WorkerLeftNavPanel2Component,
    PaginationComponent
  ],
  imports: [
    // RouterModule.forRoot(routes),
    RouterModule,
    CommonModule,
    ReactiveFormsModule
  ]
})
export class ComponentsModule { }
