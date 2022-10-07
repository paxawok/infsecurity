using System.Security.Cryptography;
using System;
using System.IO;
using System.Text;

class Programm
{
    public static void Main(string[] args)
    {
        string path = @"E:\Study KNU\III\Інформаційна безпека\Практичні\Практичне завдання 2\Practice2\pz_2_text.txt";
        byte[] pz2_txt = File.ReadAllBytes(@"E:\Study KNU\III\Інформаційна безпека\Практичні\Практичне завдання 2\Practice2\pz_2_text.txt").ToArray();

        Console.WriteLine("|__________________________________________|");
        Console.WriteLine("Enter 1 for encryption or 2 for decryption: ");
        string z = Console.ReadLine();
        

        if(z == "1")
        {
            Console.WriteLine("Enter password: ");
            string passwrd = Console.ReadLine();

            byte[] secret = Encoding.UTF8.GetBytes(passwrd);
            byte[] data = File.ReadAllBytes(path).ToArray();
            int lenght = data.Length;
            byte[] encrypted_ = new byte[lenght];

            for (int i = 0; i < lenght; i++)
            {
                encrypted_[i] = (byte)(data[i] ^ secret[i % secret.Length]);
            }            
            File.WriteAllBytes(path + "_encr.dat", encrypted_);
            Console.WriteLine(" File is encrypted.\n");

        } else if(z == "2")
        {
            Console.WriteLine("Enter password: ");
            string passwrd = Console.ReadLine();

            byte[] secret = Encoding.UTF8.GetBytes(passwrd);
            byte[] data = File.ReadAllBytes(path + "_encr.dat").ToArray();
            int lenght = data.Length;
            byte[] decrypted_ = new byte[lenght];
            for(int i = 0; i < lenght; i++)
            {
                decrypted_[i] = (byte)(data[i] ^ secret[i % secret.Length]);
            }
            File.WriteAllBytes(path + "_decr.txt", decrypted_);
            Console.WriteLine(" File is decrypted. \n");
            
        }
    }
}