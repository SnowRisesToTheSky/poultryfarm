using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.ModelsAndUtils.Db.Models.Chickens
{
    public class Breed : BaseEntity
    {
        //1. Наименование породы
        public string Name { get; set; }

        //2. Средний вес куриц породы
        public double AvgWeight { get; set; }

        //3. Среднее кол-во яиц в месяц куриц породы
        public double AvgEggCountPerMonth { get; set; }

        //4. Ссылка на диету
        [ForeignKey("DietId")]
        public Diet Diet { get; set; }
        public int DietId { get; set; }

        //5. Ссылка на куриц
        public ICollection<Chicken> Chickens { get; set; } = new HashSet<Chicken>();
    }
}
