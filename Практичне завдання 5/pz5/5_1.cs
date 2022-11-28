using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        do
        {
            Console.WriteLine("\nMenu");
            Console.WriteLine("____________________\n");
            Console.WriteLine("1. DES");
            Console.WriteLine("2. Triple-DES");
            Console.WriteLine("3. AES");
            Console.WriteLine("4. Exit\n");
            Console.WriteLine("_____________________\n");
            Console.WriteLine("Choose: ");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter your data");
                    Console.WriteLine("Your message: ");
                    string uMess = Console.ReadLine();

                    var des = new desEncryption();
                    var key = des.GenerateRandomNumber(8);
                    var iv = des.GenerateRandomNumber(8);

                    var logForEnc = des.Encryption(Encoding.UTF8.GetBytes(uMess), key, iv);
                    var encMessage = Convert.ToBase64String(logForEnc);
                    var logForDec = des.Decryption(logForEnc, key, iv);
                    var decMessage = Encoding.UTF8.GetString(logForDec);

                    Console.WriteLine("DES: \n");
                    Console.WriteLine("____________________________________\n");
                    Console.WriteLine("Original message ->  " + uMess);
                    Console.WriteLine("Encrypted message -> " + encMessage);
                    Console.WriteLine("Decrypted message -> " + decMessage);
                    break;
                case 2:
                    Console.WriteLine("Enter your data");
                    Console.WriteLine("Your message: ");
                    string uMess1 = Console.ReadLine();

                    var tdes = new tripleDesEncryption();
                    var key1 = tdes.GenerateRandomNumber(16);
                    var iv1 = tdes.GenerateRandomNumber(8);

                    var logForEnc1 = tdes.Encryption(Encoding.UTF8.GetBytes(uMess1), key1, iv1);
                    var encMessage1 = Convert.ToBase64String(logForEnc1);
                    var logForDec1 = tdes.Decryption(logForEnc1, key1, iv1);
                    var decMessage1 = Encoding.UTF8.GetString(logForDec1);

                    Console.WriteLine("Triple DES: \n");
                    Console.WriteLine("____________________________________\n");
                    Console.WriteLine("Original message ->  " + uMess1);
                    Console.WriteLine("Encrypted message -> " + encMessage1);
                    Console.WriteLine("Decrypted message -> " + decMessage1);
                    break;
                case 3:
                    Console.WriteLine("Enter your data");
                    Console.WriteLine("Your message: ");
                    string uMess2 = Console.ReadLine();

                    var aes = new aesEncryption();
                    var key2 = aes.GenerateRandomNumber(32);
                    var iv2 = aes.GenerateRandomNumber(16);

                    var logForEnc2 = aes.Encryption(Encoding.UTF8.GetBytes(uMess2), key2, iv2);
                    var encMessage2 = Convert.ToBase64String(logForEnc2);
                    var logForDec2 = aes.Decryption(logForEnc2, key2, iv2);
                    var decMessage2 = Encoding.UTF8.GetString(logForDec2);

                    Console.WriteLine("AES: \n");
                    Console.WriteLine("____________________________________\n");
                    Console.WriteLine("Original message ->  " + uMess2);
                    Console.WriteLine("Encrypted message -> " + encMessage2);
                    Console.WriteLine("Decrypted message -> " + decMessage2);
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        } while (true);
    }
}

class desEncryption
{
    public byte[] GenerateRandomNumber(int length)
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[length];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    public byte[] Encryption (byte[] dataToEncrypt, byte[] key, byte[] iv)
    {
        using (var des = new DESCryptoServiceProvider())
        {
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.Key = key;
            des.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }
    public byte[] Decryption(byte[] dataToDecrypt, byte[] key, byte[] iv)
    {
        using (var des = new DESCryptoServiceProvider())
        {
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.Key = key;
            des.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }
}
class tripleDesEncryption
{
    public byte[] GenerateRandomNumber(int length)
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[length];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    public byte[] Encryption(byte[] dataToEncrypt, byte[] key, byte[] iv)
    {
        using (var des = new TripleDESCryptoServiceProvider())
        {
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.Key = key;
            des.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }

    public byte[] Decryption(byte[] dataToDecrypt, byte[] key, byte[] iv)
    {
        using (var des = new TripleDESCryptoServiceProvider())
        {
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.Key = key;
            des.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }

}


class aesEncryption
{
    public byte[] GenerateRandomNumber(int length)
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[length];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    public byte[] Encryption(byte[] dataToEncrypt, byte[] key, byte[] iv)
    {
        using (var aes = new AesCryptoServiceProvider())
        {
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }

    public byte[] Decryption(byte[] dataToDecrypt, byte[] key, byte[] iv)
    {
        using (var des = new AesCryptoServiceProvider())
        {
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.Key = key;
            des.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }
}