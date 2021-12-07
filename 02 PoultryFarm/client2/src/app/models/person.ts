import {DbModel} from "./db-model";

//Класс для данных о персонах, которые будем получать с сервера
export class Person implements DbModel{
    //ПОЛЯ
    private _id: number;
    private _surname:string;
    private _name:string;
    private _patronymic:string;
    private _wasDeleted: boolean;

    //КОНСТРУКТОРА
    constructor(id: number, surname: string, name: string, patronymic: string, wasDeleted: boolean) {
        this._id = id;
        this._surname = surname;
        this._name = name;
        this._patronymic = patronymic;
        this._wasDeleted = wasDeleted;
    }

    //ГЕТТЕРЫ
    get id(): number {
        return this._id;
    }
    get surname(): string {
        return this._surname;
    }
    get name(): string {
        return this._name;
    }
    get patronymic(): string {
        return this._patronymic;
    }
    get wasDeleted(): boolean {
        return this._wasDeleted;
    }
    //СЕТТЕРЫ
    set id(value: number) {
        this._id = value;
    }
    set surname(value: string) {
        this._surname = value;
    }
    set name(value: string) {
        this._name = value;
    }
    set patronymic(value: string) {
        this._patronymic = value;
    }
    set wasDeleted(value: boolean) {
        this._wasDeleted = value;
    }
}