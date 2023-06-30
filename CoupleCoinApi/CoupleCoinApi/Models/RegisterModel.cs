using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoupleCoinApi.Models
{
    public class RegisterModel
    {
        [NotMapped]
        [Required]
        public string UserName { get; set; }
        [NotMapped]
        [Required]
        public string Password { get; set; }
        [NotMapped]
        [Required]
        public string Email { get; set; }
        [NotMapped]
        [Required]
        public string Name { get; set; }
        [NotMapped]
        [Required]
        public string LastName { get; set; }

    }
}
