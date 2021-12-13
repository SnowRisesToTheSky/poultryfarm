using Poultryfarm.ModelsAndUtils.Db.Models.Persons;
using sidonia.db_contexts.Wrappers;

namespace Poultryfarm.Backend.Repositories.Workers
{
    public class PersonRepository : ChunkRepository<Person>
    {
        //КОНСТРУКТОРА
        //1. Для создания подключения к стандартной базе (Это делается под капотом)
        public PersonRepository() : base()
        {

        }

        //2. Передача контекста бд через его обертку. Обертка нужна для
        //автоосвобождения ресурсов контекста
        public PersonRepository(ContextWrapper wrapper) : base(wrapper)
        {
        }

        //3. Это тоже служит для передачи контекста бд.
        public PersonRepository(DatabaseInteraction repo) : base(repo)
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
