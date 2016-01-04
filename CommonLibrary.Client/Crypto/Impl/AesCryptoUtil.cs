using System.Security.Cryptography;

namespace CommonLibrary.Client.Crypto.Impl
{
    public class AesCryptoUtil : IAesCryptoUtil
    {
        public byte[] Encrypt(byte[] plainBytes, byte[] key, byte[] iv)
        {
            return Encrypt(plainBytes, key, iv, CipherMode.CBC, PaddingMode.PKCS7);
        }

        public byte[] Decrypt(byte[] encryptedBytes, byte[] key, byte[] iv)
        {
            return Decrypt(encryptedBytes, key, iv, CipherMode.CBC, PaddingMode.PKCS7);
        }

        private static byte[] Encrypt(byte[] plainBytes, byte[] key, byte[] iv, CipherMode cipher, PaddingMode padding)
        {
            using (var aes = Rijndael.Create())
            {
                aes.Mode = cipher;
                aes.Padding = padding;

                using (var transform = aes.CreateEncryptor(key, iv))
                {
                    var encryptedBytes = transform.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return encryptedBytes;
                }
            }
        }

        private static byte[] Decrypt(byte[] encryptedBytes, byte[] key, byte[] iv, CipherMode cipher, PaddingMode padding)
        {
            using (var aes = Rijndael.Create())
            {
                aes.Mode = cipher;
                aes.Padding = padding;

                using (var transform = aes.CreateDecryptor(key, iv))
                {
                    var plainBytes = transform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    return plainBytes;
                }
            }
        }
    }
}