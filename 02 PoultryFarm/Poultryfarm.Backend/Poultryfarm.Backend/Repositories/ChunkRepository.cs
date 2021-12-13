using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sidonia.db_contexts.Wrappers;
using Poultryfarm.ModelsAndUtils.Db.Models;

namespace Poultryfarm.Backend.Repositories
{
    public abstract class ChunkRepository<T>:BaseRepository<T>
        where T:BaseEntity
    {
        //КОНСТРУКТОРА
        //1. Для создания подключения к стандартной базе (Это делается под капотом)
        public ChunkRepository() : base()
        {

        }

        //2. Передача контекста бд через его обертку. Обертка нужна для
        //автоосвобождения ресурсов контекста
        public ChunkRepository(ContextWrapper wrapper) : base(wrapper)
        {
        }

        //3. Это тоже служит для передачи контекста бд.
        public ChunkRepository(DatabaseInteraction repo) : base(repo)
        {

        }

        //МЕТОДЫ ДОСТУПА
        //1. Получить все записи партии (среди имеющихся)
        public IQueryable<T> GetChunkFromAll(int? curChunkIndex, int? chunkSize)
        {
            //1. Хранилище ответа
            IQueryable<T> result = null;
            //2. Валидные ли параметры?
            if (curChunkIndex.HasValue && chunkSize.HasValue)
            {
                //1. Кол-во пропускаемых по условию записей
                //2. Выборка текущей партии записей
                int skipCount = curChunkIndex.Value * chunkSize.Value;
                result = Context.Set<T>().Where(ent=>!ent.WasDeleted).Skip(skipCount).Take(chunkSize.Value);
            }
            //3. Ответ
            return result;
        }

        //2. Получить все записи партии (даже среди удаленных)
        public IQueryable<T> GetChunkFromAllEvenDeleted(int? curChunkIndex, int? chunkSize)
        {
            //1. Хранилище ответа
            IQueryable<T> result = null;
            //2. Валидные ли параметры?
            if (curChunkIndex.HasValue && chunkSize.HasValue)
            {
                //1. Кол-во пропускаемых по условию записей
                //2. Выборка текущей партии записей
                int skipCount = curChunkIndex.Value * chunkSize.Value;
                result = Context.Set<T>().Skip(skipCount).Take(chunkSize.Value);
            }
            //3. Ответ
            return result;
        }

        //3. Получить кол-во партий (среди имеющихся записей)
        public int? GetChunkCountFromAll(int? chunkSize)
        {
            //1.
            int? result = null;
            //2. Валиден ли параметр?
            if (chunkSize.HasValue)
            {
                result = (int)Math.Ceiling(Context.Set<T>().Where(ent=>!ent.WasDeleted).Count() / (double)chunkSize);
            }
            //3. Ответ
            return result;
        }

        //4. Получить кол-во партий (даже среди удаленных записей)
        public int? GetChunkCountFromAllEvenDeleted(int? chunkSize)
        {
            //1.
            int? result = null;
            //2. Валиден ли параметр?
            if (chunkSize.HasValue)
            {
                result = (int)Math.Ceiling(Context.Set<T>().Count() / (double)chunkSize);
            }
            //3. Ответ
            return result;
        }

        
    }
}
