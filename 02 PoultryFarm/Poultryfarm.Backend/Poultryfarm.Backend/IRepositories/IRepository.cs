using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Poultryfarm.Backend.IRepositories
{
    //Базовый интерфейс для многих производных интерфейсов репозиториев.
    //Для одного репозитория предположительно м.б. использовано несколько интерфейсов.
    //Но такого типа м.б. прикручен, к репозиторию, предположительно только 1 интерфейс.

    public interface IRepository<T> where T: class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetSome(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetOne(int? id);
        T CreateOne(T item);
        T UpdateOne(T item);
        T DeleteOne(int? id);
        //Получить кол-во записей
        int GetCount();

        //1. Получить все записи, даже удаленные
        //2. Получить некоторые записи, в т.ч. удаленные
        //3. Восстановить запись
        public IQueryable<T> GetAllEvenDeleted();
        public IQueryable<T> GetSomeEvenDeleted(Expression<Func<T, bool>> predicate);
        public T RestoreOne(int? id);
    }
}
