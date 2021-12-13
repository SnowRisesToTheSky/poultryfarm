//1. Импорт некоторых typescript-модулей
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from "@angular/router";
import {HttpClientModule} from '@angular/common/http';
//2. Компоненты
import {AppComponent} from './app.component';
//3. Сервисы и маршруты
import {routes} from "./routing";
import { ComponentsModule } from './components/components.module';
import { ServicesModule } from './services/services.module';

// 2. @NgModule - декоратор, который определяет angular-модуль и некоторые данные
// для него. Для того чтобы приложение могло выполняться в браузере, текущий
// модуль (корневой модуль) должен выполнить импорт модуля BrowserModule взятого
// из @angular/platform-browser. Задача BrowserModule зарегистрировать основные
// сервис провайдеры приложения, а также добавить общие директивы такие как ngIf и
// ngFor
@NgModule({
    imports: [
        BrowserModule,
        HttpClientModule,
        RouterModule.forRoot(routes),
        ComponentsModule,
        ServicesModule
    ],
    //1. Список импортированных компонентов (тогда они смогут взаимодействовать друг-
    //с-другом)
    declarations: [AppComponent],
    //2. Тут указывается компонент с которого начинается отображение приложения.
    bootstrap: [AppComponent],
})
export class AppModule {
}
