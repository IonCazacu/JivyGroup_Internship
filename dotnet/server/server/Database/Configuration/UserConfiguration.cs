using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Services.UserServices.Model;

namespace server.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            
            for (int i = 1; i <= 1000; i++)
            {
                builder.HasData
                (
                    new User
                    {
                        Id = i,
                        Username = $"username{ i }",
                        Email = $"username{ i }@mail.com",
                        Password = $"username{ i }Password!",
                        ConfirmPassword = $"username{ i }Password!"
                    }
                );
            }
        }
    }
}
