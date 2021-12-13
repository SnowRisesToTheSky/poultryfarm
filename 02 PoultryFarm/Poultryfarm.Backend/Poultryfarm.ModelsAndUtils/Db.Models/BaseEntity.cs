using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.ModelsAndUtils.Db.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool WasDeleted { get; set; }
    }
}
