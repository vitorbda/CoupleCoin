namespace CoupleCoinApi.Models
{
    public class ExpenseXOwner
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public Couple? Couple { get; set; }
        public Expense Expense { get; set; }
    }
}
