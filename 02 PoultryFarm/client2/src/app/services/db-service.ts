import {Injectable} from "@angular/core";
import {Person} from "../models/person";
import { HttpClient } from "@angular/common/http";
import {Observable} from "rxjs";

// сервис для получения данных с сервера
@Injectable()
export class DbService {
    //ПОЛЯ
    private static readonly HOST = "http://localhost:8180/"

    //КОНСТРУКТОРА
    constructor(private http: HttpClient) { }

    //МЕТОДЫ
    // получить данные о персонах
    getAllPersons(): Observable<Object>{
        //запрос-ответ от сервера
        return this.http.get(`${DbService.HOST}persons/get/all`);
    }
}