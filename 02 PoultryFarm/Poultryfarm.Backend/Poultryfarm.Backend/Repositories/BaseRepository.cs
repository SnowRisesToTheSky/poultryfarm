using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sidonia.db_contexts.Wrappers;
using Poultryfarm.Backend.IRepositories;
using Poultryfarm.ModelsAndUtils.Db.Models.Persons;
using Poultryfarm.ModelsAndUtils.Db.Models;
using System.Linq.Expressions;

namespace Poultryfarm.Backend.Repositories
{
    //Абстрактный класс. Содержит базовый функционал для всех репозиториев. Это
    //базовый класс для всех репозиториев
    public abstract class BaseRepository<T> : DatabaseInteraction//, IRepository<T>
        where T : BaseEntity
    {
        //КОНСТРУКТОРА
        //1. Для создания подключения к стандартной базе (Это делается под капотом)
        public BaseRepository() : base()
        {

        }

        //2. Передача контекста бд через его обертку. Обертка нужна для
        //автоосвобождения ресурсов контекста
        public BaseRepository(ContextWrapper wrapper) : base(wrapper)
        {
        }

        //3. Это тоже служит для передачи контекста бд.
        public BaseRepository(DatabaseInteraction repo) : base(repo)
        {

        }



        //CRUD - ОПЕРАЦИИ
        //1. Получить все записи
        public IQueryable<T> GetAll()
        {
            return Context.Set<T>().Where(ent => !ent.WasDeleted);
        }

        //2. Получить некоторые записи
        public IQueryable<T> GetSome(Expression<Func<T,bool>> predicate)
        {
            return Context.Set<T>().Where(ent => !ent.WasDeleted).Where(predicate);
        }

        //3. Получить одну запись
        public IQueryable<T> GetOne(int? id)
        {
            //1. C жадной подгрузкой
            //return Context.Set<Worker>().Include(w=>w.Person).Where(w=>w.Id==id);
            //2. Без подгрузки
            return Context.Set<T>().Where(ent=>!ent.WasDeleted).Where(w=>w.Id==id);
        }

        //4. Получить одну запись, даже удаленную
        public IQueryable<T> GetOneEvenDeleted(int? id)
        {
            //1. C жадной подгрузкой
            //return Context.Set<Worker>().Include(w=>w.Person).Where(w=>w.Id==id);
            //2. Без подгрузки
            return Context.Set<T>().Where(w => w.Id == id);
        }

        //5. Добавить одну запись
        public T CreateOne(T item)
        {
            //1. Запись которую нужно вернуть
            T result = null;
            //2. Если запись которую добавляем не нулл
            if (item != null)
            {
                //3. То добавляем ее и получаем рез-тат (та-же запись)
                //4. Сохраняем состояние бд
                result = Context.Set<T>().Add(item).Entity;
                SaveChanges();
            }
            //3. Если все прошло успешно - то вернем запись которую добавили
            return result;
        }

        //6. Изменить одну запись
        public T UpdateOne(int? id, Action<T> action)
        {
            //1. Запись которую нужно обновить
            var record = Context.Set<T>().Where(ent => !ent.WasDeleted).Where(ent => ent.Id == id).FirstOrDefault();
            if (record!=null)
            {
                //1. Обновляем ее
                //2. Сохраняем изменения
                action(record);
                SaveChanges();
            }
            //2. Если все прошло успешно - то вернем запись которую обновили
            return record;
        }

        //7. Удалить одну запись
        public T DeleteOne(int? id)
        {
            //1. Ищем запись для удаления
            var item = Context.Set<T>().Where(ent => !ent.WasDeleted).Where(ent => ent.Id==id).FirstOrDefault();
            //2. Если нашли
            if (item != null)
            {
                //3. То удаляем (мягкое удаление)
                //4. Сохраняем изменения
                item.WasDeleted = true;
                SaveChanges();
            }
            //3. Рез-тат
            return item;
        }

        //РАБОТА С УДАЛЕННЫМИ ЗАПИСЯМИ
        //1. Получить все записи, даже удаленные
        public IQueryable<T> GetAllEvenDeleted()
        {
            return Context.Set<T>();
        }

        //2. Получить некоторые записи, в т.ч. удаленные
        public IQueryable<T> GetSomeEvenDeleted(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        //3. Восстановить запись
        public T RestoreOne(int? id)
        {
            //1. Ищем запись для восстановления
            var item = Context.Set<T>().Where(ent => ent.WasDeleted).Where(ent => ent.Id == id).FirstOrDefault();
            //2. Если нашли
            if (item != null)
            {
                //3. То удаляем (мягкое удаление)
                //4. Сохраняем изменения
                item.WasDeleted = false;
                SaveChanges();
            }
            //3. Рез-тат
            return item;
        }

        //ДРУГИЕ ОПЕРАЦИИ
        //1. Получить кол-во записей
        public int GetCount()
        {
            return Context.Set<T>().Where(ent=>!ent.WasDeleted).Count();
        }
    }
}
