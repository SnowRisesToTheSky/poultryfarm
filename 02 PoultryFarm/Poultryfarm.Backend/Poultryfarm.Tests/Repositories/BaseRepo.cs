using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Poultryfarm.Backend.Repositories.Workers;
using Poultryfarm.ModelsAndUtils.Db.Models.Persons;
using sidonia.db_contexts.Contexts;
using sidonia.db_contexts.Wrappers;
using System.Linq;

namespace Poultryfarm.Tests.Repositories
{
    public class BaseRepo
    {
        //СВОЙСТВА
        public WorkerRepository WorkerRepo { get; set; }
        public PersonRepository PersonRepo { get; set; }

        //ПРЕДНАСТРОЙКИ
        [OneTimeSetUp]
        public void Setup()
        {
            WorkerRepo = new WorkerRepository(new ContextWrapper(new PoultryfarmGenTestingContext()));
            //Чтобы использовался общий контекст
            PersonRepo = new PersonRepository(WorkerRepo.Wrapper);
        }

        //ТЕСТЫ
        [Test]
        //1. Проверка кол-ва записей с информацией о рабочих
        public void CheckWorkerNoteCount_IsGreaterZero()
        {
            //ARRANGE
            var expectedCountLowerBoundValue = 25;

            //ACT
            var count = WorkerRepo.GetCount();

            //ASSERT
            Assert.GreaterOrEqual(count, expectedCountLowerBoundValue, $"Ожидалось, что кол-во записей равно или превосходит {expectedCountLowerBoundValue}");
        }

        [Test]
        //2. Проверка записи с информацией о рабочих
        public void CheckWorkerNote_Success()
        {
            //ARRANGE
            var expectedId = 4;

            //ACT
            //А так не работает
            //Repo.Context.Set<Worker>().Include(w => w.Person);
            //var worker = Repo.GetOne(expectedId).FirstOrDefault();

            //Так работает подгрузка
            var worker = WorkerRepo.GetOne(expectedId).Include(w => w.Person).FirstOrDefault();

            //ASSERT
            Assert.IsNotNull(worker, "Запсиь не д.б. нулл.");
            Assert.IsNotNull(worker.Person, "Связанная запись персоны не д.б. нулл.");
            Assert.AreEqual(expectedId, worker.Id, $"Ожидалось что id=={expectedId}");
            Assert.IsNotEmpty(worker.Person.Name, "Ожидалось наличие имени");
            Assert.IsNotEmpty(worker.Person.Patronymic, "Ожидалось наличие отчества");
            Assert.IsNotEmpty(worker.Person.Surname, "Ожидалось наличие фамилии");
            Assert.IsNotEmpty(worker.Person.Passport, "Ожидалось наличие паспорта");
        }

        [Test]
        //3. Проверка записей с информацией о рабочих
        public void CheckWorkerNotes_Success()
        {
            //ARRANGE
            var workerIndex = 3;
            var expectedCountLowerBound = 5;

            //ACT
            var workers = WorkerRepo.GetAll().Include(w => w.Person).ToList();

            //ASSERT
            //1. Проверка коллекции
            Assert.IsNotNull(workers, "Коллекция не д.б. нулл.");
            Assert.IsNotEmpty(workers, "Коллекция не д.б. пустой.");
            Assert.Greater(workers.Count, expectedCountLowerBound, $"Ожидалось кол-во записей большее {expectedCountLowerBound}");

            //2. Проверка одной из сущностей коллекции
            Assert.IsNotNull(workers[workerIndex], "Запсиь не д.б. нулл.");
            Assert.IsNotNull(workers[workerIndex].Person, "Связанная запись персоны не д.б. нулл.");
            Assert.IsNotEmpty(workers[workerIndex].Person.Name, "Ожидалось наличие имени");
            Assert.IsNotEmpty(workers[workerIndex].Person.Patronymic, "Ожидалось наличие отчества");
            Assert.IsNotEmpty(workers[workerIndex].Person.Surname, "Ожидалось наличие фамилии");
            Assert.IsNotEmpty(workers[workerIndex].Person.Passport, "Ожидалось наличие паспорта");
        }

