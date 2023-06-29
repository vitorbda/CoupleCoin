namespace CoupleCoinApi.Models
{
    public class ValidateRegisterModel
    {
        public bool Valid { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? StatusCode { get; set; }
    }
}
