using sidonia.db_contexts.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sidonia.db_contexts.Wrappers
{
    //Обертка над контекстом бд
    public class ContextWrapper
    {
        //ПОЛЯ
        //1. Указывает - было ли уже в этом объекте освобождение ресурсов (освобождение бд)
        private bool _disposed = false;
        //2. Контекст бд
        private PoultryfarmBaseContext _context;

        //2. Контекст бд
        public PoultryfarmBaseContext Context { 
            get {
                //Проверяем, не освобожден ли он
                if (_disposed) return null;
                return _context;
            }
        }

        //Конструктор
        public ContextWrapper(PoultryfarmBaseContext context)
        {
            _context = context;
        }

        //МЕТОДЫ
        //1. Сохранить результат CRUD - операций
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        //2. Освободить дескриптор бд и сам репозиторий
        public void Dispose()
        {
            if (!_disposed)
            {
                //1. Освобождаем ресурсы контекста бд
                _context.Dispose();
                //2. Устанавливаем что контекст уже освобожден
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
