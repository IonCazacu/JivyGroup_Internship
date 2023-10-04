using System.ComponentModel.DataAnnotations;

namespace server2.Services.UserServices.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public Guid Uuid { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Username is required")]
        [MinLength(8, ErrorMessage = "Username require at least 8 characters")]
        [MaxLength(20, ErrorMessage = "Username require at most 20 characters")]
        [RegularExpression(@"^(?!.*[._]{2})[_a-zA-Z0-9](?!.*[._]{2})[_a-zA-Z0-9.]{6,18}[_a-zA-Z0-9]$", ErrorMessage = "Username cannot contain consecutive unserscores or dots")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password require at least 8 characters")]
        [RegularExpression(@"^(?=.*[!@#$%^&*()_+\-=\[\]{}|\\:;\'<>,.?\/~])[\S]{8,}$")]
        // , ErrorMessage="Password require at least 1 special character (!@#$%^&*()_+-=[]{}|:;'<>,.?/~)"
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }

    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }

    public class IntegrityException : Exception
    {
        public IntegrityException() { }
        public IntegrityException(string email) : base(string.Format($"Integrity Exception: { email }")) { }
    }
}
