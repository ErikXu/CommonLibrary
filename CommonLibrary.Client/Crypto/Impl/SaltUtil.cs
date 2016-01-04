using System.Security.Cryptography;

namespace CommonLibrary.Client.Crypto.Impl
{
    public class SaltUtil : ISaltUtil
    {
        public byte[] GenerateSalt(int size)
        {
            var salt = new byte[size];

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);

            return salt;
        }
    }
}