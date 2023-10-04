using Microsoft.EntityFrameworkCore;
using server2.Services.UserServices.Model;

namespace server2.Services.UserServices.Data
{
    public class UserContext : DbContext
    {
        public UserContext() { }
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
