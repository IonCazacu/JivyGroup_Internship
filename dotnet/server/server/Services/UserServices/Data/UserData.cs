using Microsoft.EntityFrameworkCore;
using server.Services.UserServices.Model;

namespace server.Services.UserServices.Data
{
    public class UserContext : DbContext
    {
        public UserContext() { }
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
