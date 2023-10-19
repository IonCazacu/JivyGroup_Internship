using System.ComponentModel.DataAnnotations;

namespace server.Services.UserServices.Model
{
    public class Auth
    {
        [Required()]
        public string? Username { get; set; }
        [Required()]
        public string? Password { get; set; }
    }
}
