using System.ComponentModel.DataAnnotations.Schema;

namespace CoupleCoinApi.Models.ViewModel
{
    public class ExpenseViewModel
    {
        [NotMapped]
        public float? ExpenseValue { get; set; }
        [NotMapped]
        public string? Type { get; set; }
        [NotMapped]
        public string? Description { get; set; }
        [NotMapped]
        public DateTime? ExpenseDate { get; set; }
        [NotMapped]
        public DateTime? AlterDate { get; set; }
    }
}
