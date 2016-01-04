using System.Security.Cryptography;
using System.Text;

namespace CommonLibrary.Client.Crypto.Impl
{
    public class Md5CryptoUtil : IMd5CryptoUtil
    {
        public string Encrypt(string plainText)
        {
            using (var md5 = MD5.Create())
            {
                var encryptedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                var stringBuilder = new StringBuilder();
                foreach (var b in encryptedBytes)
                {
                    stringBuilder.AppendFormat("{0:X2}", b);
                }
                return stringBuilder.ToString();
            }
        }
    }
}