using System;
using System.Security.Cryptography;
using System.Text;

namespace hash
{
    class Program
    {
        public static void Main()
        {
            String correcthash = "po1MVkAE7IjUUwu61XxgNg=="; //заданий хеш
            String attempthash = ""; //

            int counter = 0;
            while (attempthash != correcthash) //якщо хеш не співпадає - запускаємо цикл
            {
                byte[] messageArray = Encoding.Unicode.GetBytes(counter.ToString());
                messageArray = ComputeHashMd5(messageArray); //обчислюємо хеш для числа у циклі
                attempthash = Convert.ToBase64String(messageArray); //зберігаємо хеш у змінній для того щоб цикл працював
                counter++; //перевіряємо наступне значення
            }
            Console.WriteLine(counter - 1 + " is password."); //віднімаємо одиницю, бо остання ітерація пройшла в циклі, тому counter = 20192021, хоча цикл зупинився на паролі 20192020

            static byte[] ComputeHashMd5(byte[] input)
            {
                var md5 = MD5.Create();
                return md5.ComputeHash(input);

            }

        }
    }
}