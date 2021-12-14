using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPerfectCarsData
{
   public class Banner :BaseEntity
    {
        public string Image { get; set; }

        
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
