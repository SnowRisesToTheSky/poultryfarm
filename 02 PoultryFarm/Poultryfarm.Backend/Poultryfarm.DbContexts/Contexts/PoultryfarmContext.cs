using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace sidonia.db_contexts.Contexts
{
    public class PoultryfarmContext:PoultryfarmBaseContext
    {

        //КОНСТРУКТОР
        //Репозиторий для главного проекта. Экземпляр создается в базовых сущностях реозиториев: DatabaseInteraction
        public PoultryfarmContext():base("Data Source=(localdb)\\mssqllocaldb;Database=PoultryfarmDb.Dev;Trusted_Connection=True;")
        {
            Database.EnsureCreated();
        }
    }
}
