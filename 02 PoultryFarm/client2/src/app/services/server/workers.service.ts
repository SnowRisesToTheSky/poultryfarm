import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Worker1} from "../../models";

@Injectable({
  providedIn: 'root'
})
//Сервис для получения данных по рабочим с сервера
export class WorkersService {
  //ПОЛЯ
  private static readonly HOST = "http://localhost:2580"

  //КОНСТРУКТОР
  constructor(private http: HttpClient){ }

  //МЕТОДЫ
  //1. Получить всех рабочих
  getAll(){
    return this.http
        .get(`${WorkersService.HOST}/api/workers/get-all`).toPromise()
        .then((list:[])=>{
          return list.map(el=>new Worker1().assign(el))
    })
  }

  //2. Получить одну партию записей рабочих
  getAllByChunk(curChunkIndex:number,chunkSize:number){
    return this.http
      .get(`${WorkersService.HOST}/api/workers/get-all/by-chunk?curChunkIndex=${curChunkIndex}&chunkSize=${chunkSize}`).toPromise()
      .then((list:[])=>{
        return list.map(el=>new Worker1().assign(el))
      })
  }

  //3. Получить кол-во партий
  getChunkCountFromAll(chunkSize:number):Promise<number>{
    return this.http
      .get(`${WorkersService.HOST}/api/workers/get-chunk-count/from-all?chunkSize=${chunkSize}`).toPromise()
      .then((body:number)=>body)
  }

  // //4. Получить одного рабочего
  // getOneAsync(id:number){
  //   return this.http.get(`${WorkersService.HOST}/api/workers/get-one?id=${id}`)
  // }
}
