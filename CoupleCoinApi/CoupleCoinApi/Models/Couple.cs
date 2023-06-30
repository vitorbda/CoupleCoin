using System.ComponentModel.DataAnnotations;

namespace CoupleCoinApi.Models
{
    public class Couple
    {
        [Required]
        public int Id { get; set; }
        public User? User1 { get; set; }
        public User? User2 { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
