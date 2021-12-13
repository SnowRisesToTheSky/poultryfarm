using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poultryfarm.Backend.Repositories;
using Poultryfarm.Backend.Repositories.Workers;
using Poultryfarm.ModelsAndUtils.Db.Models.Persons;
using Poultryfarm.ModelsAndUtils.Http.Workers;
using sidonia.backend.Controllers;
using System.Linq;

namespace Poultryfarm.Backend.Controllers.Workers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkersController : BaseControllerA<Worker>
    {
        public WorkersController() : base(new WorkerRepository())
        {
        }

        // GET
        //1. Получить все записи
        [HttpGet()]
        [ActionName("get-all")]
        public IActionResult GetAll()
        {
            var res = repo.GetAll().Include(w => w.Person).Select(w => new ClientWorker
            {
                id = w.Id,
                wasDeleted = w.WasDeleted,
                surname = w.Person.Surname,
                name = w.Person.Name,
                patronymic = w.Person.Patronymic,
                passport = w.Person.Passport,
                salary = w.Salary
            }).ToList();
            return Ok(res);
        }

        //1. Получить одну запись
        [HttpGet()]
        [ActionName("get-one")]
        public IActionResult GetOne([FromQuery(Name = "id")] int? id)
        {
            var res = repo.GetOne(id).Include(w => w.Person).Select(w => new ClientWorker
            {
                id = w.Id,
                wasDeleted = w.WasDeleted,
                surname = w.Person.Surname,
                name = w.Person.Name,
                patronymic = w.Person.Patronymic,
                passport = w.Person.Passport,
                salary = w.Salary
            }).FirstOrDefault();
            return Ok(res);
        }

        //1. Получить одну запись
        [HttpGet()]
        [ActionName("get-one/even-deleted")]
        public IActionResult GetOneEvenDeleted([FromQuery(Name = "id")] int? id)
        {
            var res = repo.GetOneEvenDeleted(id).Include(w => w.Person).Select(w => new ClientWorker
            {
                id = w.Id,
                wasDeleted = w.WasDeleted,
                surname = w.Person.Surname,
                name = w.Person.Name,
                patronymic = w.Person.Patronymic,
                passport = w.Person.Passport,
                salary = w.Salary
            }).FirstOrDefault();
            return Ok(res);
        }

        //1. Получить все записи, даже удаленные
        [HttpGet()]
        [ActionName("get-all/even-deleted")]
        public IActionResult GetAllEvenDeleted()
        {
            var res = repo.GetAllEvenDeleted().Include(w => w.Person).Select(w => new ClientWorker
            {
                id = w.Id,
                wasDeleted = w.WasDeleted,
                surname = w.Person.Surname,
                name = w.Person.Name,
                patronymic = w.Person.Patronymic,
                passport = w.Person.Passport,
                salary = w.Salary
            }).ToList();
            return Ok(res);
        }

        //2. Получить партию записей
        [HttpGet]
        [ActionName("get-all/by-chunk/even-deleted")]
        public IActionResult GetAllByChunkEvenDeleted([FromQuery(Name = "curChunkIndex")] int? curChunkIndex, [FromQuery(Name = "chunkSize")] int? chunkSize)
        {
            var workers = repo
                .GetChunkFromAllEvenDeleted(curChunkIndex, chunkSize)
                .Include(w => w.Person)
                .Select(w => new ClientWorker
                {
                    id = w.Id,
                    wasDeleted = w.WasDeleted,
                    surname = w.Person.Surname,
                    name = w.Person.Name,
                    patronymic = w.Person.Patronymic,
                    passport = w.Person.Passport,
                    salary = w.Salary
                })
                .ToList();
            return Ok(workers);
        }

        //2. Получить партию записей
        [HttpGet]
        [ActionName("get-all/by-chunk")]
        public IActionResult GetAllByChunk([FromQuery(Name = "curChunkIndex")] int? curChunkIndex, [FromQuery(Name = "chunkSize")] int? chunkSize)
        {
            var workers = repo
                .GetChunkFromAll(curChunkIndex, chunkSize)
                .Include(w => w.Person)
                .Select(w => new ClientWorker
                {
                    id = w.Id,
                    wasDeleted = w.WasDeleted,
                    surname = w.Person.Surname,
                    name = w.Person.Name,
                    patronymic = w.Person.Patronymic,
                    passport = w.Person.Passport,
                    salary = w.Salary
                })
                .ToList();
            return Ok(workers);
        }

        //3. Получить кол-во партий записей
        [HttpGet]
        [ActionName("get-chunk-count/from-all")]
        public IActionResult GetChunkCountFromAll([FromQuery(Name = "chunkSize")] int? chunkSize)
        {
            var workers = repo.GetChunkCountFromAll(chunkSize);
            return Ok(workers);
        }

        //3. Получить кол-во партий записей
        [HttpGet]
        [ActionName("get-chunk-count/from-all/even-deleted")]
        public IActionResult GetChunkCountFromAllEvenDeleted([FromQuery(Name = "chunkSize")] int? chunkSize)
        {
            var workers = repo.GetChunkCountFromAllEvenDeleted(chunkSize);
            return Ok(workers);
        }

        //4. Добавить одного рабочего
        [HttpPost]
        [ActionName("add-one")]
        public IActionResult AddOne(ClientWorker w)
        {
            //Персона
            var person = new Person
            {
                Surname = w.surname,
                Name = w.name,
                Patronymic = w.patronymic,
                Passport = w.passport,
            };
            //Рабочий
            var worker = new Worker
            {
                Person = person,
                Salary = w.salary
            };
            //Добавить рабочего в базу данных
            var result = repo.CreateOne(worker);
            //Ответ
            return Ok(result);
        }

        //5. Обновить одного рабочего
        [HttpPut]
        [ActionName("update-one")]
        public IActionResult UpdateOne(ClientWorker w)
        {
            //Подгружаем связанные данные в контекст
            var worker = repo.GetOne(w.id).FirstOrDefault();
            repo.Context.Entry(worker).Reference(w=>w.Person).Load();
            //Обновить рабочего в базе данных
            var result = repo.UpdateOne(w.id, updatedWorker =>
            {
                updatedWorker.Person.Surname = w.surname;
                updatedWorker.Person.Name = w.name;
                updatedWorker.Person.Patronymic = w.patronymic;
                updatedWorker.Person.Passport = w.passport;
                updatedWorker.Salary = w.salary;
            });
            //Ответ
            return Ok(result);
        }

        //6. Удалить одного рабочего
        [HttpDelete]
        [ActionName("delete-one")]
        public IActionResult DeleteOne([FromQuery(Name = "id")] int? id)
        {
            //Удаляем рабочего в бд
            var result = repo.DeleteOne(id);
            //Ответ (Возврат удаленного рабочего)
            return Ok(result);
        }

        //6. Восстановить одного рабочего
        [HttpDelete]
        [ActionName("restore-one")]
        public IActionResult RestoreOne([FromQuery(Name = "id")] int? id)
        {
            //Восстанавливаем рабочего в бд
            var result = repo.RestoreOne(id);
            //Ответ (Возврат восстановленного рабочего)
            return Ok(result);
        }

    }
}
