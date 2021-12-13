using Microsoft.AspNetCore.Mvc;
using Poultryfarm.Backend.Repositories;
using Poultryfarm.ModelsAndUtils.Db.Models;
using System;

namespace sidonia.backend.Controllers
{
    public abstract class BaseControllerA<T> : ControllerBase, IDisposable
    where T : BaseEntity
    {
        //СВОЙСТВА
        //Репозиторий для доступа к записям таблицы
        //(содержит ссылку на контекст бд)
        protected ChunkRepository<T> repo { get; set; }



        //КОНСТРУКТОРА (в контроллере м.б. только один)
        public BaseControllerA(ChunkRepository<T> repo)
        {
            //Инициализируем репозиторий для контроллера
            this.repo = repo;
        }

        //ДРУГИЕ МЕТОДЫ
        void IDisposable.Dispose()
        {
            repo.Dispose();
        }
    }
}
