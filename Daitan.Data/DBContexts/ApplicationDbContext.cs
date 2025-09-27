

using Daitan.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Daitan.Data.DBContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<GatewayDevice> GatewayDevices { get; set; }
        public DbSet<DeviceReadingHistory> DeviceReadingHistory { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Machine> Machines { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=Daitan;User Id=sa;Password=SmartLife123++@#;Encrypt=Optional;");
        //}


    }
}
