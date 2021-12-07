//1. импорт декоратора Component из модуля @angular/core
import { Component } from '@angular/core';

//2. Применение декоратора Component для класса
@Component({
    //1. имя селектора компонента в разметке
    selector: "my-app",
    //2. Ссылка на файл с разметкой компоненты
    templateUrl: "./app.component.html",
})

//3. Класс определяющий поведение компонента
export class AppComponent {
    // //СВОЙСТВА
    // //1. Некоторый массив
    // //2. Текущий выбранный язык
    // PhraseList: Phrase[] = PHRASES;
    // selectedPhraseLanguage: string;
    //
    // //МЕТОДЫ
    // //1. Обработчик события, к которому привязаны элементы li из файла hello-world-
    // // list.persons-2.html
    // onSelect(selected: Phrase) {
    //     this.selectedPhraseLanguage = selected.language;
    // }
}
