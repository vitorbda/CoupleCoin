using CoupleCoinApi.Models;

namespace CoupleCoinApi.DTO
{
    public class CoupleDTO
    {
        public User User1 { get; set; }
        public User User2 { get; set; }
        public bool IsActive { get; set; }
    }
}
