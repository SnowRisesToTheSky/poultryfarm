import {BaseEntity} from "../../base/base-entity";

export class Worker1 extends BaseEntity{
    //СВОЙСТВА
    //id и wasDeleted наследуются
    public surname:string
    public name:string
    public patronymic:string
    public passport:string
    public salary:number

    //КОНСТРУКТОР
    constructor(id: number=null, wasDeleted: boolean=null, surname: string=null, name: string=null, patronymic: string=null, passport: string=null, salary: number=null) {
        super(id, wasDeleted);
        this.surname = surname;
        this.name = name;
        this.patronymic = patronymic;
        this.passport = passport;
        this.salary = salary;
    }
    //МЕТОДЫ
    //1. Скопировать
    public assign(worker:any):this{
        super.assign(worker)
        this.surname = worker.surname;
        this.name = worker.name;
        this.patronymic = worker.patronymic;
        this.passport = worker.passport;
        this.salary = worker.salary;
        return this;
    }
}
