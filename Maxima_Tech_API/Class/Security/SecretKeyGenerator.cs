using System.Security.Cryptography;
using System.Text;

namespace Maxima_Tech_API.Class.Security
{
    public class SecretKeyGenerator
    {
        public static string GenerateSecretKey(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            var bytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            var result = new StringBuilder(length);
            foreach (var b in bytes)
            {
                result.Append(chars[b % chars.Length]);
            }
            return result.ToString();
        }
    }
}
