var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
//1. Импорт некоторых typescript-модулей
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home-2/home.component';
import { PersonsComponent } from './components/persons-2/persons.component';
import { RouterModule } from "@angular/router";
import { routes } from "./app.routes";
import { DbService } from "./services/db-service";
// 2. @NgModule - декоратор, который определяет angular-модуль и некоторые данные
// для него. Для того чтобы приложение могло выполняться в браузере, текущий
// модуль (корневой модуль) должен выполнить импорт модуля BrowserModule взятого
// из @angular/platform-browser. Задача BrowserModule зарегистрировать основные
// сервис провайдеры приложения, а также добавить общие директивы такие как ngIf и
// ngFor
let AppModule = class AppModule {
};
AppModule = __decorate([
    NgModule({
        imports: [
            BrowserModule,
            RouterModule.forRoot(routes)
        ],
        //1. Список импортированных компонентов (тогда они смогут взаимодействовать друг-
        //с-другом)
        declarations: [AppComponent, HomeComponent, PersonsComponent],
        //2. Тут указывается компонент с которого начинается отображение приложения.
        bootstrap: [AppComponent],
        providers: [DbService]
    })
], AppModule);
export { AppModule };
