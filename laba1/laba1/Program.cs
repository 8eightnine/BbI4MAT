class Program
{
    static float x0 = 8;

    static float eps = 1e-3f;
    
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
        return (float)(Math.Sqrt(result));
    }

    static float dgx(float x)
    {
        float result = (float)(Math.Sqrt(5) * Math.Log(2) * Math.Pow(2, x - 1));
        result = result / (float)(5 * Math.Sqrt(Math.Pow(2, x) + 10));
        return result;
    }

    // Проверка изменения знака первой и второй производных на интервале [a, b]
    static bool CheckSignChangeOnInterval(float a, float b)
    {
        // Проверяем знаки первой производной на концах интервала
        if ( (Math.Sign(dfx(a)) == Math.Sign(dfx(a))) && (Math.Sign(dfx(a)) == Math.Sign(dfx(a))) )
        {
            Console.WriteLine("Знаки первой производной на концах интервала совпадают.");
            
            // Проверяем знаки второй производной на концах интервала
            if ((Math.Sign(ddfx(a)) == Math.Sign(ddfx(a))) && (Math.Sign(ddfx(a)) == Math.Sign(ddfx(a))))
            {
                Console.WriteLine("Знаки второй производной на концах интервала совпадают.");
                return true;
            }
        }
        // Возможно надо будет доделать (проверка на всем интервале, но очень неэффективно)

        return false;
    }

    static void NewtonMethod(float a, float b)
    {
        //float x = val; // конкретно левый и правый
        float x = 0;

        if (CheckSignChangeOnInterval(a, b))
        {

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
                Console.WriteLine("Ошибка: не соблюдается условие совпадаемости знаков f(x) и f''(x)");
                return;
            }

            Console.WriteLine($"Начальное приближение равно: {x}");

            int iterations = 1;

            while (true)
            {
                float y1 = fx(x); float y2 = dfx(x);
                float dx = y1 / y2;
                x -= dx;

                Console.WriteLine($"Iter {iterations}: Result {x}");

                iterations++;

                if (Math.Abs(dx) < eps)
                {
                    Console.WriteLine($"Итерации: {iterations}, ответ: {x:0.00000000}");
                    break;
                }
            }
        }
    }

    static void SimpleIterMethod(float a) // Не работает ((
    {
        float x1;
        float x0 = a;
        float dx = float.MaxValue;
        int iterations = 0;
        bool error = false;

        do
        {
            x1 = gx(x0);
            iterations++;

            if (Math.Abs(x1 - x0) >= eps && iterations == 1000)
            {
                error = true;
                break;
            }

            x0 = x1;
            Console.WriteLine($"Итерация {iterations}, x = {x1}");

        } while (Math.Abs(x0 - gx(x0)) > eps);

        if (error)
        {
            Console.WriteLine("Не удалось найти решение с заданной точностью за максимальное количество итераций.");
        }
        else
        {
            Console.WriteLine($"Решение x = {x1:0.000000} найдено за {iterations} итераций");
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
        Console.WriteLine($"Количество итераций: {iterations}, x = {b}");
    }

    static void iter()
    {
        float l = 7;
        float r = 10;
        float c;

        do
        {
            c = l + (r - l) / 2;

            if (fx(l) * fx(c) > 0)
            {
                l = c;
            }
            else
            {
                r = c;
            }

        } while (Math.Abs(fx(c)) > eps);

        Console.WriteLine(c);
    }


    static void Main(string[] args)
    {
        float a = 7;
        float b = 10;

        //Console.WriteLine($"Заданный отрезок: [{a}; {b}]");
        //NewtonMethod(a, b);
        //NewtonMethod(a); //  решения для конкретно левого и правого концов отрезка
        //NewtonMethod(b);

        //float c = 10;
       // SimpleIterMethod(c);

        iter();

        //HordeMethod(a, b);

    }
}