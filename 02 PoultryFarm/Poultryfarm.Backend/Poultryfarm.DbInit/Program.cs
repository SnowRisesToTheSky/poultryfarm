using Poultryfarm.ModelsAndUtils.Db.Models.Cells;
using Poultryfarm.ModelsAndUtils.Db.Models.Chickens;
using Poultryfarm.ModelsAndUtils.Db.Models.Persons;
using sidonia.db_contexts.Contexts;
using sidonia.models_and_secondary.Secondary.Utils;
using System;
using System.Collections.Generic;

namespace Poultryfarm.DbInit
{
    public class Program
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

        //ТОЧКА ВХОДА В ПРИЛОЖЕНИЕ
        static void Main(string[] args)
        {
            PoultryfarmGenContext context = new PoultryfarmGenContext("Data Source=(localdb)\\mssqllocaldb;Database=PoultryfarmDb.Dev;Trusted_Connection=True;");
            //Генерация и связывание моделей
            InitializeProps();
            //Занесение их в бд
            InitializeDb(context);
            context.SaveChanges();
        }

        //ГЕНЕРАЦИЯ СУЩНОСТЕЙ
        static void InitializeProps()
        {


            //ГЕНЕРАЦИЯ
            //1. Персон и их сущностей рабочих
            Persons.AddRange(DbModelGenerationUtils.genPersons(DbInitUtils.PERSON_COUNT));
            Workers.AddRange(DbModelGenerationUtils.genWorkers(Persons));

            //2. Цехов, рядов, клеток
            Workshops.AddRange(DbModelGenerationUtils.genWorkshops(DbInitUtils.WORKSHOP_COUNT));
            Rows.AddRange(DbModelGenerationUtils.genRows(DbInitUtils.ROW_COUNT, Workers.ToArray(),Workshops.ToArray()));
            Cells.AddRange(DbModelGenerationUtils.genCells(DbInitUtils.CELL_COUNT, Rows.ToArray()));

            //3. Диет, пород, куриц
            Diets.AddRange(DbModelGenerationUtils.genDiets(DbInitUtils.DIET_COUNT));
            Breeds.AddRange(DbModelGenerationUtils.genBreeds(DbInitUtils.BREED_COUNT, Diets.ToArray()));
            Chickens.AddRange(DbModelGenerationUtils.genChickens(DbInitUtils.CHICKEN_COUNT, Breeds.ToArray(),Cells.ToArray()));

        }
        static void InitializeDb(PoultryfarmGenContext context)
        {
            context.Persons.AddRange(Persons);
            context.Workers.AddRange(Workers);
            context.Workshops.AddRange(Workshops);
            context.Rows.AddRange(Rows);
            context.Cells.AddRange(Cells);
            context.Diets.AddRange(Diets);
            context.Breeds.AddRange(Breeds);
            context.Chickens.AddRange(Chickens);
        }
    }
}
