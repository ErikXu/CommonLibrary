namespace CommonLibrary.Client.Text
{
    public interface ISpellUtil
    {
        string ToSpellFull(string words);

        string ToSpellShort(string words);
    }
}