using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.ModelsAndUtils.Http.Workers
{
    public class ClientWorker
    {
        //СВОЙСТВА
        public int id { get; set; }
        public bool wasDeleted { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string passport { get; set; }
        public int salary { get; set; }

    }
}
