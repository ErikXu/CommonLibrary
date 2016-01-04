using CommonLibrary.Client.Crypto;
using CommonLibrary.Client.Crypto.Impl;
using NUnit.Framework;

namespace CommonLibrary.Test.Crypto
{
    [TestFixture]
    public class Md5CryptoTests
    {
        private IMd5CryptoUtil _md5CryptoUtil;

        [SetUp]
        public void SetUp()
        {
            _md5CryptoUtil = new Md5CryptoUtil();
        }

        [TestCase("123456", "E10ADC3949BA59ABBE56E057F20F883E")]
        [TestCase("aaaaaa", "0B4E7A0E5FE84AD35FB5F95B9CEEAC79")]
        public void Encrypt_Normal(string plainText, string expetedText)
        {
            var result = _md5CryptoUtil.Encrypt(plainText);
            Assert.AreEqual(expetedText, result);
        }
    }
}