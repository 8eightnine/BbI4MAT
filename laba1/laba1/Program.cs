class Program
{
    static float eps = 1e-2f;

    static float fx(float x)
    {
        return (float)Math.Pow(2, x) - 5 * (float)Math.Pow(x, 2) + 10;
    }

    static float dfx(float x)
    {
        return (float)Math.Log(2) * (float)Math.Pow(2, x) - 10 * x;
    }

    static float ddfx(float x)
    {
        return (float)Math.Pow(Math.Log(2), 2) * (float)Math.Pow(2, x) + 10;
    }

    static float gx(float x)
    {
        float result = (float)(Math.Pow(2, x) + 10) / 5;
        return (float)Math.Sqrt(result);
    }

    static float dgx(float x)
    {
        float result = (float)(Math.Sqrt(5) * Math.Log(2) * Math.Pow(2, x - 1));
        result = result / (float)(5 * Math.Sqrt(Math.Pow(2, x) + 10));
        return result;
    }

    static void NewtonMethod(float a, float b)
    {
        float x = 0;

        // Проверяем знаки первой производной на интервале
        if ((Math.Sign(dfx(a)) != Math.Sign(dfx(a))) && (Math.Sign(dfx(a)) != Math.Sign(dfx(a))))
        {
            return;
        }
        // Проверяем знаки второй производной на интервале
        if ((Math.Sign(ddfx(a)) != Math.Sign(ddfx(a))) && (Math.Sign(ddfx(a)) != Math.Sign(ddfx(a))))
        {
            return;
        }

        if (fx(a) * ddfx(a) > 0) // если знаки функций совпадают
        {
            x = a;
        }
        else if (fx(b) * ddfx(b) > 0) // если знаки функций совпадают
        {
            x = b;
        }
        else
        {
            Console.WriteLine("Ошибка: не соблюдается условие совпадаемости знаков f(x) и f''(x)\n");
            return;
        }

        Console.WriteLine($"Начальное приближение равно: {x}");

        int iterations = 1;

        while (iterations < 1000)
        {
            float y1 = fx(x); float y2 = dfx(x);
            float dx = y1 / y2;
            x -= dx;

            Console.WriteLine($"Итерация №{iterations}, x = {x}");

            iterations++;

            if (Math.Abs(dx) < eps)
            {
                Console.WriteLine($"Решение x = {x:0.000000} найдено за {iterations} итераций\n");
                break;
            }
        }
    }

    static void HordeMethod(float a, float b)
    {
        int iterations = 0;
        while (Math.Abs(b - a) > eps)
        {
            iterations++;
            a = b - (b - a) * fx(b) / (fx(b) - fx(a));
            b = a - (a - b) * fx(a) / (fx(a) - fx(b));
            Console.WriteLine($"Итерация №{iterations}, x = {b}");
        }
        Console.WriteLine($"Решение x = {b:0.000000} найдено за {iterations} итераций\n");
    }

    public static void SimpleIterMethod(float x0, float M)
    {
        int count = 1;
        float x1 = x0;

        while (true)
        {
            float l = -1 / M;
            x1 = x0 + l * fx(x0);
            Console.WriteLine("Итерация номер: " + count++ + ", Вычисленный корень: " + x1);
            if (Math.Abs(x1 - x0) < eps)
                break;
            x0 = x1;
        }
    }

    static void Main(string[] args)
    {
        float a = 8;
        float b = 9;

        Console.WriteLine($"Заданный отрезок: [{a}; {b}]");

        Console.WriteLine("Метод Ньютона");
        NewtonMethod(a, b);
        //NewtonMethod(a); //  решения для конкретно левого и правого концов отрезка
        //NewtonMethod(b);

        Console.WriteLine("Метод простых итераций");

        float M = Math.Max(dfx(a), dfx(b));
        SimpleIterMethod((float)8.1, M);

        Console.WriteLine("Метод хорд");
        HordeMethod(b, a);
    }
}