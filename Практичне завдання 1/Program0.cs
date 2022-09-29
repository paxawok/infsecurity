using System;

public class Practice
{
    public static void Main()
    {
        Random random = new Random(1234);
        for (int y = 0; y < 10; y++)
        {
            Console.WriteLine("{0,3} ", random.Next(1, 10));
        } 
    }
}
