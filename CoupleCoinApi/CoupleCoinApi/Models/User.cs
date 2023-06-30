using System.ComponentModel.DataAnnotations;

namespace CoupleCoinApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        [Required]
        public string Password { get; set; }
        public string? CPF { get; set; }
        public string? Role { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? AlterDate { get; set; }
        [Required]
        public bool IsActive { get; set; }        
    }
}
