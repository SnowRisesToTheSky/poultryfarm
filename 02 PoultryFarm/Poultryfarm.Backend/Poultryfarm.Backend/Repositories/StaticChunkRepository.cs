using System;
using System.Linq;

namespace Poultryfarm.Backend.Repositories
{
    public static class ChunkRepository
    {
        //СТАТ. МЕТОДЫ
        //1. Получить партию из выборки
        public static IQueryable<M> GetChunkFromSome<M>(IQueryable<M> query, int? curChunkIndex, int? chunkSize)
        {
            //1. Хранилище ответа
            IQueryable<M> result = null;
            //2. Валидные ли параметры?
            if (curChunkIndex.HasValue && chunkSize.HasValue)
            {
                //1. Кол-во пропускаемых по условию записей
                //2. Выборка текущей партии записей
                int skipCount = curChunkIndex.Value * chunkSize.Value;
                result = query.Skip(skipCount).Take(chunkSize.Value);
            }
            //3. Ответ
            return result;
        }

        //2. Получить кол-во партий
        public static int? GetChunkCountFromSome<M>(IQueryable<M> query, int? chunkSize)
        {
            //1.
            int? result = null;
            //2. Валиден ли параметр?
            if (chunkSize.HasValue)
            {
                result = (int)Math.Ceiling(query.Count() / (double)chunkSize);
            }
            //3. Ответ
            return result;
        }
    }
}
