using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5
{
    internal class Task_3
    {
        // Функция
        static double Func(double x)
        {
            return (1 + x) / (Math.Pow((2 + 3 * x), 2) * Math.Sqrt(2 + x));
        }

        // Правило Рунге
        static bool RungeRule(double Sh, double Shr, double epsilon, double r, double p)
        {
            return (Math.Abs(Sh - Shr) / (Math.Pow(r, p) - 1)) < epsilon;
        }

        static double Simpson(double a, double b)
        {
            double h = (b - a) / 3;
            // Две дополнительные точки (n+1), n = 3
            double mid = a + h;
            double mid2 = mid + h;
            // Результат минус погрешность (3/80*h^5)
            double res = (3 * h / 8) * (Func(a) + 3 * Func(mid) + 3 * Func(mid2) + Func(b)) - 3 * Math.Pow(h, 5) / 80;
            return res;
        }

        static void Main2()
        {
            double result = 0, a = 5, b = 15, sum = 0;
            double intervalCount = 1;
            int k = 1;

            double functionIncrement = (b - a) / intervalCount;

            // Первичный подсчет
            for (int i = 0; i < intervalCount; i++)
            {
                double low = a + i * functionIncrement;
                double up = low + functionIncrement;
                result += Simpson(low, up);
                //Console.WriteLine(low + " " + up);
            }

            while (!RungeRule(sum, result, 1e-3, 2, 4) && k < 20)
            {
                sum = result;
                result = 0;
                intervalCount *= 2;
                functionIncrement /= 2;
                k++;
                for (int i = 0; i < intervalCount; i++)
                {
                    double low = a + i * functionIncrement;
                    double up = low + functionIncrement;
                    result += Simpson(low, up);
                }
            }
            Console.WriteLine("Шаг остановки = " + (b - a) / intervalCount);
            Console.WriteLine("Результат = " + result + " \nk = " + k);
        }

    }
}
