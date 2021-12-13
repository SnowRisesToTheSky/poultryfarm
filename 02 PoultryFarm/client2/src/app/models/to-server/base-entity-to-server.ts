export class BaseEntityToServer {
  //СВОЙСТВА
  public Id:number
  public WasDeleted:boolean
  //КОНСТРУКТОР
  constructor(Id: number=null, WasDeleted: boolean=null) {
    this.Id = Id;
    this.WasDeleted = WasDeleted;
  }

  //МЕТОДЫ
  //1. Скопировать
  public assign(baseEntity:any):this{
    this.Id = baseEntity.id;
    this.WasDeleted = baseEntity.wasDeleted;
    return this
  }
}
