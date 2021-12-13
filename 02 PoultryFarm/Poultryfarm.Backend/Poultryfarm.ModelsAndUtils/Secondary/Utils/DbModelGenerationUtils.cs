using Poultryfarm.ModelsAndUtils.Db.Models.Cells;
using Poultryfarm.ModelsAndUtils.Db.Models.Chickens;
using Poultryfarm.ModelsAndUtils.Db.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sidonia.models_and_secondary.Secondary.Utils
{
    //Генератор моделей бд
    public static class DbModelGenerationUtils
    {
        //1. Сгенерировать персону
        public static Person genPerson()
        {
            //1. Сгенерировать поля
            var name = PersonUtils.generateName();
            var patronymic = PersonUtils.generatePatronymic();
            var surname = PersonUtils.generateSurname();
            var passport = PersonUtils.generatePassport();
            //2. Сгенерировать сущность
            var person = new Person
            {
                Name = name,
                Patronymic = patronymic,
                Surname = surname,
                Passport = passport
            };
            //3. Ответ
            return person;
        }

        //2. Сгенерировать персоны
        public static Person[] genPersons(int count)
        {
            List<Person> persons = new List<Person>();
            //Генерация персон
            for (var i = 0; i < count; ++i)
            {
                persons.Add(genPerson());
            }
            return persons.ToArray();
        }

        //3. Сгенерировать рабочего
        public static Worker genWorker(Person person)
        {
            var worker = new Worker
            {
                Person = person,
                Salary = SimpleUtils.generateInteger(5000, 24000)
            };
            //Ответ
            return worker;
        }

        //4. Сгенерировать рабочих
        public static Worker[] genWorkers(List<Person> persons)
        {
            var workers = new List<Worker>();
            //Генерация сущностей
            for (var i = 0; i < persons.Count; ++i)
            {
                workers.Add(genWorker(persons[i]));
            }
            return workers.ToArray();
        }

        //5. Сгенерировать цех
        public static Workshop genWorkshop()
        {
            var worker = new Workshop
            {
                Number = TextUtils.generateWord()
            };
            //Ответ
            return worker;
        }

        //6. Сгенерировать цеха
        public static Workshop[] genWorkshops(int count)
        {
            var workshops = new List<Workshop>();
            //Генерация сущностей
            for (var i = 0; i < count; ++i)
            {
                workshops.Add(genWorkshop());
            }
            return workshops.ToArray();
        }

        //7. Сгенерировать ряд
        public static Row genRow(Worker[] workers, Workshop[] workshops)
        {
            var row = new Row
            {
                Number = SimpleUtils.generateInteger(1, 17),
                Worker = SimpleUtils.randomChoose(workers),
                Workshop = SimpleUtils.randomChoose(workshops)
            };
            //Ответ
            return row;
        }

        //8. Сгенерировать ряды
        public static Row[] genRows(int count, Worker[] workers, Workshop[] workshops)
        {
            var rows = new List<Row>();
            //Генерация сущностей
            for (var i = 0; i < count; ++i)
            {
                rows.Add(genRow(workers, workshops));
            }
            return rows.ToArray();
        }

        //9. Сгенерировать клетку
        public static Cell genCell(Row[] rows)
        {
            var cell = new Cell
            {
                Number = SimpleUtils.generateInteger(1, 41),
                Row = SimpleUtils.randomChoose(rows)
            };
            //Ответ
            return cell;
        }

        //10. Сгенерировать клетки
        public static Cell[] genCells(int count, Row[] rows)
        {
            var cells = new List<Cell>();
            //Генерация сущностей
            for (var i = 0; i < count; ++i)
            {
                cells.Add(genCell(rows));
            }
            return cells.ToArray();
        }

        //11. Сгенерировать диету
        public static Diet genDiet()
        {
            var diet = new Diet
            {
                Name = TextUtils.generateWord()
            };
            //Ответ
            return diet;
        }

        //12. Сгенерировать диеты
        public static Diet[] genDiets(int count)
        {
            var diets = new List<Diet>();
            //Генерация сущностей
            for (var i = 0; i < count; ++i)
            {
                diets.Add(genDiet());
            }
            return diets.ToArray();
        }

        //13. Сгенерировать породу
        public static Breed genBreed(Diet[] diets)
        {
            var breed = new Breed
            {
                Name = TextUtils.generateWord(),
                Diet = SimpleUtils.randomChoose(diets),
                AvgWeight = SimpleUtils.generateFloat(500, 700),
                AvgEggCountPerMonth= SimpleUtils.generateFloat(30,100),
            };
            //Ответ
            return breed;
        }

        //14. Сгенерировать породы
        public static Breed[] genBreeds(int count, Diet[] diets)
        {
            var breed = new List<Breed>();
            //Генерация сущностей
            for (var i = 0; i < count; ++i)
            {
                breed.Add(genBreed(diets));
            }
            return breed.ToArray();
        }

        //15. Сгенерировать курицу
        public static Chicken genChicken(Breed[] breeds,Cell cell)
        {
            //1. Выбираем породу
            var breed = SimpleUtils.randomChoose(breeds);

            //2. Генерируем курицу
            var chicken = new Chicken
            {
                Age = SimpleUtils.generateFloat(0.5, 4),
                Weight = SimpleUtils.generateFloat(breed.AvgWeight-100, breed.AvgWeight + 100),
                AvgEggCountPerMonth = SimpleUtils.generateFloat(breed.AvgEggCountPerMonth-10, breed.AvgEggCountPerMonth + 10),
                Breed = breed,
                Cell = cell
            };
            //3. Ответ
            return chicken;
        }

        //16. Сгенерировать куриц
        public static Chicken[] genChickens(int count, Breed[] breeds, Cell[] cells)
        {
            var chickens = new List<Chicken>();

            //Копия списка клеток, которая будет сокращаться по мере генерации куриц
            var cellLst = new List<Cell>(cells);
            //cellLst.AddRange(cells);

            //Генерация сущностей
            for (var i = 0; i < count; ++i)
            {
                //1. Выбираем клетку с ее изъятием из списка
                var cell = SimpleUtils.randomChoose(cellLst.ToArray());
                cellLst.Remove(cell);
                //2. Генерируем курицу
                chickens.Add(genChicken(breeds, cell));
            }
            return chickens.ToArray();
        }
    }
}
