using System;
using System.Linq;
using System.Text;
using CommonLibrary.Client.Crypto;
using CommonLibrary.Client.Crypto.Impl;
using NUnit.Framework;

namespace CommonLibrary.Test.Crypto
{
    [TestFixture]
    public class AesCryptoTests
    {
        private IAesCryptoUtil _aesCryptoUtil;
        private ISaltUtil _saltUtil;

        [SetUp]
        public void SetUp()
        {
            _aesCryptoUtil = new AesCryptoUtil();
            _saltUtil = new SaltUtil();
        }

        [TestCase("123456", "HDHlYOQuENPmtjFKvLZIEA==")]
        [TestCase("aaaaaa", "a18kR8QDUq+Tcdm7WUQznA==")]
        public void Encrypt_Normal(string plainText, string expetedText)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = _aesCryptoUtil.Encrypt(plainBytes, AesCryptoSetting.Key, AesCryptoSetting.Iv);
            var result = Convert.ToBase64String(encryptedBytes);

            Assert.AreEqual(expetedText, result);
        }

        [TestCase("HDHlYOQuENPmtjFKvLZIEA==", "123456")]
        [TestCase("a18kR8QDUq+Tcdm7WUQznA==", "aaaaaa")]
        public void Decrypt_Normal(string encryptedText, string expetedText)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var plainBytes = _aesCryptoUtil.Decrypt(encryptedBytes, AesCryptoSetting.Key, AesCryptoSetting.Iv);
            var result = Encoding.UTF8.GetString(plainBytes);

            Assert.AreEqual(expetedText, result);
        }

        [TestCase("123456")]
        [TestCase("aaaaaa")]
        public void Encrypt_With_Salt_Normal(string plainText)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var headSalt = _saltUtil.GenerateSalt(AesCryptoSetting.HeadSaltSize);
            var tailSalt = _saltUtil.GenerateSalt(AesCryptoSetting.TailSaltSize);
            var plainBytesWithSalts = Combine(headSalt, plainBytes, tailSalt);
            var encryptedBytes = _aesCryptoUtil.Encrypt(plainBytesWithSalts, AesCryptoSetting.Key, AesCryptoSetting.Iv);
            var result = Convert.ToBase64String(encryptedBytes);

            Console.WriteLine(result);
        }

        [TestCase("hzlfO2RIegWwUJ6BthbStzbf+/3TddAt1Fa1815Qg515yW/kMGdumWIbVxFWDwEmoRQQgX0bW8nG5IKnJI1Q3H2iOBerUJmFeDuvakDPRQjm92y0hcnSmm2iTGreQrrW7HCzCMR+J5W3k8uXa7hl1w==", "123456")]
        [TestCase("kAxgR91z/2FF0rlJwgP7tnsls+d7WaiC+zT3/JM8voOaetTvpz0MNjlc24q1JQh+4c6wRI47kr6vAMJDc/gV5CRuwx2zpay9l08ejl/wHluoR62Ae8iR+aASaBpaPBtFiJixPfrEenIw6x+4sXvrYQ==", "123456")]
        [TestCase("sOQac1ejw5vpufJRBCeqPVwlQg5PxYeO9MKC4vVUm29eTSn7XVooWG9ozcH/13r2y7UGnlatEPiZklfoyEU3c6BpkexbnPr0VIf6NCj5qta1Pemx4ANdAI07BVqyu+Ji1Idw0GaHBCqHKy83dbK5Nw==", "aaaaaa")]
        [TestCase("jQCUdzkRqxXf5ou8yLxq5vwGNyxuel/rmiTT32/Dg6fHdp9ThLJhek8L2JZJcAkNeulGrelOw7m6ir8mjNkvHNEczTV2oBRO6iAYTTyffuTJQlmV6iSJRBE/Yw5fLjBvOiceZQ8QikXy9VQXa6s4tw==", "aaaaaa")]
        public void Decrypt_With_Salt_Normal(string encryptedText, string expetedText)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var plainBytesWithSalts = _aesCryptoUtil.Decrypt(encryptedBytes, AesCryptoSetting.Key, AesCryptoSetting.Iv);
            var plainBytes = plainBytesWithSalts.Skip(AesCryptoSetting.HeadSaltSize).Take(plainBytesWithSalts.Length - AesCryptoSetting.HeadSaltSize - AesCryptoSetting.TailSaltSize).ToArray();

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