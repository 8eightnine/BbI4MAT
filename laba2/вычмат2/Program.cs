
namespace вычмат2
{
    internal class Program
    {
        static void part1()
        {
            Console.WriteLine("|Задание 1|");

            float[,] A = {
            { 2.83333f, 5.0f, 1.0f },
            { 1.7f, 3.0f, 7.0f },
            { 8.0f, 1.0f, 1.0f }};
            float[] b = { 11.66666f, 13.4f, 18.0f };

            float[] result = GaussMethod.SolveGaussMethod(A, b);
            Console.WriteLine("Метод Гаусса");
            Console.WriteLine("Решение системы:");
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"x{i + 1} = {result[i]:F9}");
            }
            Console.WriteLine();

            result = GaussMethod.SolveGaussMethodRow(A, b);
            Console.WriteLine("Метод Гаусса с выбором главного элемента по строке(вариант 2)");
            Console.WriteLine("Решение системы:");
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"x{i + 1} = {result[i]:F9}");
            }
            Console.WriteLine();
        }

        static void part2()
        {
            Console.WriteLine("|Задание 2|");

            // Введите коэффициенты системы уравнений в виде матрицы a * x = b
            float[,] A = {
            { 2.0f, 15.0f, 1.0f, 1.0f },
            { 3.0f, -1.0f, 2.0f, 20.0f },
            { 10.0f, 1.0f, 1.0f, 1.0f },
            { 3.0f, 2.0f, 14.0f, -2.0f }};

            float[] b = { 21.0f, 27.0f, 23.0f, 20.0f };

            // Количество итераций
            int iterations = 1000;

            // Точность вычислений
            float epsilon = 1e-3f;

            float[] result = IterativeMethod.SolveIterativeMethod(A, b, epsilon, iterations);
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"x{i + 1} = {result[i]:F9}");
            }
            Console.WriteLine();
        }

        static void part3()
        {
            Console.WriteLine("|Задание 3|");

            float[] a = { 1, 1, 1, 1, 1, 1, 0 };   // коэф. верхней диагонали
            float[] b = { 4, 4, 4, 4, 4, 4, 4 };   // коэф. главной диагонали
            float[] c = { 0, 1, 1, 1, 1, 1, 1 };   // коэф. нижней диагонали
            float[] d = { 1, 2, 3, 4, 5, 6, 7 };   //вектор свободных членов системы уравнений

            float[] result = TridiagonalMatrixSolver.Solve(a, b, c, d);

            Console.WriteLine("Решение системы:");
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"x[{i}] = {result[i]:F4}");
            }
        }

        static void Main(string[] args)
        {
            part1();
            part2();
            part3();
        }
    }
}
