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
        static double NewtonCotes(double epsilon, double r, double h, double a, double b)
        {
            double k = 10;
            h /= (3 * k);
            double sum = 0;
            int counter = 0;
            double sumResult = 0;
            do
            {
                sum = sumResult;
                sumResult = Func(a) + Func(b);

                for (int i = 1; i <= 3 * k - 1; i++) // k-1 = 0 Антох тут же ошибка, не?
                {
                    double x = a + h * i;
                    if (i % 3 == 0)
                        sumResult = sumResult + 2 * Func(x);
                    else
                        sumResult = sumResult + 3 * Func(x);
                }
                k *= r;
                sumResult = (3.0 / 8.0) * sumResult * h;
                Console.WriteLine($"Counter: {counter}, result: {sumResult}");
                counter++;
            }
            while (!RungeRule(sum, sumResult, epsilon, 3, 4));
            Console.WriteLine(counter);
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
                sum += h * (Func(x) + 3 * Func((x + h / 3)) + 3 * Func((x + 2 * h / 3)) + Func(x + h)) / 8;
            }
            return sum;
        }

        static double NewtonCotes3(double a, double b, int n)
        {
            double h = (b - a) / n;
            double integral = 0;

            for (int i = 0; i < n; i += 3)
            {
                double x0 = a + h * i;
                double x1 = a + h * (i + 1);
                double x2 = a + h * (i + 2);
                double x3 = a + h * (i + 3);

                integral += (3 * h / 8) * (Func(x0) + 3 * Func(x1) + 3 * Func(x2) + Func(x3));
            }

            return integral;
        }

        static double IntegralWithAccuracy(double a, double b, double epsilon)
        {
            int n = 3; // начальное количество интервалов
            double r = 2; // коэффициент уменьшения шага
            double p = 4; // порядок точности метода
            int count = 0;

            double Sh = NewtonCotes3(a, b, n);
            double Shr;

            do
            {
                n *= (int)r;
                Shr = NewtonCotes3(a, b, n);
                count++;
            }
            while (!RungeRule(Sh, Shr, epsilon, r, p));
            Console.WriteLine(count);
            return Shr;
        }

        static double NewtonCotes2(double epsilon, double a, double b)
        {
            int k = 3; // Начальное количество интервалов
            double h = (b - a) / k;
            int counter = 0;
            double Sh = NewtonCotesIntegration(a, b, k); // Интеграл с начальным количеством интервалов
            while (true)
            {
                double Shr = NewtonCotesIntegration(a, b, k); // Интеграл с удвоенным количеством интервалов
                k *= 2; // Увеличиваем количество интервалов вдвое
                h /= 2;
                if (RungeRule(Sh, Shr, epsilon, 2, 4)) // Проверяем правило Рунге
                {
                    Console.Write("Шаг остановки = " + h);
                    Console.WriteLine("Counter = " + counter);
                    return Shr; // Если правило выполняется, возвращаем значение интеграла
                }
                counter++;
                Sh = Shr; // Если не выполняется, сохраняем новое значение интеграла для следующей итерации
            }
        }

        static void Main(string[] args)
        {
            Console.Clear();

            double a = 5;
            double b = 15;
            double eps = 1e-3;

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
            result = NewtonCotes(eps, r, h, a, b);
            //5result = IntegralWithAccuracy(a, b, eps);
            Console.WriteLine("\nРезультат: " + result + "\n");
        }
    }
}
