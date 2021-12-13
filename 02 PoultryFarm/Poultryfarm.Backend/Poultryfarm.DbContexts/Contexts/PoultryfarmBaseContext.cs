using Microsoft.EntityFrameworkCore;
using Poultryfarm.ModelsAndUtils.Db.Models.Cells;
using Poultryfarm.ModelsAndUtils.Db.Models.Chickens;
using Poultryfarm.ModelsAndUtils.Db.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sidonia.db_contexts.Contexts
{
    public abstract class PoultryfarmBaseContext: DbContext
    {
        //СВОЙСТВА, таблицы 
        public virtual DbSet<Worker> Workers { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Workshop> Workshops { get; set; }
        public virtual DbSet<Row> Rows { get; set; }
        public virtual DbSet<Cell> Cells { get; set; }
        public virtual DbSet<Diet> Diets { get; set; }
        public virtual DbSet<Breed> Breeds { get; set; }
        public virtual DbSet<Chicken> Chickens { get; set; }

        //Строка соединения
        public string ConnString { get; set; }

        //КОНСТРУКТОР
        public PoultryfarmBaseContext(string connString)
        {
            ConnString = connString;
        }

        //МЕТОДЫ
        //1. сконфигурировать EntityFramework
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //1.
            base.OnConfiguring(optionsBuilder);

            //2. Строка соединения с базой данных
            optionsBuilder
                //.UseLazyLoadingProxies()
                .UseSqlServer(ConnString);

            //3. Пример строки соединения
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BooksDb;Trusted_Connection=True;");
        }

        //2. Обработчик события "Модель создана", тут м. реализовать FluentAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1. Задание связей между таблицами
            //modelBuilder.Entity<Group>().HasOne(e => e.TradeMark).WithMany(e => e.Groups);
            //modelBuilder.Entity<Group>().HasOne(e => e.Category).WithMany(e => e.Groups);
            //modelBuilder.Entity<Group>().HasOne(e => e.GeneralGroup).WithMany(e => e.SubGroups);
            //modelBuilder.Entity<ProductLine>().HasOne(e => e.Group).WithMany(e => e.ProductLines);
            //modelBuilder.Entity<Product>().HasOne(e => e.ProductLine).WithMany(e => e.Products);
            //2. Задание различных характеристик таблицам и их полям  с помощью Fluent Api:
            base.OnModelCreating(modelBuilder);
        }
    }
}
