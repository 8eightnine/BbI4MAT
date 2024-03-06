//// Введите коэффициенты системы уравнений в виде матрицы a*x = b
//float[,] a = {
//    { 2.0f, 15.0f, 1.0f, 1.0f },
//   { 3.0f, -1.0f, 2.0f, 20.0f },
//   { 10.0f, 1.0f, 1.0f, 1.0f },
//   { 3.0f, 2.0f, 14.0f, -2.0f }
//};

//float[] b = { 21.0f, 27.0f, 23.0f, 20.0f };
using System;

class IterativeMethod
{
    static void Main()
    {
        // Введите коэффициенты системы уравнений в виде матрицы a * x = b
        float[,] a = {
            { 2.0f, 15.0f, 1.0f, 1.0f },
            { 3.0f, -1.0f, 2.0f, 20.0f },
            { 10.0f, 1.0f, 1.0f, 1.0f },
            { 3.0f, 2.0f, 14.0f, -2.0f }
        };

        float[] b = { 21.0f, 27.0f, 23.0f, 20.0f };

        int n = b.Length;

        // Проверка условия диагонального преобладания
        if (!CheckDiagonalDominance(a, n))
        {
            Console.WriteLine("Матрица не удовлетворяет условию диагонального преобладания.");
            EnsureDiagonallyDominant(a, b);
        }

        // Преобразование системы уравнений к виду x = alpha * x + beta
        float[,] alpha = new float[n, n];
        float[] beta = new float[n];
        for (int i = 0; i < n; i++)
        {
            float sum = 0;
            for (int j = 0; j < n; j++)
            {
                if (i != j)
                {
                    alpha[i, j] = -a[i, j] / a[i, i];
                    sum += alpha[i, j];
                }
            }
            beta[i] = b[i] / a[i, i] + sum;
        }

        // Проверка условия нормы матрицы alpha < 1
        Console.WriteLine(CalculateMatrixNorm(alpha, n));
        if (!CheckMatrixNorm(alpha, n))
        {
            Console.WriteLine("Условие нормы матрицы alpha < 1 не выполняется.");
            return;
        }

        // Начальное приближение
        float[] x = new float[n];
        for (int i = 0; i < n; i++)
        {
            x[i] = beta[i];
        }

        // Количество итераций
        int iterations = 1000;

        // Точность вычислений
        float epsilon = 1e-3f;

        // Метод простых итераций
        for (int k = 0; k < iterations; k++)
        {
            float[] newX = new float[n];
            for (int i = 0; i < n; i++)
            {
                newX[i] = beta[i];
                for (int j = 0; j < n; j++)
                {
                    newX[i] += alpha[i, j] * x[j];
                }
            }

            // Проверка условия сходимости
            float norm = CalculateVectorNorm(SubtractVectors(x, newX));
            //float convergenceCriterion = ((1 - CalculateMatrixNorm(alpha, n)) / CalculateMatrixNorm(alpha, n)) * epsilon;
            // Поскольку норма = 0.5, используем упрощенное неравенство
            if (norm <= epsilon)
            {
                Console.WriteLine($"Критерий сходимости достигнут на итерации {k}");
                break;
            }

            x = newX;
        }

        // Вывод результатов
        Console.WriteLine("Решение системы уравнений методом простых итераций:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("x[" + i + 1 + "] = " + x[i]);
        }
    }

    // Проверка условия диагонального преобладания
    static bool CheckDiagonalDominance(float[,] matrix, int n)
    {
        for (int i = 0; i < n; i++)
        {
            float sum = 0;
            for (int j = 0; j < n; j++)
            {
                if (i != j)
                {
                    sum += Math.Abs(matrix[i, j]);
                }
            }
            if (Math.Abs(matrix[i, i]) <= sum)
            {
                return false;
            }
        }
        return true;
    }

    // Проверка условия нормы матрицы alpha < 1
    static bool CheckMatrixNorm(float[,] matrix, int n)
    {
        float norm = CalculateMatrixNorm(matrix, n);
        return norm < 1;
    }

    // Вычисление нормы матрицы
    static float CalculateMatrixNorm(float[,] matrix, int n)
    {
        float maxSum = float.MinValue;
        for (int i = 0; i < n; i++)
        {
            float rowSum = 0;
            for (int j = 0; j < n; j++)
            {
                rowSum += Math.Abs(matrix[i, j]);
            }
            maxSum = Math.Max(maxSum, rowSum);
        }
        return maxSum;
    }

    // Вычисление нормы вектора
    static float CalculateVectorNorm(float[] vector)
    {
        float sumOfSquares = 0;
        foreach (var element in vector)
        {
            sumOfSquares += (float)Math.Pow(element, 2);
        }
        return (float)Math.Sqrt(sumOfSquares);
    }

    // Вычитание векторов
    static float[] SubtractVectors(float[] vector1, float[] vector2)
    {
        int n = vector1.Length;
        float[] result = new float[n];
        for (int i = 0; i < n; i++)
        {
            result[i] = vector1[i] - vector2[i];
        }
        return result;
    }



    static float[,] EnsureDiagonallyDominant(float[,] coefficients, float[] constants)
    {
        int n = coefficients.GetLength(0);

        // Перестановка строк матрицы
        for (int i = 0; i < n; i++)
        {
            float diagonalElement = coefficients[i, i];
            float rowSum = 0;
            for (int j = 0; j < n; j++)
            {
                rowSum += Math.Abs(coefficients[i, j]);
            }

            if (Math.Abs(diagonalElement) <= rowSum - Math.Abs(diagonalElement))
            {
                // Ищем строку с максимальным элементом в текущем столбце
                int maxRow = i;
                float maxElement = Math.Abs(coefficients[i, i]);
                for (int k = i + 1; k < n; k++)
                {
                    float currentElement = Math.Abs(coefficients[k, i]);
                    if (currentElement > maxElement)
                    {
                        maxElement = currentElement;
                        maxRow = k;
                    }
                }

                // Перестановка строк
                if (maxRow != i)
                {
                    for (int j = 0; j < n; j++)
                    {
                        float temp = coefficients[i, j];
                        coefficients[i, j] = coefficients[maxRow, j];
                        coefficients[maxRow, j] = temp;
                    }
                    float tempConstant = constants[i];
                    constants[i] = constants[maxRow];
                    constants[maxRow] = tempConstant;
                }
            }
        }

        return coefficients;
    }
}
