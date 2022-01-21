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
    
    public class Portfolio : BaseEntity
    {
        [Display(Name = "Özellik Adı")]
        public string Name { get; set; }

       

        public virtual ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();

        public virtual ICollection<Modul> Moduls { get; set; } = new HashSet<Modul>();

        public virtual ICollection<Brand> Brands { get; set; } = new HashSet<Brand>();


    }

    public class PortfolioEntityTypeConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder
                .HasIndex(p => p.Name)
                .IsUnique();

           
        }
    }
}
