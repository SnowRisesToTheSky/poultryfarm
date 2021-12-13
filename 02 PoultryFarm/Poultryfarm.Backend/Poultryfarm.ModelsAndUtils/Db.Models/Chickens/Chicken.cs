using Poultryfarm.ModelsAndUtils.Db.Models.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.ModelsAndUtils.Db.Models.Chickens
{
    public class Chicken : BaseEntity
    {
        //1. Вес
        //2. Возраст
        public double Weight { get; set; }
        public double Age { get; set; }

        //3. Среднее кол-во яиц в месяц
        public double AvgEggCountPerMonth { get; set; }

        //4. Ссылка на породу
        [ForeignKey("BreedId")]
        public Breed Breed  { get; set; }
        public int BreedId { get; set; }

        //5. Ссылка на клетку
        [ForeignKey("CellId")]
        public Cell Cell { get; set; }
        public int CellId { get; set; }
    }
}
