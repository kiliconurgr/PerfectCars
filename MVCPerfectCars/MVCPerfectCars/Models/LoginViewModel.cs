using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPerfectCars.Models
{
    public class LoginViewModel
    {
        [Display(Name = "E-posta")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public string UserName { get; set; }


        [Display(Name = "Parola")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public string Password { get; set; }



        public bool IsPersistent { get; set; }

        public string ReturnUrl { get; set; }

    }
}
