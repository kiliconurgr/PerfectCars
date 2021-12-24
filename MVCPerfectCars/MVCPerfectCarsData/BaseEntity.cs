using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPerfectCarsData
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        [Display(Name = "Etkin")]
        public bool Enabled { get; set; }


        [Display(Name = "Oluşturulma Tarihi")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public DateTime DateOfCreation { get; set; }

    }
}
