using System.Text;
using XSystem.Security.Cryptography;

namespace CoupleCoinApi.Services
{
    public static class EncryptService
    {
        public static string ConvertToSHA256Hash(string data)
        {
            using (var sha256 = new SHA256Managed())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.ASCII.GetBytes(data)));
            }
        }
    }
}