        [Test]
        //4. Проверка записей с информацией о рабочих без использования склеивания с персонами
        public void CheckWorkerNotesWithoutPersonInclude_Success()
        {
            //ARRANGE
            var workerIndex = 3;
            var expectedCountLowerBound = 5;
            var expectedSalaryMinBound = 5000;

            //ACT
            var workers = WorkerRepo.GetAll().ToList();

            //ASSERT
            //1. Проверка коллекции
            Assert.IsNotNull(workers, "Коллекция не д.б. нулл.");
            Assert.IsNotEmpty(workers, "Коллекция не д.б. пустой.");
            Assert.Greater(workers.Count, expectedCountLowerBound, $"Ожидалось кол-во записей большее {expectedCountLowerBound}");

            //2. Проверка одной из сущностей коллекции
            Assert.IsNotNull(workers[workerIndex], "Запись не д.б. нулл.");
            Assert.GreaterOrEqual(workers[workerIndex].Salary,expectedSalaryMinBound, $"Ожидалась зарплата большая, чем {expectedSalaryMinBound}");
        }

        [Test]
        //5. Проверка выборки записей с информацией о рабочих
        public void CheckWorkerNoteSelection_Success()
        {
            //ARRANGE
            var workerSalaryLowerBound = 5000;
            var workerSalaryTopBound = 9000;

            var workerIndex = 0;
            var workerCountLowerBound = 0;
            //ACT
            var workers = WorkerRepo.GetSome(w=>w.Salary< workerSalaryTopBound).Include(w=>w.Person).ToList();

            //ASSERT
            //1. Проверка коллекции
            Assert.IsNotNull(workers, "Коллекция не д.б. нулл.");
            Assert.IsNotEmpty(workers, "Коллекция не д.б. пустой.");
            Assert.Greater(workers.Count, workerCountLowerBound, $"Ожидалось кол-во записей большее {workerCountLowerBound}");

            //2. Проверка одной из сущностей коллекции
            Assert.IsNotNull(workers[workerIndex], "Запись не д.б. нулл.");
            Assert.IsNotNull(workers[workerIndex].Person, "Связанная запись персоны не д.б. нулл.");
            Assert.GreaterOrEqual(workers[workerIndex].Salary, workerSalaryLowerBound, $"Ожидалась зарплата большая, чем {workerSalaryLowerBound}");
            Assert.LessOrEqual(workers[workerIndex].Salary, workerSalaryTopBound, $"Ожидалась зарплата меньшая, чем {workerSalaryTopBound}");
        }

        [Test]
        //6. Проверка добавления записи о рабочем
        public void CheckWorkerNoteCreation_Success()
        {
            //ARRANGE
            //1. Данные рабочего 
            var name = "Василий";
            var patronymic = "Витальевич";
            var surname = "Соломкин";
            var passport = "TYUO21323432";
            var salary = 7000;
            //2. Новая персона, которую добавим в новую сущность рабочего
            var person = new Person
            {
                Name= name,
                Patronymic= patronymic,
                Surname= surname,
                Passport= passport
            };
            //3. Новая сущность рабочего
            var worker = new Worker
            {
                Person = person,
                Salary = salary
            };

            //ACT
            //1. Добавленная сущность рабочего
            //2. А тут смотрим и ищем, добавилась ли персона с добавлением рабочего
            var actualWorker = WorkerRepo.CreateOne(worker);
            var addedPerson = PersonRepo
                .GetSome(p => 
                p.Passport == passport 
                && p.Surname == surname 
                && p.Name == name 
                && p.Patronymic == patronymic)
                .FirstOrDefault();

            //ASSERT
            Assert.IsNotNull(actualWorker, "Запсиь не д.б. нулл.");
            Assert.IsNotNull(actualWorker.Person, "Связанная запись персоны не д.б. нулл.");
            Assert.IsNotNull(addedPerson, "Запись персоны не д.б. нулл. Видимо она не добавлась в бд.");
            Assert.IsNotEmpty(actualWorker.Person.Name, "Ожидалось наличие имени");
            Assert.IsNotEmpty(actualWorker.Person.Patronymic, "Ожидалось наличие отчества");
            Assert.IsNotEmpty(actualWorker.Person.Surname, "Ожидалось наличие фамилии");
            Assert.IsNotEmpty(actualWorker.Person.Passport, "Ожидалось наличие паспорта");
            Assert.AreEqual(actualWorker.Salary, salary, $"Ожидалась зарплата равная {salary}");
        }

