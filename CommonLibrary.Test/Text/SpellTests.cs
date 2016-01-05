using CommonLibrary.Client.Text;
using CommonLibrary.Client.Text.Impl;
using NUnit.Framework;

namespace CommonLibrary.Test.Text
{
    [TestFixture]
    public class SpellTests
    {
        private ISpellUtil _spellUtil;

        [SetUp]
        public void SetUp()
        {
            _spellUtil = new SpellUtil();
        }

        [TestCase("深圳", "ShenZhen")]
        [TestCase("清远", "QingYuan")]
        public void ToSpellFull_Normal(string words, string expetedText)
        {
            var result = _spellUtil.ToSpellFull(words);
            Assert.AreEqual(expetedText, result);
        }

        [TestCase("广州", "(An|Guang)Zhou")]
        [TestCase("佛山", "(Fo|Fu)Shan")]
        public void ToSpellFull_Polyphone(string words, string expetedText)
        {
            var result = _spellUtil.ToSpellFull(words);
            Assert.AreEqual(expetedText, result);
        }

        [TestCase("深圳", "SZ")]
        [TestCase("清远", "QY")]
        public void ToSpellShort_Normal(string words, string expetedText)
        {
            var result = _spellUtil.ToSpellShort(words);
            Assert.AreEqual(expetedText, result);
        }

        [TestCase("广州", "(A|G)Z")]
        [TestCase("佛山", "FS")]
        public void ToSpellShort_Polyphone(string words, string expetedText)
        {
            var result = _spellUtil.ToSpellShort(words);
            Assert.AreEqual(expetedText, result);
        }
    }
}