//Класс для данных о персонах, которые будем получать с сервера
export class Person {
    //КОНСТРУКТОРА
    constructor(id, surname, name, patronymic, wasDeleted) {
        this._id = id;
        this._surname = surname;
        this._name = name;
        this._patronymic = patronymic;
        this._wasDeleted = wasDeleted;
    }
    //ГЕТТЕРЫ
    get id() {
        return this._id;
    }
    get surname() {
        return this._surname;
    }
    get name() {
        return this._name;
    }
    get patronymic() {
        return this._patronymic;
    }
    get wasDeleted() {
        return this._wasDeleted;
    }
    //СЕТТЕРЫ
    set id(value) {
        this._id = value;
    }
    set surname(value) {
        this._surname = value;
    }
    set name(value) {
        this._name = value;
    }
    set patronymic(value) {
        this._patronymic = value;
    }
    set wasDeleted(value) {
        this._wasDeleted = value;
    }
}
