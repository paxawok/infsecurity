using System.Security.Cryptography;
using System.Text;

namespace hash 
{
    class Programm
    {
        public static void Main()
        {

            Console.Write("Enter message:  ");
            string message = Console.ReadLine();

            byte[] messageInArray = Encoding.Unicode.GetBytes(message);

            byte[] keySHA1 = cryptoKey(20);
            byte[] keySHA256 = cryptoKey(32);
            byte[] keySHA512 = cryptoKey(64);

            var HmacSHA1 = ComputeHMACSHA1(messageInArray, keySHA1);
            var HmacSHA256 = ComputeHMACSHA256(messageInArray, keySHA256);
            var HmacSHA512 = ComputeHMACSHA512(messageInArray, keySHA512);

            Console.WriteLine("You can: \n");
            Console.WriteLine("1 - Just authentication");
            Console.WriteLine("2 - Failed authentication\n");
            Console.WriteLine("Enter the num: ");
            int option = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("--------------------\n");
            switch (option)
            {
                case 1:

                    Console.WriteLine($"Hash by HMAC SHA1 ->     {Convert.ToBase64String(HmacSHA1)}"); 
                    Console.WriteLine($"Hash by HMAC SHA256 ->   {Convert.ToBase64String(HmacSHA256)}");
                    Console.WriteLine($"Hash by HMAC SHA512 ->   {Convert.ToBase64String(HmacSHA512)}\n");

                    var HmacsSHA1 = ComputeHMACSHA1(messageInArray, keySHA1);
                    var HmacsSHA256 = ComputeHMACSHA256(messageInArray, keySHA256);
                    var HmacsSHA512 = ComputeHMACSHA512(messageInArray, keySHA512);

                    Console.WriteLine($"Hash by HMAC SHA1 (second) ->     {Convert.ToBase64String(HmacsSHA1)}");
                    Console.WriteLine($"Hash by HMAC SHA256 (second) ->   {Convert.ToBase64String(HmacsSHA256)}");
                    Console.WriteLine($"Hash by HMAC SHA512 (second) ->   {Convert.ToBase64String(HmacsSHA512)}\n");

                    Console.Write("Check HMAC SHA1:   ");
                    checkHash(HmacSHA1, HmacsSHA1);
                    Console.Write("Check HMAC SHA256: ");
                    checkHash(HmacSHA256, HmacsSHA256);
                    Console.Write("Check HMAC SHA512: ");
                    checkHash(HmacSHA512, HmacsSHA512);
                    break;
                case 2:

                    Console.WriteLine($"Hash by HMAC SHA1 ->     {Convert.ToBase64String(HmacSHA1)}"); 
                    Console.WriteLine($"Hash by HMAC SHA256 ->   {Convert.ToBase64String(HmacSHA256)}");
                    Console.WriteLine($"Hash by HMAC SHA512 ->   {Convert.ToBase64String(HmacSHA512)}\n");

                    //після знаходження хежу ми перезнаходимо секретні ключі

                    byte[] key2SHA1 = cryptoKey(20);
                    byte[] key2SHA256 = cryptoKey(32);
                    byte[] key2SHA512 = cryptoKey(64);

                    //тому зараз тут ми матимемо інакший хеш, ніж в попередньому повідомленні

                    var Hmacs2SHA1 = ComputeHMACSHA1(messageInArray, key2SHA1);
                    var Hmacs2SHA256 = ComputeHMACSHA256(messageInArray, key2SHA256);
                    var Hmacs2SHA512 = ComputeHMACSHA512(messageInArray, key2SHA512);

                    Console.WriteLine($"Hash by HMAC SHA1 (second) ->     {Convert.ToBase64String(Hmacs2SHA1)}"); 
                    Console.WriteLine($"Hash by HMAC SHA256 (second) ->   {Convert.ToBase64String(Hmacs2SHA256)}");
                    Console.WriteLine($"Hash by HMAC SHA512 (second) ->   {Convert.ToBase64String(Hmacs2SHA512)}\n\n");

                    Console.Write("Check HMAC SHA1:    ");
                    checkHash(HmacSHA1, Hmacs2SHA1);
                    Console.Write("Check HMAC SHA256:  ");
                    checkHash(HmacSHA256, Hmacs2SHA256);
                    Console.Write("Check HMAC SHA512:  ");
                    checkHash(HmacSHA512, Hmacs2SHA512);

                    break;

            }
        }

        static byte[] cryptoKey (int amnt)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                var key = new byte[amnt];
                rng.GetBytes(key);
                return key;
            }
        }
        static int checkHash(byte[] hash1, byte[] hash2)
        {
            if (Convert.ToBase64String(hash1) == Convert.ToBase64String(hash2))
            {
                Console.WriteLine("Message is not corrupted. All is okay.");
            }
            else
            {
                Console.WriteLine("Message is corrupted!!!");
            }
            return 0;
        }
        public static byte[] ComputeHMACSHA1(byte[] toBeHashed, byte[] key)
        {
            using (var HMAC1 = new HMACSHA1(key))
            {
                return HMAC1.ComputeHash(toBeHashed);
            }
        }

        public static byte[] ComputeHMACSHA256(byte[] toBeHashed, byte[] key)
        {
            using (var hmac256 = new HMACSHA256(key))
            {
                return hmac256.ComputeHash(toBeHashed);
            }
        }

        public static byte[] ComputeHMACSHA512(byte[] toBeHashed, byte[] key)
        {
            using (var hmac512 = new HMACSHA512(key))
            {
                return hmac512.ComputeHash(toBeHashed);
            }
        }
    }
}