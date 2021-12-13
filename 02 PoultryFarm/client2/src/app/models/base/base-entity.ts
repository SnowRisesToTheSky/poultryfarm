export class BaseEntity {
    //СВОЙСТВА
    public id:number
    public wasDeleted:boolean
    //КОНСТРУКТОР
    constructor(id: number=null, wasDeleted: boolean=null) {
        this.id = id;
        this.wasDeleted = wasDeleted;
    }

    //МЕТОДЫ
    //1. Скопировать
    public assign(baseEntity:any):this{
        this.id = baseEntity.id;
        this.wasDeleted = baseEntity.wasDeleted;
        return this
    }
}
