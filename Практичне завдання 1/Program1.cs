using System.Security.Cryptography;

public class RandomCrypto
{
    public static byte[] GenerateRandomNumber()
    {
        using (var randomNumberGenerator = RandomNumberGenerator.Create())
        {
            int length = 64;
            var randomNumber = new byte[length];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    public static void Main()
    {
        int c = 1;
        for (int i = 0; i < 20; i++)
        {
            var rnd = GenerateRandomNumber();
            Console.WriteLine(c + ". " + Convert.ToBase64String(rnd) + "\n");
            c++;
        }
        Console.ReadLine();
    }
}
