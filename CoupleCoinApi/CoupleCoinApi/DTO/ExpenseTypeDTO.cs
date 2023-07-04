using CoupleCoinApi.Models;

namespace CoupleCoinApi.DTO
{
    public class ExpenseTypeDTO
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string? OwnerTwo { get; set; }
    }
}
