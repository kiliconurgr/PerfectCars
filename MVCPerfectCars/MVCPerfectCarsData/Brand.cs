using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPerfectCarsData
{
    public class Brand : BaseEntity
    {
        [Display(Name = "Marka Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string Name { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public string SafeImage => Image ?? "/content/images/no-photo-available (1).png";



        public virtual ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();

        public virtual ICollection<Modul> Moduls { get; set; } = new HashSet<Modul>();

        public virtual ICollection<Portfolio> Portfolios { get; set; } = new HashSet<Portfolio>();

       

        
    }

    public class BrandEntityTypeConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder

                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
                

            builder
                .Property(p => p.Image)
                .IsRequired()
                .IsUnicode(false);
        }
    }
}
