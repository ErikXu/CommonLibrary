using System.Text;

namespace CommonLibrary.Test.Crypto
{
    public class AesCryptoSetting
    {
        /// <summary>
        /// A system key and the length should be 16.
        /// You can use tool to generate the string on https://www.random.org/strings/ or other website.
        /// </summary>
        private const string SystemKey = "84ImUeBn432oPkqo";

        /// <summary>
        /// A custom key and the lenth should between 4 and 16. You can use the project name as the custom key.
        /// </summary>
        private const string UserKey = "AecCrypto";

        /// <summary>
        /// Please indicate a random string here, and the length must be 16.
        /// You can use tool to generate the string on https://www.random.org/strings/ or other website.
        /// </summary>
        public static byte[] Iv
        {
            get
            {
                return Encoding.ASCII.GetBytes("bCNtStALc7bRqREq");
            }
        }

        public static byte[] Key
        {
            get
            {
                var key = UserKey.PadRight(16, '#') + SystemKey;
                return Encoding.ASCII.GetBytes(key);
            }
        }

        public static int HeadSaltSize
        {
            get
            {
                return 32;
            }
        }

        public static int TailSaltSize
        {
            get
            {
                return 65;
            }
        }
    }
}