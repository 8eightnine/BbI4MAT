
namespace вычмат2
{
    internal class Program
    {
        static void part1()
        {
            Console.WriteLine("|Задание 1|");

            float[,] A = {
            { 1.0f, -1.0f, 1.0f, -1.0f, 1.0f },
            { 1.0f, 1.0f,  1.0f,  1.0f,  1.0f },
            { 1.0f, 2.0f, 4.0f, 8.0f, 16.0f },
            { 1.0f, 3.0f, 9.0f, 27.0f, 81.0f },
            { 1.0f, 4.0f, 16.0f, 64.0f, 256.0f }};

            float[] b = { -5.0f, -3.0f, 18.0f, 6.0f, -2.0f };

            float[] result = GaussMethod.SolveGaussMethod(A, b);
            Console.WriteLine("Метод Гаусса");
            Console.WriteLine("Решение системы:");
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"x{i + 1} = {result[i]:F9}");
            }
            Console.WriteLine();

            result = GaussMethod.SolveGaussMethodColumn(A, b);
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
            { 1.0f, -1.0f, 1.0f, -1.0f, 1.0f },
            { 1.0f, 1.0f,  1.0f,  1.0f,  1.0f },
            { 1.0f, 2.0f, 4.0f, 8.0f, 16.0f },
            { 1.0f, 3.0f, 9.0f, 27.0f, 81.0f },
            { 1.0f, 4.0f, 16.0f, 64.0f, 256.0f }};

            float[] b = { -5.0f, -3.0f, 18.0f, 6.0f, -2.0f };

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

            float[] a = { -1, -1, 2, -4, 0 };   // коэф. верхней диагонали
            float[] b = { 2, 8, 12, 18, 10 };   // коэф. главной диагонали
            float[] c = { 0, -3, -5, -6, -5 };   // коэф. нижней диагонали
            float[] d = { -25, 72, -69, -156, 20 };   //вектор свободных членов системы уравнений

            // https://kontromat.ru/?page_id=4980

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
            //part2();
            part3();
        }
    }
}
