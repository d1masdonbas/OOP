using System;
using System.Threading;

namespace Formula
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter num of exercise:");
            int x = Convert.ToInt32(Console.ReadLine());

            switch (x)
            {
                case 123:
                    zavd1();
                    Thread.Sleep(2000);
                    for (int i = 3; i >= 0; --i)
                    {
                        Console.WriteLine(i);
                        Thread.Sleep(1000);
                    }
                    goto case 321;
                case 321:
                    zavd2();
                    Thread.Sleep(2000);
                    for (int i = 3; i >= 0; --i)
                    {
                        Console.WriteLine(i);
                        Thread.Sleep(1000);
                    }
                    goto case 213;
                case 213:
                    zavd3();
                    Thread.Sleep(2000);
                    Console.WriteLine("Great job, Mr Susla");
                    Thread.Sleep(2000);
                    Console.WriteLine("Have a nice day, and good luck!");
                    break;
                default:
                    Console.WriteLine("Wrong number, Mr. Susla");
                    Thread.Sleep(2000);
                    Console.WriteLine("Let`s play a game, Mr. Susla");
                    Thread.Sleep(2000);
                    for (int i = 3; i >= 0; --i)
                    {
                        Console.WriteLine(i);
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine("Okay, go to first exercise :)");
                    goto case 123;
            }

        }

        static void zavd1()
        {
            Console.WriteLine("Enter x:");
            double x = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter z:");
            double z = Convert.ToDouble(Console.ReadLine());  

            double y = (2 * Math.Pow(x, 2) + x - 5) / (x + 2) + (1.0 / Math.Tan(x)) * x / (2 * z);

            Console.WriteLine("Результат обчислення виразу y: " + y);
        }

        static void zavd2()
        {
            Console.WriteLine("The pairs of friendly numbers from 200 to 300:");

            for (int num1 = 200; num1 <= 300; num1++)
            {
                int num2 = SumOfDivisors(num1); 

                if (num1 < num2 && num2 >= 200 && num2 <= 300)
                {
                   
                    Console.WriteLine($"({num1}, {num2})");
                }
            }
        
            int SumOfDivisors(int num)
            {
                int sum = 1; 

                for (int i = 2; i <= Math.Sqrt(num); i++)
                {
                    if (num % i == 0)
                    {
                        sum += i;
                        if (i != (num / i))
                        {
                            sum += (num / i);
                        }
                    }
                }

                return sum;
            }
        }

        static void zavd3()
        {
            Console.WriteLine("Enter y:");

            double y = Convert.ToDouble(Console.ReadLine());

            double result = (1.7 * t(0.25) + 2 * t(1 + y)) / (6 - t(Math.Pow(y, 2) - 1));

            Console.WriteLine(result);

            double t(double a)
            {
                double sum1 = 0, sum2 = 0;

                for (int k = 0; k <= 10; k++)
                {
                    sum1 += Math.Pow(a, 2 * k + 1) / Factorial(2 * k + 1);
                }

                for (int k = 0; k <= 10; k++)
                {
                    sum2 += Math.Pow(a, 2 * k + 1) / Factorial(2 * k);
                }

                double Factorial(int n)
                {
                    if (n == 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return n * Factorial(n - 1);
                    }
                }

                return a;
            }
        }
    }
}
