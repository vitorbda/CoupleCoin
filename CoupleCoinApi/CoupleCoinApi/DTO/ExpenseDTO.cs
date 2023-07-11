using CoupleCoinApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoupleCoinApi.DTO
{
    public class ExpenseDTO
    {
        [NotMapped]
        public double ExpenseValue { get; set; }
        [NotMapped]
        public string? UsernameOne { get; set; }
        [NotMapped]
        public string? UsernameTwo { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public DateTime ExpenseDate { get; set; }
        [NotMapped]
        public int ExpenseTypeId { get; set; }
    }
}
