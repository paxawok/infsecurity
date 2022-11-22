using System;
using System.Security.Cryptography;
using System.Text;

namespace hash
{
    class Programm
    {
        public static void Main()
        {
            int q = 0;
            string[] dataLogin = new string[10];
            string[] dataPassword = new string[10];
            do
            {
                Console.WriteLine("Menu\n");
                Console.WriteLine("1 -> Registration");
                Console.WriteLine("2 -> Login");
                Console.WriteLine("3 -> Exit");
                Console.WriteLine("Choose variant: ");
                Console.WriteLine("___________________");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.WriteLine("\n____________________\n");
                        Console.WriteLine("Enter your login:");
                        string login_input = Console.ReadLine();
                        Console.WriteLine("Enter your password:");
                        string pas_input = null;
                        while (true)
                        {
                            var key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.Enter)
                                break;
                            pas_input += key.KeyChar;
                        }
                        byte[] login_byte = Encoding.Unicode.GetBytes(login_input);
                        byte[] pas_byte = Encoding.Unicode.GetBytes(pas_input);
                        var hmac256forlogin = ComputeHMACSHA256(login_byte, pas_byte);
                        var hmac256forpas = ComputeHMACSHA256(pas_byte, login_byte);
                        dataLogin[q] = Convert.ToBase64String(hmac256forlogin);
                        dataPassword[q] = Convert.ToBase64String(hmac256forpas);
                        q++;
                        Console.WriteLine("\n\nDone.");
                        break;
                    case 2:
                        Console.WriteLine("Verification\n\n");
                        Console.WriteLine("Enter your login:");
                        string login_verify = Console.ReadLine();
                        Console.WriteLine("Enter the password:");
                        string pas_verify = null;
                        while (true)
                        {
                            var key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.Enter)
                                break;
                            pas_verify += key.KeyChar;
                        }
                        byte[] login_verify_byte = Encoding.Unicode.GetBytes(login_verify);
                        byte[] pas_verify_byte = Encoding.Unicode.GetBytes(pas_verify);
                        var hmac256forverifylogin = ComputeHMACSHA256(login_verify_byte, pas_verify_byte);
                        var hmac256forverifypas = ComputeHMACSHA256(pas_verify_byte, login_verify_byte);
                        int index_login = Array.IndexOf(dataLogin, Convert.ToBase64String(hmac256forverifylogin));
                        int index_pas = Array.IndexOf(dataPassword, Convert.ToBase64String(hmac256forverifypas));
                        if (dataLogin.Contains(Convert.ToBase64String(hmac256forverifylogin)) && dataPassword.Contains(Convert.ToBase64String(hmac256forverifypas)) && index_login == index_pas)
                        {
                            Console.WriteLine("\nSuch user exists. SUCCESS!\n\n");
                        }
                        else
                        {
                            Console.WriteLine("\n\nERROR!!! USER DOESN'T EXIST!\n\n");
                        }
                        break;

                    case 3:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Choose an option from the list");
                        break;
                }
            } while (true);

        }
        public static byte[] ComputeHMACSHA256(byte[] dataforhmac, byte[] pas)
        {
            using (var hmac256 = new HMACSHA256(pas))
            {
                return hmac256.ComputeHash(dataforhmac);
            }
        }
    }
}
