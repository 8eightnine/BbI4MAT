using System;

namespace YourNamespace
{
    class Program
    {
        public const int n = 1000;

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

        // Метод правых прямоугольников
        static double RightRec(double epsilon, double r, double h, double a, double b)
        {
            double sum = 0;
            double sumResult = 0;
            double x = a + h;

            // Первичный подсчет
            while (x <= b + h)
            {
                sum += Func(x) * h;
                x += h;
            }

            // Процесс уменьшения шага для достижения заданной точности
            while (!RungeRule(sum, sumResult, epsilon, r, 1)) // Порядок точности равен 1
            {
                x = a + h;
                sum = sumResult;
                sumResult = 0;
                h /= r;
                while (x <= b + h)
                {
                    sumResult += Func(x) * h;
                    x += h;
                }
            }
            Console.Write("Шаг остановки = " + h);
            return sumResult;
        }

        // Метод средних прямоугольников
        static double MiddleRec(double epsilon, double r, double h, double a, double b)
        {
            double sum = 0;
            double sumResult = 0;
            double x = (a + b) / 2;

            // Первичный подсчет
            while (x <= b)
            {
                sum += Func(x) * h;
                x += h;
            }

            //Процесс уменьшения шага для достижения заданной точности
            while (!RungeRule(sum, sumResult, epsilon, r, 1)) // Порядок точности равен 1
            {
                x = (a + a + h) / 2;
                sum = sumResult;
                sumResult = 0;
                h /= r;
                while (x <= b)
                {
                    sumResult += Func(x) * h;
                    x += h;
                }
            }
            Console.Write("Шаг остановки = " + h);
            return sumResult;
        }

        // Метод Ньютона-Котеса
        static double NewtonCotes(double epsilon, double a, double b)
        {
            double h = (b - a) / (3 * 10);
            double sum = 0;
            double sumResult = 0;
            do
            {
                sum = sumResult;
                sumResult = Func(a) + Func(b);

                for (int i = 1; i <= 3 * n - 1; i++)
                {
                    double x = a + h * i;
                    if (i % 3 == 0)
                        sumResult = sumResult + 2 * Func(x);
                    else
                        sumResult = sumResult + 3 * Func(x);
                }

                sumResult = (3.0 / 8.0) * sumResult * h;
            }
            while (!RungeRule(sum, sumResult, epsilon, 3, 4));
            Console.Write("Шаг остановки = " + h);
            return sumResult;
        }

        static double NewtonCotesIntegration(double a, double b, int k)
        {
            double h = (b - a) / k;
            double sum = 0;
            for (int i = 0; i < k; i++)
            {
                double x = a + i * h;
                sum += 3 * h * (Func(x) + 3 * Func((x + h)) + 3 * Func((x + 2 * h)) + Func(x + 3 * h)) / 8;
            }
            return sum;
        }

        static double NewtonCotes2(double epsilon, double a, double b)
        {
            int k = 3; // Начальное количество интервалов
            double h = (b - a) / k;
            double Sh = NewtonCotesIntegration(a, b, k); // Интеграл с начальным количеством интервалов
            while (true)
            {
                k *= 2; // Увеличиваем количество интервалов вдвое
                h /= 2;
                double Shr = NewtonCotesIntegration(a, b, k); // Интеграл с удвоенным количеством интервалов
                if (RungeRule(Sh, Shr, epsilon, 2, 4)) // Проверяем правило Рунге
                {
                    Console.Write("Шаг остановки = " + h);
                    return Shr; // Если правило выполняется, возвращаем значение интеграла
                }
                Sh = Shr; // Если не выполняется, сохраняем новое значение интеграла для следующей итерации
            }
        }

        static void Main(string[] args)
        {
            Console.Clear();

            double a = 5;
            double b = 15;
            double eps = 1e-4;

            double h = (b - a);
            const double r = 2;

            double result;
            Console.WriteLine("Метод левых прямоугольников:\n");
            result = RightRec(eps, r, h, a, b);
            Console.WriteLine("\nРезультат: " + result + "\n");

            Console.WriteLine("Метод центральных прямоугольников:\n");
            result = MiddleRec(eps, r, h, a, b);
            Console.WriteLine("\nРезультат: " + result + "\n");

            Console.WriteLine("Метод Ньютона-Котеса 3-го порядка:\n");
            //result = NewtonCotes(eps, r, a, b, n);
            result = NewtonCotes2(eps, a, b);
            Console.WriteLine("\nРезультат: " + result + "\n");
        }
    }
}
