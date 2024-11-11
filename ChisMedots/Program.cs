using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChisMedots
{
    internal class Program
    {
        static double Function(double x)
        {
            return 3 * Math.Sin(x) - x + 3;
        }
        static void Main(string[] args)
        {
            int methods;
            double accuracy, x0 = 0, x = 0, a = 0, b = 0, x1 = 0, x2 = 0, min = 0;


            Console.WriteLine("Выберите метод который будет использоваться");
            Console.WriteLine("0 - Метод дихотомии\n1 - Метод итераций\n2 - Метод Касательных\n3 - Метод хорд и секущих");
            methods = Convert.ToInt32(Console.ReadLine());


            switch (methods)
            {
                //Метод дихотомии
                case 0:
                    {
                        Console.WriteLine("Введите погрешность: ");
                        accuracy = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Введите первую границу: ");
                        a = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите вторую границу: ");
                        b = Convert.ToDouble(Console.ReadLine());

                        while (b - a >= 2 * accuracy)
                        {
                            x1 = (a + b - accuracy) / 2;
                            x2 = (a + b + accuracy) / 2;

                            if (Function(x1) <= Function(x2))
                            {
                                b = x2;
                            }
                            else
                            {
                                a = x1;
                            }
                        }
                        x = (a + b) / 2;
                        min = Function(x);

                        Console.WriteLine($"Минимум: x = {x}, f(x) = {min}");
                    }
                break;

                    // Метод итераций
                case 1:
                    {
                        Console.WriteLine("Введите x0: ");
                        x0 = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите погрешность: ");
                        accuracy = Convert.ToDouble(Console.ReadLine());
                        x1 = Function(x0);
                        while (Math.Abs(x1 - x0) > accuracy)
                        {
                            x0 = x1;
                            x1 = Function(x0);
                        }
                        x = x1;
                        Console.WriteLine("x = " + x);
                    }
                    break;
                
                    // Метод секущих
                case 2:
                    {
                        Console.WriteLine("Введите первое приближение x0: ");
                        x0 = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите второе приближение x1: ");
                        x1 = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите погрешность: ");
                        accuracy = Convert.ToDouble(Console.ReadLine());

                       

                        while (Math.Abs(x1 - x0) > accuracy)
                        {
                            double fx0 = Function(x0);
                            double fx1 = Function(x1);
                            x2 = x1 - fx1 * (x1 - x0) / (fx1 - fx0);

                            x0 = x1;
                            x1 = x2;
                        }

                        Console.WriteLine($"Корень x = {x1}");
                        
                    }
                    break;



                    //Метод хорд и секущих
                case 3:
                    {
                        Console.WriteLine("Введите a: ");
                        a = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите b: ");
                        b = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите погрешность: ");
                        accuracy = Convert.ToDouble(Console.ReadLine());
                        

                        double h = (b - a) / 100;
                        x = a;
                        double y = a + h;

                        double m = (Math.Abs(Function(y) - Function(x))) / h;
                        double c = 0;
                        while (y < b)
                        {
                            x = y;
                            y = x + h;
                            c = (Math.Abs(Function(y) - Function(x))) / h;

                            if (c > m)
                            {
                                m = c;
                            }
                        }

                        m = m + 1;

                        x0 = (a + b) / 2;

                        x1 = x0 - Function(x0) / m;

                        int maxIterations = 10000; 
                        int iterations = 0;

                        while (Math.Abs(x1 - x0) > accuracy && iterations < maxIterations)
                        {
                            x0 = x1;
                            x1 = x0 - Function(x0) / m;
                            iterations++;
                        }

                        x = x1;
                                              
                            Console.WriteLine("x = " + x);
                        
                    }
                    break;

            }




        }
    }
}
