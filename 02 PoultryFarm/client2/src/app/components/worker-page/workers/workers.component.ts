import {Component, OnInit} from '@angular/core';
import {Worker1} from "../../../models";
import {WorkersService} from "../../../services";
import {ActivatedRoute, Router} from "@angular/router";
import { Location } from '@angular/common';
import {Subscription} from "rxjs";
import * as $ from "jquery"
import "bootstrap"

@Component({
  selector: 'app-workers',
  templateUrl: './workers.component.html',
  styleUrls: ['./workers.component.css']
})
export class WorkersComponent implements OnInit {

  //КОНСТАНТЫ
  public readonly CHUNK_SIZE = 25;

  //ПОЛЯ
  //1. Подгружаемый массив рабочих
  //2. Индекс текущей партии
  //3. Кол-во партий
  public workers: Promise<Worker1[]>
  public curChunkIndex: number=0;
  public chunkCount: Promise<number>;

  //4. Для работы с параметрами маршрута
  public state: string | undefined;



  //КОНСТРУКТОР
  constructor(public service: WorkersService, private activateRoute: ActivatedRoute,private router: Router,private location: Location) {
    //Достать параметры маршрута
    activateRoute.params.subscribe(params => this.state = params['state']);

  }

  //МЕТОДЫ ВРЕМЕНИЕ ЖИЗНИ КОМПОНЕНТА
  ngOnInit(): void {
    this.loadRecords();

    //Запуск модального окна в случае если только-что была добавлена запись
    $('#modalRecordAdded').modal({
      show: this.state === 'record-added'
    })
    //Изменяем маршрут на стандартный после его обработки
    this.location.replaceState(this.router.serializeUrl(this.router.createUrlTree(['/workers/look-at-records', 'just-show-records'])));
  }

  //ДРУГИЕ МЕТОДЫ
  //1. Выбрать следующую партию
  public onSelectBatch(batchIndex){
    this.curChunkIndex=batchIndex;
    this.loadRecords();
  }

  //2. Загрузить записи текущей партии
  public loadRecords(){
    this.workers=this.service.getAllByChunk(this.curChunkIndex,this.CHUNK_SIZE);
    this.chunkCount=this.service.getChunkCountFromAll(this.CHUNK_SIZE)
  }
}
