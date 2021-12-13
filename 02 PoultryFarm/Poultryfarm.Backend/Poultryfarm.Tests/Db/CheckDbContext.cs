using NUnit.Framework;
using sidonia.db_contexts.Contexts;
using Poultryfarm.DbInit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.Tests.Db
{
    public class CheckDbContext
    {
        public PoultryfarmGenTestingContext context { get; set; }

        //ПРЕДНАСТРОЙКИ
        [OneTimeSetUp]
        public void Setup()
        {
            context = new PoultryfarmGenTestingContext();
        }

        //ТЕСТЫ
        [Test, Order(1)]
        //1. Проверка записей таблиц рабочих
        public void CheckWorkerTables_Success()
        {
            //ARRANGE
            //Мин. граница ид
            var minId = 0;

            //ACT
            //Достаем записи из бд для проверки
            var persons = context.Persons;
            var workers = context.Workers;

            //ASSERT
            //Проверяем что их больше или равно столько сколько добавили при генерации
            Assert.GreaterOrEqual(persons.Count(), DbInitUtils.PERSON_COUNT, $"Кол-во персон д.б. больше или равным {DbInitUtils.PERSON_COUNT}");
            Assert.GreaterOrEqual(workers.Count(), DbInitUtils.PERSON_COUNT, $"Кол-во рабочих д.б. больше или равным {DbInitUtils.PERSON_COUNT}");
            //Проверяем что они не нулл
            Assert.IsNotNull(persons.FirstOrDefault(),"Персоны не д.б. нулл");   
            Assert.IsNotNull(workers.FirstOrDefault(),"Рабочиен не д.б. нулл");   
            //Проверяем их ид
            Assert.Greater(persons.First().Id, minId, $"Ид персоны д.б. больше нуля");
            Assert.Greater(workers.First().Id, minId, $"Ид рабочего д.б. больше нуля");
        }

        [Test, Order(2)]
        //2. Проверка записей таблиц цехов
        public void CheckWorkshopTables_Success()
        {
            //ARRANGE
            //Мин. граница ид
            var minId = 0;

            //ACT
            //Достаем записи из бд для проверки
            var workshops = context.Workshops;
            var rows = context.Rows;
            var cells = context.Cells;

            //ASSERT
            //Проверяем что их больше или равно столько сколько добавили при генерации
            Assert.GreaterOrEqual(workshops.Count(), DbInitUtils.WORKSHOP_COUNT, $"Кол-во цехов д.б. больше или равным {DbInitUtils.WORKSHOP_COUNT}");
            Assert.GreaterOrEqual(rows.Count(), DbInitUtils.ROW_COUNT, $"Кол-во рядов д.б. больше или равным {DbInitUtils.ROW_COUNT}");
            Assert.GreaterOrEqual(cells.Count(), DbInitUtils.CELL_COUNT, $"Кол-во клеток д.б. больше или равным {DbInitUtils.CELL_COUNT}");
            //Проверяем что они не нулл
            Assert.IsNotNull(workshops.FirstOrDefault(), "Цеха не д.б. нулл");
            Assert.IsNotNull(rows.FirstOrDefault(), "Ряды не д.б. нулл");
            Assert.IsNotNull(cells.FirstOrDefault(), "Клетки не д.б. нулл");
            //Проверяем их ид
            Assert.Greater(workshops.First().Id, minId, $"Ид цеха д.б. больше нуля");
            Assert.Greater(rows.First().Id, minId, $"Ид ряда д.б. больше нуля");
            Assert.Greater(cells.First().Id, minId, $"Ид клетки д.б. больше нуля");
            //Проверить что не все клетки содержат курицу
            Assert.IsTrue(cells.Any(cell => cell.Chicken == null),"Ожидалось что не все клетки содержат курицу. Получилось иначе.");
        }

        [Test, Order(3)]
        //3. Проверка записей таблиц куриц
        public void CheckChickenTables_Success()
        {
            //ARRANGE
            //Мин. граница ид
            var minId = 0;

            //ACT
            //Достаем записи из бд для проверки
            var diets = context.Diets;
            var breeds = context.Breeds;
            var chickens = context.Chickens;

            //ASSERT
            //Проверяем что их больше или равно столько сколько добавили при генерации
            Assert.GreaterOrEqual(diets.Count(), DbInitUtils.DIET_COUNT, $"Кол-во диет д.б. больше или равным {DbInitUtils.DIET_COUNT}");
            Assert.GreaterOrEqual(breeds.Count(), DbInitUtils.BREED_COUNT, $"Кол-во пород д.б. больше или равным {DbInitUtils.BREED_COUNT}");
            Assert.GreaterOrEqual(chickens.Count(), DbInitUtils.CHICKEN_COUNT, $"Кол-во куриц д.б. больше или равным {DbInitUtils.CHICKEN_COUNT}");
            //Проверяем что они не нулл
            Assert.IsNotNull(diets.FirstOrDefault(), "Диеты не д.б. нулл");
            Assert.IsNotNull(breeds.FirstOrDefault(), "Породы не д.б. нулл");
            Assert.IsNotNull(chickens.FirstOrDefault(), "Курицы не д.б. нулл");
            //Проверяем их ид
            Assert.Greater(diets.First().Id, minId, $"Ид диеты д.б. больше нуля");
            Assert.Greater(breeds.First().Id, minId, $"Ид породы д.б. больше нуля");
            Assert.Greater(chickens.First().Id, minId, $"Ид курицы д.б. больше нуля");
        }
    }
}
