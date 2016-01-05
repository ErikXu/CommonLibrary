using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.International.Converters.PinYinConverter;

namespace CommonLibrary.Client.Text.Impl
{
    public class SpellUtil : ISpellUtil
    {
        public string ToSpellFull(string words)
        {
            var spell = new StringBuilder();

            foreach (var word in words.Trim())
            {
                if (ChineseChar.IsValidChar(word))
                {
                    var spellsOfWord = GetSpellsOfWord(word);

                    if (spellsOfWord.Count > 1)
                    {
                        spell.AppendFormat("({0})", string.Join("|", spellsOfWord));
                    }
                    else
                    {
                        spell.Append(spellsOfWord[0]);
                    }
                }
                else
                {
                    spell.Append(word);
                }
            }

            return spell.ToString();
        }

        public string ToSpellShort(string words)
        {
            var spell = new StringBuilder();

            foreach (var word in words.Trim())
            {
                if (ChineseChar.IsValidChar(word))
                {
                    var heads = GetHeadsOfWord(word);

                    if (heads.Count > 1)
                    {
                        spell.AppendFormat("({0})", string.Join("|", heads));
                    }
                    else
                    {
                        spell.Append(heads[0]);
                    }
                }
                else
                {
                    spell.Append(word);
                }
            }

            return spell.ToString();
        }

        private static List<string> GetSpellsOfWord(char word)
        {
            var chineseChar = new ChineseChar(word);

            var spells = new List<string>();

            foreach (var pinyin in chineseChar.Pinyins)
            {
                if (pinyin == null)
                {
                    continue;
                }

                var spell = pinyin.Substring(0, 1) + pinyin.Substring(1, pinyin.Length - 2).ToLower();
                spells.Add(spell);
            }

            return spells.Distinct().ToList();
        }

        private static List<string> GetHeadsOfWord(char word)
        {
            var chineseChar = new ChineseChar(word);

            var heads = new List<string>();

            foreach (var pinyin in chineseChar.Pinyins)
            {
                if (pinyin == null)
                {
                    continue;
                }

                var head = pinyin.Substring(0, 1);
                heads.Add(head);
            }

            return heads.Distinct().ToList();
        }
    }
}