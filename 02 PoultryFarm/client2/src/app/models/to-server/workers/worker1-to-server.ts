import {BaseEntity} from "../../base/base-entity";
import {BaseEntityToServer} from "../base-entity-to-server";

export class Worker1ToServer extends BaseEntityToServer{
  //СВОЙСТВА
  //Id и WasDeleted наследуются
  public Surname:string
  public Name:string
  public Patronymic:string
  public Passport:string
  public Salary:number

  //КОНСТРУКТОР
  constructor(Id:number,WasDeleted:boolean, Surname: string, Name: string, Patronymic: string, Passport: string, Salary: number) {
    super(Id,WasDeleted);
    this.Surname = Surname;
    this.Name = Name;
    this.Patronymic = Patronymic;
    this.Passport = Passport;
    this.Salary = Salary;
  }

//МЕТОДЫ
  //1. Скопировать
  public assign(worker:any):this{
    super.assign(worker)
    this.Surname = worker.surname;
    this.Name = worker.name;
    this.Patronymic = worker.patronymic;
    this.Passport = worker.passport;
    this.Salary = worker.salary;
    return this;
  }
}
