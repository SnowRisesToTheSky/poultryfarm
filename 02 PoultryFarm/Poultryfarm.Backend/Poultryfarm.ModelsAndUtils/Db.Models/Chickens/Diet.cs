using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.ModelsAndUtils.Db.Models.Chickens
{
    public class Diet:BaseEntity
    {
        [Required]
        //1. Наименование
        public string Name { get; set; }

        //2. Ссылка на породы
        public ICollection<Breed> Breeds { get; set; } = new HashSet<Breed>();
    }
}