        [Test]
        //7. Проверка обновления записей рабочих
        public void CheckWorkerNoteUpdating_Success()
        {
            //ARRANGE
            //1. Данные рабочего 
            var newSalary = 8001;
            var newName = "Карп";
            //2. Получить сущность для обновления
            var worker = WorkerRepo.GetAll().Include(w=>w.Person).OrderByDescending(w=>w.Id).FirstOrDefault();
            //3. Модифицировать
            //worker.Salary = newSalary;
            //worker.Person.Name = newName;

            //ACT
            //1. Измененная сущность рабочего
            //2. А тут смотрим и ищем, изменилась ли персона с изменением рабочего
            var updatedWorker = WorkerRepo.UpdateOne(worker.Id, w =>
            {
                w.Salary = newSalary;
                w.Person.Name = newName;
            });
            var updatedPerson = PersonRepo.GetOne(worker.Person.Id).FirstOrDefault();

            //ASSERT
            Assert.IsNotNull(updatedWorker, "Запсиь не д.б. нулл.");
            Assert.IsNotNull(updatedWorker.Person, "Связанная запись персоны не д.б. нулл.");
            Assert.IsNotNull(updatedPerson, "Запись персоны не д.б. нулл.");
            Assert.AreEqual(updatedPerson.Name, newName, $"Ожидалось имя персоны {newName}");
            Assert.IsNotEmpty(updatedWorker.Person.Name, "Ожидалось наличие имени");
            Assert.IsNotEmpty(updatedWorker.Person.Patronymic, "Ожидалось наличие отчества");
            Assert.IsNotEmpty(updatedWorker.Person.Surname, "Ожидалось наличие фамилии");
            Assert.IsNotEmpty(updatedWorker.Person.Passport, "Ожидалось наличие паспорта");
            Assert.AreEqual(updatedWorker.Salary, newSalary, $"Ожидалась зарплата равная {newSalary}");
        }

        [Test]
        //8. Проверка не безопасного удаления записи рабочего
        //(по идее д.б. быть провал, т.к. фактическим удалением является безопасное,
        //но это не так. Их работа внешне схожа)
        public void CheckWorkerNoteNotSafeDeleting_Success()
        {
            //ARRANGE
            //1. Получить сущность, которую будем удалять
            var worker = WorkerRepo.GetAll().Include(w=>w.Person).OrderByDescending(w => w.Id).FirstOrDefault();
            var entityId = worker.Id;

            //ACT
            //1. Удаленная сущность рабочего
            //2. Ищем этого рабочего в бд. Д.б. нулл
            //3. Ищем другим способом. Должны найти
            var deletedWorker = WorkerRepo.DeleteOne(entityId);
            var emptyWorker1 = WorkerRepo.GetOne(entityId).FirstOrDefault();
            var emptyWorker2 = WorkerRepo.GetOneEvenDeleted(entityId).FirstOrDefault();

            //ASSERT
            Assert.IsNotNull(deletedWorker, "Запсиь не д.б. нулл.");
            Assert.AreEqual(deletedWorker.Id,entityId, "Id удаляемой и удаленной записи должны совпадать");
            Assert.IsNotNull(deletedWorker.Person, "Связанная запись персоны не д.б. нулл.");
            Assert.IsNull(emptyWorker1, "В БД больше не должно было быть этой записи, т.к. ее удалили.");
            Assert.IsNotNull(emptyWorker2, "Эта запись должна была найтись со статусом \"Удалена\"");
            Assert.IsTrue(emptyWorker2.WasDeleted, "Ожидалось что запись будет удалена.");

            //END
            WorkerRepo.RestoreOne(entityId);
        }

        [Test]
        //9. Проверка безопасного удаления записи рабочего
        public void CheckWorkerNoteSafeDeleting_Success()
        {
            //ARRANGE
            //1. Получить сущность, которую будем удалять
            var worker = WorkerRepo.GetAll().OrderByDescending(w => w.Id).FirstOrDefault();
            var entityId = worker.Id;

            //ACT
            //1. Удаленная сущность рабочего
            //2. Ищем этого рабочего в бд. Д.б. нулл
            var deletedWorker = WorkerRepo.DeleteOne(entityId);
            var emptyWorker = WorkerRepo.GetOne(entityId).FirstOrDefault();

            //ASSERT
            Assert.IsNotNull(deletedWorker, "Запсиь не д.б. нулл.");
            Assert.AreEqual(deletedWorker.Id, entityId, "Id удаляемой и удаленной записи должны совпадать");
            Assert.IsNull(emptyWorker, "В БД больше не должно было быть этой записи, т.к. ее удалили.");

            //END
            WorkerRepo.RestoreOne(entityId);
        }
    }
}