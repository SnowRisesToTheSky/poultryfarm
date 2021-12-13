//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using sidonia.backend.IRepositories;
//using sidonia.backend.IRepositories.Products;
//using sidonia.backend.Repositories;
//using sidonia.backend.Repositories.Products;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//namespace sidonia.backend.Controllers.Secondary.Outdated
//{

//    public abstract class SomeController<T> : ControllerBase, IDisposable//, ISomeController<T>
//        where T : class
//    {
//        //СВОЙСТВА
//        //Репозиторий для доступа к записям таблицы
//        //(содержит ссылку на контекст бд)
//        protected BaseRepository<T> repo { get; set; }



//        //КОНСТРУКТОРА (в контроллере м.б. только один)
//        public SomeController(BaseRepository<T> repo)
//        {
//            //Инициализируем репозиторий для контроллера
//            this.repo = repo;
//        }



//        // GET
//        // 1. Получить все записи 
//        //    (Этот метод следует реализовывать в каждом контроллере в отдельности.
//        //    Иначе при его использовании будет отправляться слишком много данных
//        //    связанных между собой)
//        //[HttpGet]
//        //[ActionName("get-all")]
//        //public virtual IActionResult GetAll()
//        //{
//        //    //1. Достать из бд через ее оболочку (репозиторий)
//        //    //все записи о торговых марках
//        //    var tms = repo.GetAll().ToList();
//        //    //2. Если резльтат нулл
//        //    if (tms == null)
//        //        //3. Возвращаем 404
//        //        return NotFound();
//        //    //3. Иначе 200
//        //    return Ok(tms);
//        //}

//        //// 2. Получить одну запись
//        //[HttpGet]
//        ////[HttpGet("{id}")] //так тоже можно, но id будет типа string
//        //[ActionName("get-one")]
//        //public virtual IActionResult GetOne([FromQuery(Name = "id")] int? id)
//        //{
//        //    //1. Достать из бд через ее оболочку (репозиторий)
//        //    //запись с таким ид
//        //    var tms = repo.GetOne(id);
//        //    //2. Если рез-тат нулл
//        //    if (tms == null)
//        //        //3. Возвращаем 404
//        //        return NotFound();
//        //    //3. Иначе 200
//        //    return Ok(tms);
//        //}

//        //// 3. Получить кол-во записей
//        //[HttpGet]
//        //[ActionName("get-count")]
//        //public virtual IActionResult GetCount()
//        //{
//        //    //1. Получить кол-во записей
//        //    int count = repo.GetCount();
//        //    //2. Ответ
//        //    return Ok(new { count = count });
//        //}



//        //// POST
//        //// 4. Добавить новую запись
//        //[HttpPost]
//        //[ActionName("create-one")]
//        //public virtual IActionResult PostOne(T tm)
//        //{
//        //    //1. Создать в бд через ее оболочку (репозиторий)
//        //    //новую запись 
//        //    var tms = repo.CreateOne(tm);
//        //    //2. Если добавить не удалось
//        //    if (tms == null)
//        //        //3. Возвращаем 404
//        //        return NotFound();
//        //    //3. Иначе 200
//        //    return Ok(tms);
//        //}



//        //// PUT
//        //// 5. Обновить запись
//        //[HttpPut]
//        //[ActionName("update-one")]
//        //public virtual IActionResult PutOne(T tm)
//        //{
//        //    //1. Обновить в бд через ее оболочку (репозиторий)
//        //    //указанную запись по ид
//        //    var tms = repo.UpdateOne(tm);
//        //    //2. Если обновить не удалось
//        //    if (tms == null)
//        //        //3. Возвращаем 404
//        //        return NotFound();
//        //    //3. Иначе 200
//        //    return Ok(tms);
//        //}



//        //// DELETE
//        //// 6. Удалить запись
//        //[HttpDelete]
//        //[ActionName("delete-one")]
//        //public virtual IActionResult DeleteOne([FromQuery(Name = "id")] int? id)
//        //{
//        //    //1. Удалить из бд через ее оболочку (репозиторий)
//        //    //указанную запись по ид
//        //    var tms = repo.DeleteOne(id);
//        //    //2. Если удалить не удалось
//        //    if (tms == null)
//        //        //3. Возвращаем 404
//        //        return NotFound();
//        //    //3. Иначе 200
//        //    return Ok(tms);
//        //}



//        //ДРУГИЕ МЕТОДЫ
//        void IDisposable.Dispose()
//        {
//            repo.Dispose();
//        }
//    }
//}
