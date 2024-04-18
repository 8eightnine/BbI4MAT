using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5
{
    internal class FileName
    {
        //static double NewtonCotesIntegration(double a, double b, int k)
        //{
        //    double h = (b - a) / k;
        //    double sum = 0;
        //    for (int i = 0; i < k; i++)
        //    {
        //        double x = a + i * h;
        //        sum += 3 * h * (Func(x) + 3 * Func((x + h)) + 3 * Func((x + 2 * h)) + Func(x + 3 * h)) / 8;
        //    }
        //    return sum;
        //}

        //static double NewtonCotes2(double epsilon, double a, double b)
        //{
        //    int k = 3; // Начальное количество интервалов
        //    double h = (b - a) / k;
        //    double Sh = NewtonCotesIntegration(a, b, k); // Интеграл с начальным количеством интервалов
        //    while (true)
        //    {
        //        k *= 2; // Увеличиваем количество интервалов вдвое
        //        h /= 2;
        //        double Shr = NewtonCotesIntegration(a, b, k); // Интеграл с удвоенным количеством интервалов
        //        if (RungeRule(Sh, Shr, epsilon, 2, 4)) // Проверяем правило Рунге
        //        {
        //            Console.Write("Шаг остановки = " + h);
        //            return Shr; // Если правило выполняется, возвращаем значение интеграла
        //        }
        //        Sh = Shr; // Если не выполняется, сохраняем новое значение интеграла для следующей итерации
        //    }
        //}

    }
}
