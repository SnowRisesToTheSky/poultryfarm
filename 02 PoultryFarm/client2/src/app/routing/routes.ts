//1. Стандартный импорт
//2. Импорт моих компонентов
import {Routes} from "@angular/router";
import {
  HomeComponent,
  WorkerAddingComponent,
  WorkerLeftNavPanel1Component, WorkerLeftNavPanel2Component,
  WorkersComponent,
  WorkerTemplateComponent
} from "./../components";

//3. Настройка маршрутов
export const routes: Routes = [
  { path: "", redirectTo: "home", pathMatch: "full" },
  { path: "home", component: HomeComponent },
  { path: "workers", component: WorkerTemplateComponent,
    children: [
      {
        path: 'look-at-records/:state',
        children: [
          //Подмаршруты, определение дочерних компонентов шаблона
          {outlet: "left-nav-outlet", path: "", component: WorkerLeftNavPanel1Component,},
          {outlet: "central-content-outlet", path: "", component: WorkersComponent,},
        ]
      },
      {
        path: 'add-a-record',
        children: [
          // //Подмаршруты, определение дочерних компонентов шаблона
          {outlet: "left-nav-outlet", path: "", component: WorkerLeftNavPanel2Component,},
          {outlet: "central-content-outlet", path: "", component: WorkerAddingComponent,},
        ]
      },
    ]
  }
]
