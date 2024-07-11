using FuelFocusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FuelFocusAPI.Data
{
    public class TripDbContext : DbContext
    {
        public TripDbContext(DbContextOptions<TripDbContext> options):base(options)
        {
            
        }
        public DbSet<TripDetail> Tbl_TripDetails { get; set; } = default;
    }
}
