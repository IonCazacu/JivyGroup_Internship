using System.ComponentModel.DataAnnotations;

namespace server.Services.UserServices.Entities.Authorization
{
    public class BasicAuthorization
    {
        [Required()]
        public string? Username { get; set; }
        [Required()]
        public string? Password { get; set; }
    }
}
