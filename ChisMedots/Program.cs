using MathNet.Symbolics;
using System;
using Expr = MathNet.Symbolics.SymbolicExpression;
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
        public static float accuracy = 0, x0 = 0, x = 0, a = 0, b = 0, x1 = 0, x2 = 0, min = 0;
        public static int methods;
        //static float Function(float x)
        //{
        //    return (float)(3 * Math.Sin(x) - x + 3);
        //}

        static float Function(float x)
        {
            return (float)(Math.Log(x) + x - 2);
        }

        static float Derivative(float x, float accuracy)
        {
            float h = accuracy; // Шаг для численного дифференцирования
            return (Function(x + h) - Function(x - h)) / (2 * h);
        }


        //static float Function(float x)
        //{
        //    return (float)Math.Pow(x, 2) - 2;
        //}

        ////Для итераций 1 1e-6
        //static float Function(float x)
        //{
        //    return (float)(x + 2 / x) / 2;
        //}



        static void Main(string[] args)
        {

            Console.WriteLine("f(x) = Log(x) + x - 2");
            Console.WriteLine("Выберите метод который будет использоваться");
            Console.WriteLine("1 - Метод дихотомии\n2 - Метод итераций\n3 - Метод Касательных\n4 - Метод хорд");
            methods = Convert.ToInt32(Console.ReadLine());
            inputData();
            executingMethods();

            Console.WriteLine($"x = {Math.Round(x, countNum(accuracy))}");



        }

        static int countNum(float accuracy)
        {
            string str = accuracy.ToString(new System.Globalization.NumberFormatInfo() { NumberDecimalSeparator = "," });
            return str.Contains(",") ? str.Remove(0, Math.Truncate(accuracy).ToString().Length + 1).Length : 0;
        }

        static void inputData()
        {
            switch (methods)
            {
                case 1:
                case 3:
                case 4:
                    Console.WriteLine("Введите a: ");
                    a = float.Parse(Console.ReadLine());
                    Console.WriteLine("Введите b: ");
                    b = float.Parse(Console.ReadLine());
                    Console.WriteLine("Введите e: ");
                    accuracy = float.Parse(Console.ReadLine());
                    break;

                case 2:
                    Console.WriteLine("Введите x0: ");
                    x0 = float.Parse(Console.ReadLine());
                    Console.WriteLine("Введите e: ");
                    accuracy = float.Parse(Console.ReadLine());
                    break;
            }
        }

        static void executingMethods()
        {

            switch (methods)
            {

                //Метод дихотомии
                case 1:
                    {
                        while (b - a > accuracy)
                        {
                            x = (a + b) / 2;

                            if (Function(a) * Function(x) < 0)
                            {
                                b = x;
                            }
                            else
                            {
                                a = x;
                            }
                        }
                        x = (a + b) / 2;
                        
                    }
                    break;


                // Метод итераций
                case 2:
                    {
                        x1 = Function(x0);
                        while (Math.Abs(x1 - x0) > accuracy)
                        {
                            x0 = x1;
                            x1 = Function(x0);
                        }
                        x = x1; 
                    }
                    break;

                // Метод касательных
                case 3:
                    {
                         float h = (b - a) / 100; 
                         x = Function(a); 
                            float y = Function(a + 2 * h) - 2 * Function(a + h) + Function(a); 

                         x0 = (x * y > 0) ? a : b; 

                         x1 = x0 - Function(x0) / Derivative(x0, accuracy);

                        
                        while (Math.Abs(x1 - x0) > accuracy)
                        {
                            x0 = x1;
                            x1 = x0 - Function(x0) / Derivative(x0, accuracy);
                        }
                        x = x1;
                    }
                    break;



                //Метод хорд
                case 4:
                    {
                        float h = (b - a) / 100;
                        x = a;
                        float y = a + h;

                        float m = (Math.Abs(Function(y) - Function(x))) / h;
                        float c = 0;
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



                        while (Math.Abs(x1 - x0) > accuracy)
                        {
                            x0 = x1;
                            x1 = x0 - Function(x0) / m;
                        }

                        x = x1;

                        
                    }
                    break;
            

            }
        }
    }
}
