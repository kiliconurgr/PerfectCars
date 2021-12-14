using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPerfectCarsData
{
    public class VehicleImage
    {
        public int VehicleImageId { get; set; }
        public int VehicleId { get; set; }

        public string Photo { get; set; }

        public virtual Vehicle Vehicle { get; set; }

    }
    public class VehicleImageEntityTypeConfiguration : IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
        {
            builder
                .Property(p => p.Photo)
                .IsRequired()
                .IsUnicode(false);
        }
    }

}
