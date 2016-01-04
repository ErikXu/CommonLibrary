namespace CommonLibrary.Client.Crypto
{
    public interface IAesCryptoUtil
    {
        byte[] Encrypt(byte[] plainBytes, byte[] key, byte[] iv);

        byte[] Decrypt(byte[] encryptedBytes, byte[] key, byte[] iv);
    }
}