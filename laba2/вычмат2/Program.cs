
//namespace вычмат2
//{
//    internal class Program
//    {
//        static void part1()
//        {
//            float[,] A = {
//            { 2.83333f, 5.0f, 1.0f },
//            { 1.7f, 3.0f, 7.0f },
//            { 8.0f, 1.0f, 1.0f }};
//            float[] b = { 11.66666f, 13.4f, 18.0f };

//            float[] result = GaussMethod.GaussMethodSingleDivision(A, b);
//            Console.WriteLine("Метод Гаусса");
//            Console.WriteLine("Решение системы:");
//            for (int i = 0; i < result.Length; i++)
//            {
//                Console.WriteLine($"x{i + 1} = {result[i]:F9}");
//            }

//            result = GaussMethod.GaussMethodRow(A, b);
//            Console.WriteLine("Метод Гаусса с выбором главного элемента по строке(вариант 2)");
//            Console.WriteLine("Решение системы:");
//            for (int i = 0; i < result.Length; i++)
//            {
//                Console.WriteLine($"x{i + 1} = {result[i]:F9}");
//            }
//        }

//        static void part2()
//        {
//            float[,] A = {
//            { 2, 15, 1, 1 },
//            { 3, -1, 2, 20 },
//            { 10, 1, 1, 1 },
//            { 3, 2, 14, -2 }};
//            float[] b = { 21, 27, 23, 20 };

//            float[] result = GaussMethod.SimpleIterations(A, b, epsilon:0.01f);
//            Console.WriteLine("Метод Гаусса");
//            Console.WriteLine("Решение системы:");
//            for (int i = 0; i < result.Length; i++)
//            {
//                Console.WriteLine($"x{i + 1} = {result[i]:F9}");
//            }
//        }
//            static void part3()
//        {
//            float[] a = { 0, 15, 1, 1 };   // коэф. нижней диагонали
//            float[] b = { 3, -1, 2, 20 };  // коэф. главной диагонали
//            float[] c = { 10, 1, 1, 0 };   // коэф. верхней диагонали
//            float[] d = { 7, 8, 5 };       //вектор свободных членов системы уравнений

//            float[] solution = TridiagonalMatrixSolver.Solve(a, b, c, d);

//            Console.WriteLine("Решение:");
//            for (int i = 0; i < solution.Length; i++)
//            {
//                Console.WriteLine($"x[{i}] = {solution[i]}");
//            }
//        }
//        static void Main(string[] args)
//        {
//            part1();
//            part2();
//            part3();
//        }
//    }
//}
