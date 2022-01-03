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
    public class VehicleType : BaseEntity
    {
        [Display(Name = "Araç Tipi Adı")]
        public string Name { get; set; }

        public virtual ICollection<Modul> Moduls { get; set; } = new HashSet<Modul>();



    }

    public class VehicleTypeEntityTypeConfiguration : IEntityTypeConfiguration<VehicleType>
    {
        public void Configure(EntityTypeBuilder<VehicleType> builder)
        {

            builder
                .HasIndex(p => new { p.Name })
                .IsUnique(true);

            builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

            builder
                .HasMany(p => p.Moduls)
                .WithOne(p => p.VehicleType)
                .HasForeignKey(p => p.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
