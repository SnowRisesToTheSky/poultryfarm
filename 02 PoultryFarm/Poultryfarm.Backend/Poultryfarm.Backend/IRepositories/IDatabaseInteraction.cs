using sidonia.db_contexts.Contexts;
using sidonia.db_contexts.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poultryfarm.Backend.IRepositories
{
    //Интерфейс для класса взаимодействия с базой данных. Этот класс -
    //базовый класс для всех моих репозиториев
    public interface IDatabaseInteraction : IDisposable
    {
        void SaveChanges();
        ContextWrapper Wrapper { get; set; }
        PoultryfarmBaseContext Context { get; }
    }
}
