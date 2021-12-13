using NUnit.Framework;
using Poultryfarm.ModelsAndUtils.Db.Models.Cells;
using Poultryfarm.ModelsAndUtils.Db.Models.Chickens;
using Poultryfarm.ModelsAndUtils.Db.Models.Persons;
using sidonia.models_and_secondary.Secondary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.Tests.Db
{
    public class DataGenerators
    {
        //СВОЙСТВА
        public static List<Person> Persons { get; set; } = new List<Person>();
        public static List<Worker> Workers { get; set; } = new List<Worker>();
        public static List<Cell> Cells { get; set; } = new List<Cell>();
        public static List<Row> Rows { get; set; } = new List<Row>();
        public static List<Workshop> Workshops { get; set; } = new List<Workshop>();
        public static List<Diet> Diets { get; set; } = new List<Diet>();
        public static List<Breed> Breeds { get; set; } = new List<Breed>();
        public static List<Chicken> Chickens { get; set; } = new List<Chicken>();

        //КОНСТАНТЫ
        public static readonly int PERSON_COUNT = 25;
        public static readonly int WORKSHOP_COUNT = 5;
        public static readonly int ROW_COUNT = 50;
        public static readonly int CELL_COUNT = 1000;
        public static readonly int DIET_COUNT = 4;
        public static readonly int BREED_COUNT = 4;
        public static readonly int CHICKEN_COUNT = 700;


        //ПРЕДНАСТРОЙКИ
        [OneTimeSetUp]
        public void Setup()
        {
        }

        //ТЕСТЫ
        [Test, Order(1)]
        //1. Проверка генерации записей с информацией о рабочих
        public void CheckWorkerGenerators_Success()
        {
            //ARRANGE
            //1. Индекс рассматриваемой персоны
            var personIndex = 3;
            var minId = 0;

            //ACT
            Persons.AddRange(DbModelGenerationUtils.genPersons(PERSON_COUNT));
            Workers.AddRange(DbModelGenerationUtils.genWorkers(Persons));

            //ASSERT
            Assert.AreEqual(PERSON_COUNT, Persons.Count, $"Кол-во персон д.б. быть {PERSON_COUNT}");
            Assert.AreEqual(PERSON_COUNT, Workers.Count, $"Кол-во рабочих д.б. быть {PERSON_COUNT}");
            Assert.IsNotNull(Persons[personIndex], "Персоны не д.б. нулл");
            Assert.IsNotNull(Workers[personIndex], "Рабочие не д.б. нулл");
            //Это же не данные бд, поэтому тут будет провал
            //Assert.Greater(Persons[personIndex].Id,minId, "Ид персоны д.б. больше нуля");
            //Assert.Greater(Workers[personIndex].Id,minId, "Ид рабочего д.б. больше нуля");
        }

        [Test, Order(2)]
        //2. Проверка генерации записей с информацией о рабочих
        public void CheckWorkshopGenerators_Success()
        {
            //ARRANGE
            //1. Индекс рассматриваемого цеха
            //2. Индекс рассматриваемого ряда
            //3. Индекс рассматриваемой клетки
            var workshopIndex = 3;
            var rowIndex = 3;
            var cellIndex = 3;
            //4. Мин граница ид
            var minId = 0;

            //ACT
            Workshops.AddRange(DbModelGenerationUtils.genWorkshops(WORKSHOP_COUNT));
            Rows.AddRange(DbModelGenerationUtils.genRows(ROW_COUNT, Workers.ToArray(), Workshops.ToArray()));
            Cells.AddRange(DbModelGenerationUtils.genCells(CELL_COUNT, Rows.ToArray()));

            //ASSERT
            Assert.AreEqual(WORKSHOP_COUNT, Workshops.Count, $"Кол-во цехов д.б. быть {WORKSHOP_COUNT}");
            Assert.AreEqual(ROW_COUNT, Rows.Count, $"Кол-во рядов д.б. быть {ROW_COUNT}");
            Assert.AreEqual(CELL_COUNT, Cells.Count, $"Кол-во клеток д.б. быть {CELL_COUNT}");
            Assert.IsNotNull(Workshops[workshopIndex], "Цеха не д.б. нулл");
            Assert.IsNotNull(Rows[rowIndex], "Ряды не д.б. нулл");
            Assert.IsNotNull(Cells[cellIndex], "Клетки не д.б. нулл");
            //Это же не данные бд, поэтому тут будет провал
            //Assert.Greater(Workshops[workshopIndex].Id, minId, "Ид цеха д.б. больше нуля");
            //Assert.Greater(Rows[rowIndex].Id, minId, "Ид ряда д.б. больше нуля");
            //Assert.Greater(Cells[cellIndex].Id, minId, "Ид клетки д.б. больше нуля");
        }

        [Test, Order(3)]
        //1. Проверка генерации записей с информацией о курицах
        public void CheckChickenGenerators_Success()
        {
            //ARRANGE
            //1. Индекс рассматриваемой диеты
            //2. Индекс рассматриваемой породы
            //3. Индекс рассматриваемой курицы
            var dietIndex = 3;
            var breedIndex = 3;
            var chickenIndex = 3;
            //4. Мин граница ид
            var minId = 0;

            //ACT
            Diets.AddRange(DbModelGenerationUtils.genDiets(DIET_COUNT));
            Breeds.AddRange(DbModelGenerationUtils.genBreeds(BREED_COUNT, Diets.ToArray()));
            Chickens.AddRange(DbModelGenerationUtils.genChickens(CHICKEN_COUNT, Breeds.ToArray(), Cells.ToArray()));

            //ASSERT
            Assert.AreEqual(DIET_COUNT, Diets.Count, $"Кол-во диет д.б. быть {DIET_COUNT}");
            Assert.AreEqual(BREED_COUNT, Breeds.Count, $"Кол-во пород д.б. быть {BREED_COUNT}");
            Assert.AreEqual(CHICKEN_COUNT, Chickens.Count, $"Кол-во куриц д.б. быть {CHICKEN_COUNT}");
            Assert.IsNotNull(Diets[dietIndex], "Диеты не д.б. нулл");
            Assert.IsNotNull(Breeds[breedIndex], "Породы не д.б. нулл");
            Assert.IsNotNull(Chickens[chickenIndex], "Курицы не д.б. нулл");
            //Это же не данные бд, поэтому тут будет провал
            //Assert.Greater(Diets[dietIndex].Id, minId, "Ид диеты д.б. больше нуля");
            //Assert.Greater(Breeds[breedIndex].Id, minId, "Ид породы д.б. больше нуля");
            //Assert.Greater(Chickens[chickenIndex].Id, minId, "Ид курицы д.б. больше нуля");
        }
    }
}
