using Poultryfarm.ModelsAndUtils.Db.Models.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.ModelsAndUtils.Db.Models.Cells
{
    public class Row : BaseEntity
    {
        //1. Номер ряда
        [Required]
        public int Number { get; set; }

        //2. Ссылка на цех
        [ForeignKey("WorkshopId")]
        public Workshop Workshop { get; set; }
        public int WorkshopId { get; set; }
        
        //3. Ссылка на рабочего
        [ForeignKey("WorkerId")]
        public Worker Worker { get; set; }
        public int WorkerId { get; set; }


        //4. Ссылка на клетки ряда
        public ICollection<Cell> Cells { get; set; } = new HashSet<Cell>();

    }
}
