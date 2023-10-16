using System.ComponentModel.DataAnnotations;

namespace server.Services.UserServices.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public Guid Uuid { get; set; } = Guid.NewGuid();

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
