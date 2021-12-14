using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPerfectCarsData
{
    public enum TranmissionType
    {
        Auto,
        Manuel
    }

    public enum Fuel
    {
        Diesel,
        Gasoline
    }

    public enum DriveType
    {
        FW,
        RW,
        AW
    }
    public class Portfolio : BaseEntity
    {
        public string Name { get; set; }

        public string PlateNumber { get; set; }

        public TranmissionType TranmissionType { get; set; }

        public Fuel Fuel { get; set; }

        public DriveType DriveType { get; set; }

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
                .Property(p => p.PlateNumber)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
