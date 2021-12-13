using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.ModelsAndUtils.Db.Models.Cells
{
    public class Workshop:BaseEntity
    {
        //Номер цеха
        [Required,MaxLength(20)]
        public string Number { get; set; }

        //Ссылка на ряды цеха
        public ICollection<Row> Rows { get; set; } = new HashSet<Row>();
    }
}
