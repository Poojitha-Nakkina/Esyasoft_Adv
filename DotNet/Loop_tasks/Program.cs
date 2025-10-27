namespace Loop_tasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Task-1 
            //Console.WriteLine("Squares");
            //for (int i = 1; i <= 10; i++)
            //{
            //    Console.WriteLine(i * i);
            //}
            //Console.WriteLine("Cubes");
            //for (int i = 1; i <= 10; i++)
            //{
            //    Console.WriteLine(Math.Pow(i, 3));
            //}

            ////Task-2

            //for (int i = 1; i <= 1000; i++)
            //{
            //    int sum = 0;
            //    for (int j = 1; j < i; j++)
            //    {
            //        if (i % j == 0)
            //            sum += j;
            //    }

            //    if (sum == i)
            //        Console.WriteLine(i);
            //}


            //Task-3
            //int n = 5;
            //for (int i = 0; i < (n / 2) + 1; i++)
            //{
            //    for (int j = 0; j < i; j++)
            //    {
            //        Console.Write(" ");
            //    }
            //    for (int k = 0; k < n - 2 * i; k++)
            //    {
            //        Console.Write("*");

            //    }
            //    Console.WriteLine();
            //}

            //for (int i = n / 2 - 1; i >= 0; i--)
            //{
            //    for (int j = 0; j < i; j++)
            //    {
            //        Console.Write(" ");
            //    }
            //    for (int k = 0; k < n - 2 * i; k++)
            //    {
            //        Console.Write("*");

            //    }
            //    Console.WriteLine();
            //}

            //Task-4

            //int p = 5;
            //for(int i = 1; i <= p; i++)
            //{
            //    for(int j = 1; j <=p-i; j--)
            //    {
            //        Console.Write("");
            //    }
            //    for(int k = 1; k <= i; k++)
            //    {
            //        Console.Write(k);
            //    }
            //    for(int t = i - 1; t > 0; t--)
            //    {
            //        Console.Write(t);
            //    }
            //    Console.WriteLine();

            //}

            ////TASK-5
            //int n = 5;

            //for (int i = 1; i <= n; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        for (int j = 1; j <= i; j++)
            //        {
            //            if (j % 2 == 0)
            //            {
            //                Console.Write("1");
            //            }
            //            else
            //            {
            //                Console.Write("0");
            //            }
            //        }
            //        Console.WriteLine();
            //    }
            //    else
            //    {
            //        for (int j = 1; j <= i; j++)
            //        {
            //            if (j % 2 == 0)
            //            {
            //                Console.Write("0");
            //            }
            //            else
            //            {
            //                Console.Write("1");
            //            }
            //        }
            //        Console.WriteLine();

            //    }

            //}

            //Task-6 

            //for (int i = 100; i < 1000; i++)
            //{
            //    int num = i;
            //    int sum = 0;
            //    while (num > 0)
            //    {
            //        int rem = num % 10;
            //        sum += rem * rem * rem;
            //        num = num / 10;
            //    }
            //    if (sum == i)
            //    {
            //        Console.WriteLine($"number is {i}");
            //    }

            //}

            //Task-7
            //int[] fibonnaci = new int[10];
            //fibonnaci[0] = 0;
            //fibonnaci[1] = 1;

            //for(int i = 2; i < 10; i++)
            //{
            //    fibonnaci[i]= fibonnaci[i-1]+fibonnaci[i-2];
            //}

            //for(int i = 9; i >= 0; i--)
            //{
            //    Console.WriteLine(fibonnaci[i]);
            //}

            //Task-9

            //Console.WriteLine("Enter num:");
            //int num= Convert.ToInt32(Console.ReadLine());
            //int total = 0;
            //while (num > 0)
            //{
            //    total++;
            //    num = num / 10;
            //}
            //Console.WriteLine(total);


            //Task-10
            int p = 5;
            for (int i = 1; i <= p; i++)
            {
                for (int j = 1; j <= p - i; j++)
                {
                    Console.Write(" ");
                }
                for (int k = 1; k <= i; k++)
                {
                    Console.Write(k);
                }
                for (int t = i - 1; t > 0; t--)
                {
                    Console.Write(t);
                }
                Console.WriteLine();

            }
            for(int i = p - 1; i > 0; i--)
            {
                for(int j = 1; j <= p - i; j++)
                {
                    Console.Write(" ");
                }
                for(int k = 1; k <= i; k++)
                {
                    Console.Write(k);
                }
                for(int t = i - 1; t > 0; t--)
                {
                    Console.Write(t);
                }
                Console.WriteLine();
            }
        }
    }
}
