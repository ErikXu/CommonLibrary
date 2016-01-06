using System.IO;
using System.Security.Cryptography;

namespace CommonLibrary.Client.Crypto.Impl
{
    public class DesCryptoUtil : IDesCryptoUtil
    {
        public byte[] Encrypt(byte[] plainBytes, byte[] key, byte[] iv)
        {
            using (var provider = new DESCryptoServiceProvider())
            {
                provider.Key = key;
                provider.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, provider.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                        cryptoStream.FlushFinalBlock();
                    }
                    return memoryStream.ToArray();
                }
            }
        }

        public byte[] Decrypt(byte[] encryptedBytes, byte[] key, byte[] iv)
        {
            using (var provider = new DESCryptoServiceProvider())
            {
                provider.Key = key;
                provider.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, provider.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                        cryptoStream.FlushFinalBlock();
                    }
                    return memoryStream.ToArray();
                }
            }
        }
    }
}