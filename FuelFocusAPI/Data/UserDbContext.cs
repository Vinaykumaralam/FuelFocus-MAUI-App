using FuelFocusAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace FuelFocusAPI.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        public DbSet<User> Tbl_User_Profile { get; set; } = default!;
    }
}
