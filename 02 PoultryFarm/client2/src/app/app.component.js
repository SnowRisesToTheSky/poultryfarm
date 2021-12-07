var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
//1. импорт декоратора Component из модуля @angular/core
import { Component } from '@angular/core';
//2. Применение декоратора Component для класса
let AppComponent = 
//3. Класс определяющий поведение компонента
class AppComponent {
};
AppComponent = __decorate([
    Component({
        //1. имя селектора компонента в разметке
        selector: "my-app",
        //2. Ссылка на файл с разметкой компоненты
        templateUrl: "./app.component.html",
    })
    //3. Класс определяющий поведение компонента
], AppComponent);
export { AppComponent };
