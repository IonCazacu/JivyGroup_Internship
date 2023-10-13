using System.ComponentModel.DataAnnotations;

namespace server.Services.UserServices.Model
{
    public class AuthModel
    {
        [Required()]
        public string? Username { get; set; }
        [Required()]
        public string? Password { get; set; }
    }
}
