using System.Security.Cryptography;
using System.Text;
using pz6_2;

namespace pz6_2
{
    class Program
     {
         static void Main()
         {
            string publicKey_Path = @"E:\Study KNU\III\Інформаційна безпека\Практичні\Практичне завдання 6\pz6\pz6_2\key_folder\";
            string cypher_Path = @"E:\Study KNU\III\Інформаційна безпека\Практичні\Практичне завдання 6\pz6\pz6_2\enc_folder\";
            string enc_Path = @"E:\Study KNU\III\Інформаційна безпека\Практичні\Практичне завдання 6\pz6\pz6_2\enc_folder\";
            string our_path;

            string key_ext = ".xml";
            string enc_ext = ".txt";

            var RSA = new RSA();
            do
             {   
                 
                Console.WriteLine("\nMenu");
                Console.WriteLine("1. Generate keys ");
                Console.WriteLine("2. Encrypt message ");
                Console.WriteLine("3. Decrypt message ");
                Console.WriteLine("4. Clear private key ");
                Console.WriteLine("5. Exit ");

                Console.WriteLine("\nEnter your number:\n");
                string c = Console.ReadLine();
                switch (c)
                {
                    case "1":
                        //генерація ключів
                        string textone = "yurii_cherevach";
                        our_path = publicKey_Path + textone + key_ext;
                        RSA.AssignNewKey(our_path);
                        Console.WriteLine("Done");
                        break;

                    case "2":
                        //шифрування
                        byte[] texttwo = Encoding.Unicode.GetBytes("moscow delenda est");
                        RSA.GetKey($"{publicKey_Path}/yurii_cherevach{key_ext}", null);

                        byte[] encryption = RSA.EncryptData(texttwo);
                        File.WriteAllBytes($"{enc_Path}/yurii_cherevach{enc_ext}", encryption);

                        Console.WriteLine($"Text: {Encoding.Unicode.GetString(texttwo)}");
                        Console.WriteLine($"Encrypted: {Convert.ToBase64String(encryption)}");
                        break;
                    case "3":
                        //дешифрування
                        byte[] encrText = File.ReadAllBytes($"{cypher_Path}/yurii_cherevach{enc_ext}");
                        byte[] decryption = RSA.DecryptData(encrText);

                        Console.WriteLine($"Decrypted: {Encoding.Unicode.GetString(decryption)}");
                        break;
                    case "4":
                        //видалити ключі
                        RSA.DeleteKeyInCsp();
                        Console.WriteLine("Keys deleted.");
                        break;
                    case "5":
                        //вихід
                        Environment.Exit(0);
                        break;
                }
             } while (true);
         }
     }
    
}
