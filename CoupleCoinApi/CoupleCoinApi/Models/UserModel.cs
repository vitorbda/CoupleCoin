using System.ComponentModel.DataAnnotations.Schema;

namespace CoupleCoinApi.Models
{
    public class UserModel
    {
        [NotMapped]
        public int Id { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
