using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.ModelsAndUtils.Db.Models.Persons
{
    public class Person:BaseEntity
    {
        //1. Серия, номер паспорта
        [Required, MaxLength(20)]
        public string Passport { get; set; }

        //2. Фамилия
        [MaxLength(60)]
        public string Surname { get; set; }

        //3. Имя
        [Required, MaxLength(60)]
        public string Name { get; set; }

        //4. Отчество
        [MaxLength(60)]
        public string Patronymic { get; set; }

        //5. Сущность работника для этой персоны
        public Worker Worker { get; set; }
    }
}
