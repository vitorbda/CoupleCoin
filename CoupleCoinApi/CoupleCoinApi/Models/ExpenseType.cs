using System.ComponentModel.DataAnnotations;

namespace CoupleCoinApi.Models
{
    public class ExpenseType
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public User? Owner { get; set; }
        public bool? IsCouple { get; set; }
        public Couple? Couple { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? AlterDate { get; set;}
        [Required]
        public bool IsActive { get; set; }        
    }
}
