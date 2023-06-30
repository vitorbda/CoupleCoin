using System.ComponentModel.DataAnnotations;

namespace CoupleCoinApi.Models
{
    public class Expense
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public float ExpenseValue { get; set; }
        public ExpenseType? Type { get; set; }
        [StringLength(250)]
        public string? Description { get; set; }
        [Required]
        public DateTime ExpenseDate { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? AlterDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
