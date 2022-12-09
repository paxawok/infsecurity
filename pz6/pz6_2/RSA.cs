using System.Security.Cryptography;

namespace pz6_2
{
    public class RSA
    {
        private readonly static string CspContainerName = "RsaContainer";
        private static RSAParameters _publicKey, _privateKey;

        public static void GetKey(string publicKeyPath, string privateKeyPath)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                string pb = File.ReadAllText(publicKeyPath);
                rsa.FromXmlString(pb);
                _publicKey = rsa.ExportParameters(false);
                if (privateKeyPath != null)
                {
                    string pr = File.ReadAllText(privateKeyPath);
                    rsa.FromXmlString(pr);
                    _privateKey = rsa.ExportParameters(true);
                }
            }
        }
        public static void AssignNewKey(string publicKeyPath)
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            using (var rsa = new RSACryptoServiceProvider(2048, cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
            };
        }
        public static void DeleteKeyInCsp()
        {
            CspParameters cspParameters = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = false
            };
            rsa.Clear();
        }
        public static byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cypherBytes;
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                // rsa.PersistKeyInCsp = true;
                rsa.ImportParameters(_publicKey);
                cypherBytes = rsa.Encrypt(dataToEncrypt, true);
            }
            return cypherBytes;
        }
        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainBytes;
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                // rsa.ImportParameters(_privateKey);
                plainBytes = rsa.Decrypt(dataToDecrypt, true);
                File.WriteAllText("./Private.xml", rsa.ToXmlString(true));
            }
            return plainBytes;
        }
    }
}