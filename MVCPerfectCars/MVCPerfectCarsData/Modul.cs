using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPerfectCarsData
{
    public class Modul : BaseEntity

    {
        public string Name { get; set; }
        public int BrandId { get; set; }

        public int VehicleTypeId { get; set; }

        public virtual VehicleType VehicleType { get; set; }

        public virtual Brand Brand { get; set; }


        public virtual ICollection<Portfolio> Portfolios { get; set; } = new HashSet<Portfolio>();

        public virtual ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();



    }


    public class ModulEntityTypeConfiguration : IEntityTypeConfiguration<Modul>
    {
        public void Configure(EntityTypeBuilder<Modul> builder)
        {
            builder
                .HasIndex(p => new { p.Name, p.BrandId })
                .IsUnique(true);
            builder
                .HasIndex(p => new { p.Name, p.VehicleTypeId })
                .IsUnique(true);
            builder
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasMany(p => p.Vehicles)
                .WithOne(p => p.Modul)
                .HasForeignKey(p => p.ModulId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
