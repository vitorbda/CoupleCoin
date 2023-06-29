using System.ComponentModel.DataAnnotations;

namespace CoupleCoinApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(30, MinimumLength = 5)]
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(11)]        
        public string? CPF { get; set; }
        public string? Role { get; set; }
        public DateTime? CreateDate { get; set; }
        [Required]
        public bool IsActive { get; set; }        
    }
}
