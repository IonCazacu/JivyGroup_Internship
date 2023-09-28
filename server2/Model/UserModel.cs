using System.ComponentModel.DataAnnotations;

// The namespace keyword is used to declare a scope that contains a set of related objects

namespace UserApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Uuid { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
