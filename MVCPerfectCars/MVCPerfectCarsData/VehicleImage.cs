using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPerfectCarsData
{
    public class VehicleImage
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }

        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual Vehicle Vehicle { get; set; }

    }
    public class VehicleImageEntityTypeConfiguration : IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
        {
            builder
                .Property(p => p.Image)
                .IsRequired()
                .IsUnicode(false);
        }
    }

}
