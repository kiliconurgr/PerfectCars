using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPerfectCarsData
{
    public class Vehicle : BaseEntity
    {

        public string Name { get; set; }
        public int ModulId { get; set; }

        public int BrandId { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        [Display(Name = "Açıklamalar")]
        public string Description { get; set; }

        public virtual Modul Modul { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Representative Representative { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; } = new HashSet<Portfolio>();


        public virtual ICollection<VehicleImage> VehicleImages { get; set; } = new HashSet<VehicleImage>();


    }

    public class VehicleEntityTypeConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
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
