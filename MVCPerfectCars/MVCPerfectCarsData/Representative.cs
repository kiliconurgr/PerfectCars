using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPerfectCarsData
{
    public class Representative : User
    {

        public string Photo { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();

    }


    public class RepresentativeEntityTypeConfiguration : IEntityTypeConfiguration<Representative>
    {
        public void Configure(EntityTypeBuilder<Representative> builder)
        {
            builder
                .Property(x => x.Photo)
                .IsRequired()
                .IsUnicode(false);

            builder
                .Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

        }
    }
}

