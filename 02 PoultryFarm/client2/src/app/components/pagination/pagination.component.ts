import {EventEmitter, OnChanges} from '@angular/core';
import {Component, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit, OnChanges {

  //ПОЛЯ
  //1. Индекс текущей партии
  @Input()
  public currentBatchIndex: number = 0
  //2. Кол-во партий
  @Input()
  public batchCount: number = 1

  //3. Событие зажигаемое при нажатии на номер партии
  @Output() onSelectBatch = new EventEmitter<number>();

  //4. Стартовая, центральная и конечная части пагинатора
  public start: number = 1
  public middle: number[] = []
  public end: number = null

  //КОНСТРУКТОР
  constructor() {
  }

  //ПЕРЕОПРЕДЕЛЕНИЯ
  ngOnInit(): void {
    this.constructPaginator();
  }

  ngOnChanges(){
    this.constructPaginator();
  }

  //МЕТОДЫ
  //1. Сконструировать все части пагинатора
  private constructPaginator() {
    this.setStart()
    this.constructMiddle()
    this.setEnd()
  }

  //2. Установить начальную часть пагинатора
  private setStart(){
    this.start=this.currentBatchIndex>2?1:null;
  }

  //3. Сконструировать конечную часть пагинатора
  private setEnd(){
    this.end=this.currentBatchIndex+3<this.batchCount?this.batchCount:null;
  }

  //4. Сконструировать среднюю часть пагинатора
  private constructMiddle() {
    this.middle = new Array<number>(5)
      //1. Заполнить начальными значениями
      .fill(1)
      //2. Генерируем номера
      .map((value, index) => {
        //1. -2, т.к. средняя часть д. начинаться на 2 номера раньше текущего
        //2. +1, т.к. из индексов делаем номера
        //3. Итого -1
        return index+this.currentBatchIndex-1
      })
      //3. Фильтруем от заграничных номеров
      .filter(num=>0<num&&num<=this.batchCount);
  }

  //5. Для возможности вывода сообщений из шаблона
  log(message: any) {
    console.log(message)
  }

  //6. Перейти к партии
  goToBatch(batchNumber:number){
    //1. Зажечь событие
    //2. Выбрать другой номер партии
    this.onSelectBatch.emit(batchNumber-1)
    this.selectBatch(batchNumber-1)
  }

  //7. Выбрать следующий номер партии и переконструировать пагинатор
  selectBatch(batchIndex:number){
    this.currentBatchIndex=batchIndex;
    this.constructPaginator()
  }
}
