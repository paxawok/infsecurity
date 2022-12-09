using System.Security.Cryptography;
using System.Text;
using pz6_1;

namespace pz6_1
{
    class Program
     {
         static void Main()
         {
             do
             {
                Console.WriteLine("\nEnter your text:\n");
                 string c = Console.ReadLine();
                 RSA.AssignNewKey();

                 byte[] textfromuser = Encoding.Unicode.GetBytes(c);

                 byte[] encryption = RSA.EncryptData(textfromuser);
                 byte[] decryption = RSA.DecryptData(encryption);

                 Console.WriteLine($"\nOriginal text:     {c}");
                 Console.WriteLine($"Encrypted text:    \n\n{Convert.ToBase64String(encryption)}\n");
                 Console.WriteLine($"Decrypted text:    {Encoding.Unicode.GetString(decryption)}");
             } while (true);
         }
     }
    
}
