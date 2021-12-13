using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sidonia.db_contexts.Contexts
{
    public class PoultryfarmTestingContext : PoultryfarmBaseContext
    {

        //КОНСТРУКТОР
        public PoultryfarmTestingContext() : base("Data Source=(localdb)\\mssqllocaldb;Database=PoultryfarmDb.Test;Trusted_Connection=True;")
        {
            Database.EnsureDeleted();
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
