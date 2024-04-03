using System;

class Program
{
    static void Main(string[] args)
    {
        double[] steps = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6, 1e-7, 1e-8 };

        foreach (var step in steps)
        {
            double x = 1.0; // Точка, в которой вычисляется производная
            double h = step; // Шаг

            double derivative = CalculateDerivative(x, h);

            Console.WriteLine($"Шаг: {step}, Первая производная: {derivative}");
        }

        Console.ReadLine();
    }

    static double Function(double x)
    {
        return 3 * x * x;
    }

    static double CalculateDerivative(double x, double h)
    {
        double fxph = Function(x + h); // f(x + h)
        double fxmh = Function(x - h); // f(x - h)

        double derivative = (fxph - fxmh) / (2 * h);

        return derivative;
    }
}