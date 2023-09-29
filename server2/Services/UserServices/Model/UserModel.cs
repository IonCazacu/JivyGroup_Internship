using System.ComponentModel.DataAnnotations;

namespace server2.Services.UserServices.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid Uuid { get; set; } = new Guid();
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }

    public class IntegrityException : Exception
    {
        public IntegrityException() { }
        public IntegrityException(string email) : base(string.Format("Integrity Exception: {0}" + email))
        {
        }
    }
}
