using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sidonia.db_contexts.Contexts;
using sidonia.db_contexts.Wrappers;
using Poultryfarm.ModelsAndUtils.Db.Models.Persons;

namespace Poultryfarm.Backend.Repositories.Workers
{
    public class WorkerRepository : ChunkRepository<Worker>
    {

        //КОНСТРУКТОРА
        //1. Для создания подключения к стандартной базе (Это делается под капотом)
        public WorkerRepository() : base()
        {

        }

        //2. Передача контекста бд через его обертку. Обертка нужна для
        //автоосвобождения ресурсов контекста
        public WorkerRepository(ContextWrapper wrapper) : base(wrapper)
        {
        }

        //3. Это тоже служит для передачи контекста бд.
        public WorkerRepository(DatabaseInteraction repo) : base(repo)
        {

        }

        //МЕТОДЫ ДОСТУПА
        //1. Кол-во товаров, сгруппированных по торговым маркам
        //public IEnumerable<dynamic> GetSomeTradeMarkWithProductCount(IEnumerable<Worker> workers)
        //{
        //    return null;
        //}
    }
}
