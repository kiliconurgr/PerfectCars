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
   
    public class Vehicle : BaseEntity, IHasImage
    {
        [Display(Name = "İlan Adı")]
        public string Name { get; set; }

        [Display(Name = "Model Adı")]
        public int ModulId { get; set; }

        [Display(Name = "Marka Adı")]
        public int BrandId { get; set; }


        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [NotMapped]
        [Display(Name = "Liste Fiyatı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [RegularExpression("^[+-]?[0-9]{1,3}(?:.?[0-9]{3})*(,[0-9]{2})?$", ErrorMessage = "Lütfen geçerli bir {0} yazınız!")]
        public string PriceText { get; set; }

        [Display(Name = "Görsel")]
        public string Image { get; set; }

        [Display(Name = "Açıklamalar")]
        public string Description { get; set; }
        
        
        public virtual Modul Modul { get; set; }

        public virtual Brand Brand { get; set; }

        [NotMapped]
        public string SafeImage => Image ?? "/content/images/no-image.png";

        [NotMapped]
        public IFormFile ImageFile { get; set; }


        [NotMapped]
        
        public IFormFile[] ImageFiles { get; set; }

        [NotMapped]
        public int[] ImagesToDeleted { get; set; }

        public virtual Representative Representative { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; } = new HashSet<Portfolio>();


        public virtual ICollection<VehicleImage> VehicleImages { get; set; } = new HashSet<VehicleImage>();


    }

    public class VehicleEntityTypeConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder
               .HasIndex(p => new { p.Name })
               .IsUnique(true);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(p => p.Price)
                .HasPrecision(18, 4);

            builder
                .HasMany(p => p.VehicleImages)
                .WithOne(p => p.Vehicle)
                .HasForeignKey(p => p.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}
