using System;
using System.Linq;
using System.Text;
using CommonLibrary.Client.Crypto;
using CommonLibrary.Client.Crypto.Impl;
using NUnit.Framework;

namespace CommonLibrary.Test.Crypto
{
    [TestFixture]
    public class DesCryptoTests
    {
        private IDesCryptoUtil _desCryptoUtil;
        private ISaltUtil _saltUtil;

        [SetUp]
        public void SetUp()
        {
           _desCryptoUtil = new DesCryptoUtil();
           _saltUtil = new SaltUtil();
        }

        [TestCase("123456", "ecIwYJUsLa0=")]
        [TestCase("aaaaaa", "GkU0ADc3ggk=")]
        public void Encrypt_Normal(string plainText, string expetedText)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = _desCryptoUtil.Encrypt(plainBytes, DesCryptoSetting.Key, DesCryptoSetting.Iv);
            var result = Convert.ToBase64String(encryptedBytes);

            Assert.AreEqual(expetedText, result);
        }

        [TestCase("ecIwYJUsLa0=", "123456")]
        [TestCase("GkU0ADc3ggk=", "aaaaaa")]
        public void Decrypt_Normal(string encryptedText, string expetedText)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var plainBytes = _desCryptoUtil.Decrypt(encryptedBytes, DesCryptoSetting.Key, DesCryptoSetting.Iv);
            var result = Encoding.UTF8.GetString(plainBytes);

            Assert.AreEqual(expetedText, result);
        }

        [TestCase("123456")]
        [TestCase("aaaaaa")]
        public void Encrypt_With_Salt_Normal(string plainText)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var headSalt = _saltUtil.GenerateSalt(DesCryptoSetting.HeadSaltSize);
            var tailSalt = _saltUtil.GenerateSalt(DesCryptoSetting.TailSaltSize);
            var plainBytesWithSalts = Combine(headSalt, plainBytes, tailSalt);
            var encryptedBytes = _desCryptoUtil.Encrypt(plainBytesWithSalts, DesCryptoSetting.Key, DesCryptoSetting.Iv);
            var result = Convert.ToBase64String(encryptedBytes);

            Console.WriteLine(result);
        }

        [TestCase("afSfgho561FmwjB8X86oHrSOytKnMcw5sIYbkabJ+B3eqb7gyVS8Vw==", "123456")]
        [TestCase("96AjF3ifrnNyX74NSAvGBKK2Gi8NrkA/LQ1uMdm6FdhGIdgvsAqHMg==", "123456")]
        [TestCase("OHsmYlq+fcOr7rze50DUWorNJCRU08sILBgfyCM1nSVk56PZcIxiGw==", "aaaaaa")]
        [TestCase("hYevLVzjlLBgeR+PxgaGR2pDhQIezLI9XkXh/SRBGgQC/2z9Ees7Jg==", "aaaaaa")]
        public void Decrypt_With_Salt_Normal(string encryptedText, string expetedText)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var plainBytesWithSalts = _desCryptoUtil.Decrypt(encryptedBytes, DesCryptoSetting.Key, DesCryptoSetting.Iv);
            var plainBytes = plainBytesWithSalts.Skip(DesCryptoSetting.HeadSaltSize).Take(plainBytesWithSalts.Length - DesCryptoSetting.HeadSaltSize - DesCryptoSetting.TailSaltSize).ToArray();

            var result = Encoding.UTF8.GetString(plainBytes);

            Assert.AreEqual(expetedText, result);
        }

        private static byte[] Combine(params byte[][] arrays)
        {
            var result = new byte[arrays.Sum(a => a.Length)];

            var offset = 0;

            foreach (var array in arrays)
            {
                Buffer.BlockCopy(array, 0, result, offset, array.Length);
                offset += array.Length;
            }

            return result;
        }
    }
}