using Poultryfarm.ModelsAndUtils.Db.Models.Chickens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.ModelsAndUtils.Db.Models.Cells
{
    public class Cell : BaseEntity
    {
        //1. Номер клетки
        [Required]
        public int Number { get; set; }

        //2. Ссылка на ряд клетки
        [ForeignKey("RowId")]
        public Row Row { get; set; }
        public int RowId { get; set; }

        //3. Ссылка на курицу внутри
        public Chicken Chicken { get; set; }
    }
}
