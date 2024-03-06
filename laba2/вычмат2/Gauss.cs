//class GaussMethod
//{
//    static void Main(string[] args)
//    {
//        float[,] A = {
//            { 2.83333f, 5.0f, 1.0f },
//            { 1.7f, 3.0f, 7.0f },
//            { 8.0f, 1.0f, 1.0f }
//        };

//        float[] b = { 11.66666f, 13.4f, 18.0f };

//        float[] result = GaussMethodSingleDivision(A, b);

//        Console.WriteLine("Решение системы:");
//        for (int i = 0; i < result.Length; i++)
//        {
//            Console.WriteLine($"x{i + 1} = {result[i]:F9}");
//        }
//    }

//    static float[] GaussMethodSingleDivision(float[,] matrix, float[] b)
//    {
//        int n = b.Length;

//        // Прямой ход с нормализацией диагональных элементов
//        for (int i = 0; i < n; ++i)
//        {
//            // Нормализация текущей строки
//            float diagElement = matrix[i, i];
//            for (int j = i; j < n; ++j)
//            {
//                matrix[i, j] /= diagElement;
//            }
//            b[i] /= diagElement;

//            // Вычитание текущей строки из всех следующих строк
//            for (int row = i + 1; row < n; ++row)
//            {
//                float factor = matrix[row, i];
//                for (int col = i; col < n; ++col)
//                {
//                    matrix[row, col] -= factor * matrix[i, col];
//                }
//                b[row] -= factor * b[i];
//            }
//        }

//        // Обратный ход
//        float[] solution = new float[n];
//        for (int i = n - 1; i >= 0; --i)
//        {
//            solution[i] = b[i];
//            for (int j = i + 1; j < n; ++j)
//            {
//                solution[i] -= matrix[i, j] * solution[j];
//            }
//        }

//        return solution;
//    }
//}

//class GaussMethod
//{
//    static void Main(string[] args)
//    {
//        //float[,] A = {
//        //    { 2.83333f, 5.0f, 1.0f },
//        //    { 1.7f, 3.0f, 7.0f },
//        //    { 8.0f, 1.0f, 1.0f }
//        //};

//        //float[] b = { 11.66666f, 13.4f, 18.0f };

//        float[,] A = {
//            { 2.83333f, 5.0f, 1.0f },
//            { 1.7f, 3.0f, 7.0f },
//            { 8.0f, 1.0f, 1.0f }
//        };

//        float[] b = { 11.66666f, 13.4f, 18.0f };

//        float[] result = GaussMethodRow(A, b);

//        Console.WriteLine("Решение системы:");
//        for (int i = 0; i < result.Length; i++)
//        {
//            Console.WriteLine($"x{i + 1} = {result[i]:F9}");
//        }
//    }

//    static float[] GaussMethodRow(float[,] matrix, float[] b)
//    {
//        int n = b.Length;
//        // Инициализация массива индексов переменных
//        int[] indexes = new int[n];
//        for (int i = 0; i < n; i++) indexes[i] = i;

//        for (int i = 0; i < n; ++i)
//        {
//            // Выбор главного элемента в строке
//            int maxIndex = i;
//            float maxValue = Math.Abs(matrix[i, i]);
//            for (int j = i + 1; j < n; ++j)
//            {
//                float absValue = Math.Abs(matrix[i, j]);
//                if (absValue > maxValue)
//                {
//                    maxValue = absValue;
//                    maxIndex = j;
//                }
//            }

//            // Перестановка столбцов
//            if (maxIndex != i)
//            {
//                for (int k = 0; k < n; ++k)
//                {
//                    float temp = matrix[k, i];
//                    matrix[k, i] = matrix[k, maxIndex];
//                    matrix[k, maxIndex] = temp;
//                }
//                int tempIndex = indexes[i];
//                indexes[i] = indexes[maxIndex];
//                indexes[maxIndex] = tempIndex;
//            }

//            // Преобразование остальных строк
//            for (int j = i + 1; j < n; ++j)
//            {
//                float factor = matrix[j, i] / matrix[i, i];
//                for (int k = i; k < n; ++k)
//                {
//                    matrix[j, k] -= factor * matrix[i, k];
//                }
//                b[j] -= factor * b[i];
//            }
//        }

//        // Обратный ход
//        float[] solution = new float[n];
//        for (int i = n - 1; i >= 0; --i)
//        {
//            float sum = b[i];
//            for (int j = i + 1; j < n; ++j)
//            {
//                sum -= matrix[i, j] * solution[j];
//            }
//            solution[i] = sum / matrix[i, i];
//        }

//        // Переставляем решения в соответствии с перестановкой столбцов
//        float[] orderedSolution = new float[n];
//        for (int i = 0; i < n; i++)
//        {
//            orderedSolution[indexes[i]] = solution[i];
//        }

//        return orderedSolution;
//    }
//}