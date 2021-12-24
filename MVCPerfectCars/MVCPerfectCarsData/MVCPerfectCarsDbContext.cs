using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MVCPerfectCarsData
{
    public class MVCPerfectCarsDbContext : IdentityDbContext<User,Role,int>
    {
        public MVCPerfectCarsDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Banner> Banner { get; set; }

        public virtual DbSet<Modul> Moduls { get; set; }

        public virtual DbSet<Portfolio> Portfolios { get; set; }

        public virtual DbSet<Representative> Representative { get; set; }

        public virtual DbSet<Vehicle> Vehicles { get; set; }

        public virtual DbSet<VehicleImage> VehicleImages { get; set; }

        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    }
}
