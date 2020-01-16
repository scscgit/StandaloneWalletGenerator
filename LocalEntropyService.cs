namespace StandaloneWalletGenerator
{
    public class LocalEntropyService
    {
        public static byte[] GetRandomBytes(int numberOfBytes)
        {
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var bytes = new byte[numberOfBytes];
                rng.GetBytes(bytes);
                return bytes;
            }
        }
    }
}
