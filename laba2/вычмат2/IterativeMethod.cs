using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace вычмат2
{
    class IterativeMethod
    {
        public static float[] SolveIterativeMethod(float[,] a, float[] b, float epsilon, int iterations)
        {
            int n = b.Length;

            // Проверка условия диагонального преобладания
            if (!CheckDiagonalDominance(a))
            {
                Console.WriteLine("Матрица не удовлетворяет условию диагонального преобладания.");
                EnsureDiagonallyDominant(a, b);
            }

            // Преобразование системы уравнений к виду x = alpha * x + beta
            float[,] alpha = new float[n, n];
            float[] beta = new float[n];
            for (int i = 0; i < n; i++)
            {
                beta[i] = b[i] / a[i, i];
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        alpha[i, j] = -a[i, j] / a[i, i];
                    }
                }
            }

            // Проверка условия нормы матрицы alpha < 1
            float alphaNorm = CalculateMatrixNorm(alpha, n);
            if (alphaNorm >= 1)
            {
                Console.WriteLine("Условие нормы матрицы alpha < 1 не выполняется. Норма alpha: " + alphaNorm);
                return null;
            }

            // Начальное приближение
            float[] x = new float[n];
            float[] newX = new float[n];
            for (int k = 0; k < iterations; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    newX[i] = beta[i];
                    for (int j = 0; j < n; j++)
                    {
                        newX[i] += alpha[i, j] * x[j];
                    }
                }

                // Проверка условия сходимости
                float norm = CalculateVectorNorm(SubtractVectors(newX, x));
                if (norm <= epsilon)
                {
                    Console.WriteLine($"Сходимость достигнута на итерации {k + 1}");
                    return newX;
                }

                Array.Copy(newX, x, n); // Подготовка к следующей итерации
            }

            Console.WriteLine("Метод не сошелся после заданного числа итераций.");
            return null; // Метод не сходится
        }

        static bool CheckDiagonalDominance(float[,] matrix)
            {
                int n = matrix.GetLength(0);
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
}
