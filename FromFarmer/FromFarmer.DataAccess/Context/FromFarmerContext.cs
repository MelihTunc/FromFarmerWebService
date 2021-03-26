using FromFarmer.Entities.Concrete;
using FromFarmer.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace FromFarmer.DataAccess.Context
{
    public class FromFarmerContext : DbContext, IDisposable
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = ConfigurationHelper.GetConfig();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("FromFarmerDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<FF_USER> FF_USER { get; set; }
        public DbSet<FF_PRODUCT> FF_PRODUCT { get; set; }
        public DbSet<FF_CONSUMER> FF_CONSUMER { get; set; }
        public DbSet<FF_FARMER> FF_FARMER { get; set; }
        public DbSet<FF_ORDER> FF_ORDER { get; set; }
    }
}
