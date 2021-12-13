using sidonia.db_contexts.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sidonia.db_contexts.Wrappers;
using Poultryfarm.Backend.IRepositories;

namespace Poultryfarm.Backend.Repositories
{
    //Абстрактный класс взаимодействия с базой данных. Это базовый класс
    //для всех моих репозиториев
    public abstract class DatabaseInteraction : IDatabaseInteraction
    {
        //СВОЙСТВА
        //1. Обертка над контекстом бд
        public ContextWrapper Wrapper { get; set; }

        //2. Контекст бд для взаимодействия с ней
        public PoultryfarmBaseContext Context { get => Wrapper.Context; }




        //КОНСТРУКТОРА
        //1. Для создания подключения к стандартной базе
        public DatabaseInteraction()
        {
            this.Wrapper = new ContextWrapper(new PoultryfarmContext());
        }

        public DatabaseInteraction(ContextWrapper wrapper)
        {
            this.Wrapper = wrapper;
        }

        //2. Для случаев, когда нужно повторно использовать один контекст.
        // В этом случае контекст получен извне, из другого репозитория и
        // значит в этом репозитории контекст освобождать не нужно. Он ос-
        // вободится в его первом репозитории
        public DatabaseInteraction(DatabaseInteraction obj)
        {
            this.Wrapper = obj.Wrapper;
        }




        //РЕАЛИЗАЦИЯ МЕТОДОВ
        //1. Сохранить результат CRUD - операций
        public void SaveChanges()
        {
            Wrapper.SaveChanges();
        }

        //2. Освободить дескриптор бд и сам репозиторий
        public void Dispose()
        {
            //1. Освобождаем ресурсы контекста бд
            Wrapper.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
