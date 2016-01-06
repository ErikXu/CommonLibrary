using System.Text;

namespace CommonLibrary.Test.Crypto
{
    public class DesCryptoSetting
    {
        /// <summary>
        /// The iv, length is 8, generated on https://www.random.org/strings/
        /// </summary>
        private static readonly string _iv = "62EcX79F";

        /// <summary>
        /// The key, length is 8, generated on https://www.random.org/strings/
        /// </summary>
        private static readonly string _key = "0e3Nl9Z9";

        public static byte[] Iv
        {
            get
            {
                return Encoding.ASCII.GetBytes(_iv);
            }
        }

        public static byte[] Key
        {
            get
            {
                return Encoding.ASCII.GetBytes(_key);
            }
        }

        public static int HeadSaltSize
        {
            get
            {
                return 11;
            }
        }

        public static int TailSaltSize
        {
            get
            {
                return 22;
            }
        }
    }
}