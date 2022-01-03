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
   public class Banner :BaseEntity, IHasImage
    {
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string SafeImage => Image ?? "/content/images/no-image.png";


    }


    public class BannerEntityTypeConfiguration : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder
                .Property(p => p.Image)
                .IsRequired()
                .IsUnicode(false);
            
        }
    }
}
