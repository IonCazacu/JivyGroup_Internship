using System.ComponentModel.DataAnnotations;
using server.Services.UserServices.Entities.Base;

namespace server.Services.UserServices.Entities
{
    public class User : BaseEntity
    {
        public string Uuid { get; set; } = Guid.NewGuid().ToString();
        [Required()]
        [RegularExpression(
            @"^(?!.*[._]{2})[_a-zA-Z0-9](?!.*[._]{2})[_a-zA-Z0-9.]{6,18}[_a-zA-Z0-9]$"
        )]
        public string? Username { get; set; }
        [Required()]
        [EmailAddress()]
        public string? Email { get; set; }
        [Required()]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,}$"
        )]
        public string? Password { get; set; }
        [Compare("Password")]   
        public string? ConfirmPassword { get; set; }
    }

    public class IntegrityException : Exception
    {
        public IntegrityException() { }
        public IntegrityException(string property) : base(property) { }
    }
}
