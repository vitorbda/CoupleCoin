namespace CoupleCoinApi.Models
{
    public class Couple
    {
        public int Id { get; set; }
        public User? User1 { get; set; }
        public User? User2 { get; set; }
        public bool IsActive { get; set; }
    }
}
