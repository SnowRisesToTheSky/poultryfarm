using Poultryfarm.ModelsAndUtils.Db.Models.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.ModelsAndUtils.Db.Models.Persons
{
    public class Worker: BaseEntity
    {
        //1. Персона
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public int PersonId { get; set; }

        //2. Зарплата
        public int Salary { get; set; }

        //3. Ссылка на ряды рабочего
        public ICollection<Row> Rows { get; set; } = new HashSet<Row>();
    }
}
