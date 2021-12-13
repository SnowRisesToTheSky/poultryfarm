using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sidonia.db_contexts.Contexts
{
    public class PoultryfarmGenTestingContext : PoultryfarmBaseContext
    {

        //КОНСТРУКТОР
        public PoultryfarmGenTestingContext() : base("Data Source=(localdb)\\mssqllocaldb;Database=PoultryfarmDb.Dev;Trusted_Connection=True;")
        {
            Database.EnsureCreated();
        }

        //МЕТОДЫ
        //1. Обработчик события "Модель создана", тут м. реализовать FluentAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Задание различных характеристик таблицам и их полям  с помощью Fluent Api:
            base.OnModelCreating(modelBuilder);
        }
    }
}
