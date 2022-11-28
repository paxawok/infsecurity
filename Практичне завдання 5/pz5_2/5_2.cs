using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    { //menu
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

            Console.WriteLine("\nEnter text: ");
            string inputData = Console.ReadLine();
            byte[] dataForUse = Encoding.UTF8.GetBytes(inputData);
            Console.WriteLine("\nEnter the password for your text: ");
            string uPass = Console.ReadLine();
            //hash паролю із сіллю
            byte[] hash = PBKDF2.hashPassword(uPass, 250000, generateRandomNumber(32));


            var init = new Algorithms();
            SymmetricAlgorithm algorithm;

            switch (option)
            {
                case 1:
                    var key1 = Key(8, hash);
                    var iv1 = Iv(8, hash);

                    algorithm = DESCryptoServiceProvider.Create();

                    var encDESMessage = init.Encryption(dataForUse, key1, iv1, algorithm);
                    var decDESMessage =init.Decryption(encDESMessage, key1, iv1, algorithm);

                    Console.WriteLine("DES: \n");
                    Console.WriteLine("____________________________________\n");
                    Console.WriteLine("Original message ->  " + inputData);
                    Console.WriteLine("Encrypted message -> " + Convert.ToBase64String(encDESMessage));
                    Console.WriteLine("Decrypted message -> " + Encoding.UTF8.GetString(decDESMessage));
                    break;
                case 2:
                    var key2 = Key(16, hash);
                    var iv2 = Iv(8, hash);

                    algorithm = TripleDESCryptoServiceProvider.Create();

                    var encTripleDESMessage = init.Encryption(dataForUse, key2, iv2, algorithm);
                    var decTripleDESMessage = init.Decryption(encTripleDESMessage, key2, iv2, algorithm);

                    Console.WriteLine("Triple DES: \n");
                    Console.WriteLine("____________________________________\n");
                    Console.WriteLine("Original message ->  " + inputData);
                    Console.WriteLine("Encrypted message -> " + Convert.ToBase64String(encTripleDESMessage));
                    Console.WriteLine("Decrypted message -> " + Encoding.UTF8.GetString(decTripleDESMessage));
                    break;
                case 3:
                    var key3 = Key(32, hash);
                    var iv3 = Iv(16, hash);

                    algorithm = AesCryptoServiceProvider.Create();

                    var encAesMessage = init.Encryption(dataForUse, key3, iv3, algorithm);
                    var decAesMessage = init.Decryption(encAesMessage, key3, iv3, algorithm);

                    Console.WriteLine("AES: \n");
                    Console.WriteLine("____________________________________\n");
                    Console.WriteLine("Original message ->  " + inputData);
                    Console.WriteLine("Encrypted message -> " + Convert.ToBase64String(encAesMessage));
                    Console.WriteLine("Decrypted message -> " + Encoding.UTF8.GetString(decAesMessage));
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        } while (true);
    }
    //генерація рандомних чисел
    public static byte[] generateRandomNumber(int length)
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[length];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    //функція для ключа
    public static byte[] Key(int length, byte[] hash)
    {
        var KEY = new byte[length];
        int key = 0;
        for (int i = 0; i < length; i++)
        {
            KEY[key] = hash[i];
            key++;
        }
        return KEY;
    }
    //функція для вектору ініціалізації
    public static byte[] Iv(int length, byte[] hash)
    {
        var IV = new byte[length];
        int iv = 0;
        for (int i = hash.Length - 1; i != hash.Length - length; i--)
        {
            IV[iv] = hash[i];
            iv++;
        }
        return IV;
    }
}
//алгоритми
public class Algorithms
{
    public byte[] Encryption(byte[] toEncrypt, byte[] key, byte[] iv, SymmetricAlgorithm symmetricAlgorithm)
    {
        using (var ag = symmetricAlgorithm)
        {
            ag.Mode = CipherMode.CBC;
            ag.Padding = PaddingMode.PKCS7;
            ag.Key = key;
            ag.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, ag.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(toEncrypt, 0, toEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }

    public byte[] Decryption(byte[] toDecrypt, byte[] key, byte[] iv, SymmetricAlgorithm symmetricAlgorithm)
    {
        using (var ag = symmetricAlgorithm)
        {
            ag.Mode = CipherMode.CBC;
            ag.Padding = PaddingMode.PKCS7;
            ag.Key = key;
            ag.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, ag.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(toDecrypt, 0, toDecrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }
}
//ключ на основі пароля
public class PBKDF2
{
    public static byte[] hashPassword(string passwordToHash, int numOfRounds, byte[] generatedSalt)
    {
        var hashedPassword = hashPasswordHash(Encoding.UTF8.GetBytes(passwordToHash), generatedSalt, numOfRounds);
        return hashedPassword;
    }

    public static byte[] hashPasswordHash(byte[] toBeHashed, byte[] generatedSalt, int numOfRounds)
    {
        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, generatedSalt, numOfRounds, HashAlgorithmName.SHA256))
        {
            return rfc2898.GetBytes(32);
        }
    }
}