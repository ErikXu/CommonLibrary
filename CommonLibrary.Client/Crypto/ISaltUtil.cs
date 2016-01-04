namespace CommonLibrary.Client.Crypto
{
    public interface ISaltUtil
    {
        byte[] GenerateSalt(int size);
    }
}