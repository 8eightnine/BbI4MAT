namespace YourNamespace
{
    class Program
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
            while (!RungeRule(sum, sumResult, epsilon, r, 2)) // Порядок точности равен 1
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
        static double Simpson(double a, double b)
        {
            double h = (b - a) / 3;
            // Две дополнительные точки (n+1), n = 3
            double mid = a + h;
            double mid2 = mid + h;
            double res = (3 * h / 8) * (Func(a) + 3 * Func(mid) + 3 * Func(mid2) + Func(b)) - 3 * Math.Pow(h, 5) / 80;
            return res;
        }

        static double NewtonCotes3(double a, double b, double r, double eps)
        {
            double result = 0, sum = 0;
            double intervalCount = r;
            int k = 1;

            double h = (b - a) / intervalCount;

            // Первичный подсчет
            for (int i = 0; i < intervalCount; i++)
            {
                double low = a + i * h;
                double up = low + h;
                result += Simpson(low, up);
                //Console.WriteLine(low + " " + up);
            }

            while (!RungeRule(sum, result, eps, 2, 4) && k < 20)
            {
                sum = result;
                result = 0;
                intervalCount *= 2;
                h /= 2;
                k++;
                for (int i = 0; i < intervalCount; i++)
                {
                    double low = a + i * h;
                    double up = low + h;
                    result += Simpson(low, up);
                }
            }
            Console.Write("Шаг остановки = " + h);
            return result;
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
            Console.WriteLine("Метод правых прямоугольников:\n");
            result = RightRec(eps, r, h, a, b);
            Console.WriteLine("\nРезультат: " + result + "\n");

            Console.WriteLine("Метод центральных прямоугольников:\n");
            result = MiddleRec(eps, r, h, a, b);
            Console.WriteLine("\nРезультат: " + result + "\n");

            Console.WriteLine("Метод Ньютона-Котеса 3-го порядка:\n");
            result = NewtonCotes3(a, b, r, eps);
            Console.WriteLine("\nРезультат: " + result);
        }
    }
}
