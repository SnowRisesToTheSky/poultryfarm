using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sidonia.db_contexts.Contexts
{
    public class PoultryfarmGenContext : PoultryfarmBaseContext
    {

        //КОНСТРУКТОР
        public PoultryfarmGenContext(string connString):base("Data Source=(localdb)\\mssqllocaldb;Database=PoultryfarmDb.Dev;Trusted_Connection=True;")
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        //МЕТОДЫ
        //1. Обработчик события "Модель создана", тут м. реализовать FluentAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1. Задание различных характеристик таблицам и их полям  с помощью Fluent Api:
            base.OnModelCreating(modelBuilder);
            //2. Инициализация модели данными
            //modelBuilder.Entity<TradeMark>().HasData(ProductDataInitializer.TradeMarks);
            //modelBuilder.Entity<Category>().HasData(ProductDataInitializer.Categories);
            //modelBuilder.Entity<Group>().HasData(ProductDataInitializer.Groups);
            //modelBuilder.Entity<ProductLine>().HasData(ProductDataInitializer.ProductLines);
            //modelBuilder.Entity<Product>().HasData(ProductDataInitializer.Products);

        }
    }
}
