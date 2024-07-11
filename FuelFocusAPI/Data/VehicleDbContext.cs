using FuelFocusApI.Model;
using Microsoft.EntityFrameworkCore;

namespace FuelFocusAPI.Data
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {

        }
        public DbSet<Vehicle> Tbl_Vehicle_Details { get; set; } = default!;
    }
}
