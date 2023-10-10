using Microsoft.EntityFrameworkCore;
using server.Database.Configuration;
using server.Services.UserServices.Model;

namespace server.Services.UserServices.Data
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        /*public IConfiguration Configuration { get; set; }*/
        public UserContext(/*IConfiguration configuration*/)
        {
            /*Configuration = configuration;*/
        }
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=./Database/Database.db");
        }*/
    }
}
