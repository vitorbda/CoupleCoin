namespace CoupleCoinApi.Models
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User? Owner { get; set; }
        public bool IsActive { get; set; }        
    }
}
