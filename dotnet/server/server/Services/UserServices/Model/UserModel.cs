using System.ComponentModel.DataAnnotations;

namespace server.Services.UserServices.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public Guid Uuid { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Username is required")]
        [MinLength(8, ErrorMessage = "Username require at least 8 characters")]
        [MaxLength(20, ErrorMessage = "Username require at most 20 characters")]
        [RegularExpression(
            @"^(?!.*[._]{2})[_a-zA-Z0-9](?!.*[._]{2})[_a-zA-Z0-9.]{6,18}[_a-zA-Z0-9]$",
            ErrorMessage = "Username cannot contain consecutive unserscores or dots"
        )]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "At least 8 characters long")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,}$"
        )]
        // , ErrorMessage="Password require at least 1 special character (!@#$%^&*()_+-=[]{}|:;'<>,.?/~)"
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Must match the first password field.")]
        public string? ConfirmPassword { get; set; }
    }

    public class IntegrityException : Exception
    {
        public IntegrityException() { }
        public IntegrityException(string property) : base(property) { }
    }
}
