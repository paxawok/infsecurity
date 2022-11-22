using System.Security.Cryptography;
using System.Text;

namespace hash 
{
    class Programm
    {
        public static void Main()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("1 -> Hash using MD5");
            Console.WriteLine("2 -> Hash using SHA");
            Console.WriteLine("3 -> Hash using HMAC");
            Console.WriteLine("--------------------");
            Console.WriteLine("Choose your variant: ");
            int choose = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("--------------------");
            switch (choose)
            {
                case 1:
                    Console.WriteLine("Enter the message for finding a hash [MD5]");
                    string MD5inpt = Console.ReadLine();
                    Console.WriteLine("\n");
                    var MD5forinpt = ComputeHashMD5(Encoding.Unicode.GetBytes(MD5inpt));
                    Console.WriteLine($"Your message: {MD5inpt}\n");
                    Console.WriteLine($"Hash [MD5]: {Convert.ToBase64String(MD5forinpt)}");
                    break;
                case 2:
                    Console.WriteLine("Entet the message for finding hash [SHA]");
                    string SHAinpt = Console.ReadLine();
                    Console.WriteLine("\n");
                    var SHAforinpt = ComputeHashSHA256(Encoding.Unicode.GetBytes(SHAinpt));
                    Console.WriteLine($"Your message: {SHAinpt}\n");
                    Console.WriteLine($"Hash [SHA]: {Convert.ToBase64String(SHAforinpt)}");
                    break;
                case 3:
                    Console.WriteLine("Enter the message for finding hash [HMAC]");
                    string HMACinpt = Console.ReadLine();
                    Console.WriteLine("Enter the password: ");
                    string HMACpassinpt = Console.ReadLine();
                    byte[] HMACbyte = Encoding.Unicode.GetBytes(HMACinpt);
                    byte[] HMACpassbyte = Encoding.Unicode.GetBytes(HMACpassinpt);
                    var HMAC1 = ComputeHMACSHA1(HMACbyte, HMACpassbyte);
                    var HMAC256 = ComputeHMACSHA256(HMACbyte, HMACpassbyte);
                    var HMAC512 = ComputeHMACSHA512(HMACbyte, HMACpassbyte);
                    var HMACMD5 = ComputeHMACMD5(HMACbyte, HMACpassbyte);
                    Console.WriteLine($"\nYour message: {HMACinpt}\n");
                    Console.WriteLine($"Hash [HMAC1]: {Convert.ToBase64String(HMAC1)}");
                    Console.WriteLine($"Hash [HMAC256]: {Convert.ToBase64String(HMAC256)}");
                    Console.WriteLine($"Hash [HMAC512]: {Convert.ToBase64String(HMAC512)}");
                    Console.WriteLine($"Hash [HMACMD5]: {Convert.ToBase64String(HMACMD5)}");
                    break;
                default:
                    Console.WriteLine("Choose an option from the list");
                    break;
            }
        }

        static byte[] ComputeHashMD5(byte[] dataforMD5)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(dataforMD5);
            }
        }
        public static byte[] ComputeHashSHA256(byte[] dataforSHA)
        {
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(dataforSHA);
            }
        }

        public static byte[] ComputeHMACSHA1(byte[] dataforHMAC, byte[] pas)
        {
            byte[] key = new byte[dataforHMAC.Length];
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = pas[i % pas.Length];
            }
            using (var HMAC1 = new HMACSHA1(key))
            {
                return HMAC1.ComputeHash(dataforHMAC);
            }
        }

        public static byte[] ComputeHMACSHA256(byte[] dataforhmac, byte[] pas)
        {
            byte[] key = new byte[dataforhmac.Length];
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = pas[i % pas.Length];
            }
            using (var hmac256 = new HMACSHA256(key))
            {
                return hmac256.ComputeHash(dataforhmac);
            }
        }

        public static byte[] ComputeHMACSHA512(byte[] dataforhmac, byte[] pas)
        {
            byte[] key = new byte[dataforhmac.Length];
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = pas[i % pas.Length];
            }
            using (var hmac512 = new HMACSHA512(key))
            {
                return hmac512.ComputeHash(dataforhmac);
            }
        }
        public static byte[] ComputeHMACMD5(byte[] dataforhmac, byte[] pas)
        {
            byte[] key = new byte[dataforhmac.Length];
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = pas[i % pas.Length];
            }
            using (var hmacmd5 = new HMACMD5(key))
            {
                return hmacmd5.ComputeHash(dataforhmac);
            }
        }
    }
}