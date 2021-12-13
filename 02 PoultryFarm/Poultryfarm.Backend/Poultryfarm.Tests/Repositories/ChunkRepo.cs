using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Poultryfarm.Backend.Repositories;
using Poultryfarm.Backend.Repositories.Workers;
using Poultryfarm.ModelsAndUtils.Db.Models.Persons;
using sidonia.db_contexts.Contexts;
using sidonia.db_contexts.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.Tests.Repositories
{
    public class ChunkRepo
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
        //1. Проверка метода вычисления кол-ва партий (среди имеющихся записей)
        public void CheckChunkCount_IsGreaterZero()
        {
            //ARRANGE
            var chunkSize = 3;
            var expectedChunkCountLowerBoundValue = 2;

            //ACT
            var count = WorkerRepo.GetChunkCountFromAll(chunkSize);

            //ASSERT
            Assert.GreaterOrEqual(count, expectedChunkCountLowerBoundValue, $"Ожидалось, что кол-во партий равно или превосходит {expectedChunkCountLowerBoundValue}");
        }

        [Test]
        //2. Проверка разбивки партий
        public void CheckChunkFromAll_Success()
        {
            //ARRANGE
            //1. Размер партий
            //2. Текущая партия
            var chunkSize = 3;
            var chunkIndex = 2;
            //3. Ожидаемое кол-во записей в партии
            //4. Индекс рассматриваемого рабочего
            var expectedChunkLength = 3;
            var workerIndex = 1;

            //ACT
            var chunk = WorkerRepo.GetChunkFromAll(chunkIndex, chunkSize).Include(w=>w.Person).ToList();

            //ASSERT
            //1. Проверка коллекции
            Assert.IsNotNull(chunk, "Коллекция не д.б. нулл.");
            Assert.IsNotEmpty(chunk, "Коллекция не д.б. пустой.");
            Assert.AreEqual(chunk.Count, expectedChunkLength, $"Ожидалось кол-во записей равное {expectedChunkLength}");
            //2. Проверка одной из сущностей коллекции
            Assert.IsNotNull(chunk[workerIndex], "Запсиь не д.б. нулл.");
            Assert.IsNotNull(chunk[workerIndex].Person, "Связанная запись персоны не д.б. нулл.");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Name, "Ожидалось наличие имени");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Patronymic, "Ожидалось наличие отчества");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Surname, "Ожидалось наличие фамилии");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Passport, "Ожидалось наличие паспорта");
        }

        [Test]
        //2. Проверка разбивки партий
        public void CheckChunkFromAllEvenDeleted_Success()
        {
            //ARRANGE
            //1. Размер партий
            //2. Текущая партия
            var chunkSize = 3;
            var chunkIndex = 0;
            //3. Ожидаемое кол-во записей в партии
            //4. Индекс рассматриваемого рабочего
            var expectedChunkLength = 3;
            var workerIndex = 0;

            //ACT
            //1. Удалить первого рабочего
            var firstWorkerId = WorkerRepo.GetAll().First().Id;
            WorkerRepo.DeleteOne(firstWorkerId);
            //2. Получить первую партию
            var chunk = WorkerRepo.GetChunkFromAllEvenDeleted(chunkIndex, chunkSize).Include(w=>w.Person).ToList();

            //ASSERT
            //1. Проверка коллекции
            Assert.IsNotNull(chunk, "Коллекция не д.б. нулл.");
            Assert.IsNotEmpty(chunk, "Коллекция не д.б. пустой.");
            Assert.AreEqual(chunk.Count, expectedChunkLength, $"Ожидалось кол-во записей равное {expectedChunkLength}");
            //2. Проверка одной из сущностей коллекции
            Assert.IsNotNull(chunk[workerIndex], "Запсиь не д.б. нулл.");
            Assert.IsNotNull(chunk[workerIndex].Person, "Связанная запись персоны не д.б. нулл.");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Name, "Ожидалось наличие имени");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Patronymic, "Ожидалось наличие отчества");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Surname, "Ожидалось наличие фамилии");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Passport, "Ожидалось наличие паспорта");
            Assert.IsTrue(chunk[workerIndex].WasDeleted, "Ожидалось что запись будет удалена.");

            //END
            WorkerRepo.RestoreOne(firstWorkerId);
        }

        [Test]
        //3. Проверка метода вычисления кол-ва партий по некоторой выборке
        public void CheckChunkCountFromSome_IsGreaterZero()
        {
            //ARRANGE
            //1. Размер партий
            //2. Минимальное ожидаемое кол-во партий
            var chunkSize = 3;
            var expectedChunkCountLowerBoundValue = 2;
            //3. Верхняя граница зарплаты
            var salaryTopBound = 10000;

            //ACT
            //1. Сначала делаем выборку рабочих
            //2. Потом разбиваем ее на партии
            var selection = WorkerRepo.GetSome(ent => ent.Salary < salaryTopBound);
            var count = ChunkRepository.GetChunkCountFromSome(selection, chunkSize);

            //ASSERT
            Assert.GreaterOrEqual(count, expectedChunkCountLowerBoundValue, $"Ожидалось, что кол-во партий равно или превосходит {expectedChunkCountLowerBoundValue}");
        }

        [Test]
        //4. Проверка разбивки партий над сущностями некоторой выборки
        public void CheckChunkFromSome_Success()
        {
            //ARRANGE
            //1. Размер партий
            //2. Текущая партия
            var chunkSize = 3;
            var chunkIndex = 0;
            //3. Ожидаемое кол-во записей в партии
            //4. Индекс рассматриваемого рабочего
            var expectedChunkLength = 3;
            var workerIndex = 1;
            //5. Верхняя граница зарплаты
            var salaryTopBound = 9000;

            //ACT
            //1. Сначала делаем выборку рабочих
            //2. Потом разбиваем ее на партии
            var selection = WorkerRepo.GetSome(ent => ent.Salary < salaryTopBound);
            var chunk = ChunkRepository.GetChunkFromSome(selection, chunkIndex, chunkSize).Include(w => w.Person).ToList();

            //ASSERT
            //1. Проверка коллекции
            Assert.IsNotNull(chunk, "Коллекция не д.б. нулл.");
            Assert.IsNotEmpty(chunk, "Коллекция не д.б. пустой.");
            Assert.AreEqual(chunk.Count, expectedChunkLength, $"Ожидалось кол-во записей равное {expectedChunkLength}");
            //2. Проверка одной из сущностей коллекции
            Assert.IsNotNull(chunk[workerIndex], "Запсиь не д.б. нулл.");
            Assert.IsNotNull(chunk[workerIndex].Person, "Связанная запись персоны не д.б. нулл.");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Name, "Ожидалось наличие имени");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Patronymic, "Ожидалось наличие отчества");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Surname, "Ожидалось наличие фамилии");
            Assert.IsNotEmpty(chunk[workerIndex].Person.Passport, "Ожидалось наличие паспорта");
        }
    }
}
