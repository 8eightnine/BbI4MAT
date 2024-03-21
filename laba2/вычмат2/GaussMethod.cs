
namespace вычмат2
{
    class GaussMethod
    {
        public static float[] SolveGaussMethod(float[,] matrix, float[] b)
        {
            int n = b.Length;

            // Прямой ход для преобразования системы к верхнетреугольному виду
            for (int i = 0; i < n - 1; ++i)
            {
                // Проверка на ноль текущего элемента диагонали
                if (Math.Abs(matrix[i, i]) < 1e-7f)
                {
                    throw new Exception("Деление на ноль в методе Гаусса.");
                }

                for (int row = i + 1; row < n; ++row)
                {
                    float factor = matrix[row, i] / matrix[i, i];
                    for (int col = i; col < n; ++col)
                    {
                        matrix[row, col] -= factor * matrix[i, col];
                    }
                    b[row] -= factor * b[i];
                }
            }

            // Обратный ход для вычисления решений
            float[] solution = new float[n];
            for (int i = n - 1; i >= 0; --i)
            {
                float sum = b[i];
                for (int j = i + 1; j < n; ++j)
                {
                    sum -= matrix[i, j] * solution[j];
                }
                solution[i] = sum / matrix[i, i]; // Здесь происходит деление для нахождения неизвестных
            }

            return solution;
        }

        public static float[] SolveGaussMethodColumn(float[,] originalMatrix, float[] originalB)
        {
            int n = originalB.Length;
            float[,] matrix = new float[n, n];
            float[] b = new float[n];
            Array.Copy(originalB, b, n);
            Array.Copy(originalMatrix, matrix, n * n);

            int[] columnIndices = Enumerable.Range(0, n).ToArray();

            for (int i = 0; i < n; i++)
            {
                // Search for the column with the maximum absolute value in the row
                int maxColumn = i;
                float maxVal = Math.Abs(matrix[i, i]);
                for (int k = i + 1; k < n; k++)
                {
                    float absVal = Math.Abs(matrix[i, k]);
                    if (absVal > maxVal)
                    {
                        maxColumn = k;
                        maxVal = absVal;
                    }
                }

                // Swap columns
                SwapColumns(matrix, columnIndices, i, maxColumn);

                if (Math.Abs(matrix[i, i]) < 1e-7f)
                {
                    throw new Exception("Division by zero in Gauss method with main element selection by row.");
                }

                // Transform the matrix to upper triangular form
                for (int k = i + 1; k < n; k++)
                {
                    float coeff = matrix[k, i] / matrix[i, i];
                    for (int j = i; j < n; j++)
                    {
                        matrix[k, j] -= matrix[i, j] * coeff;
                    }
                    b[k] -= b[i] * coeff;
                }
            }

            // Back substitution
            float[] solution = new float[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += matrix[i, j] * solution[columnIndices[j]];
                }
                solution[columnIndices[i]] = (float)(b[i] - sum) / matrix[i, i];
            }

            return solution;
        }

        private static void SwapColumns(float[,] matrix, int[] columnIndices, int column1, int column2)
        {
            int n = matrix.GetLength(0);
            for (int row = 0; row < n; row++)
            {
                float temp = matrix[row, column1];
                matrix[row, column1] = matrix[row, column2];
                matrix[row, column2] = temp;
            }
            int tempIndex = columnIndices[column1];
            columnIndices[column1] = columnIndices[column2];
            columnIndices[column2] = tempIndex;
        }
    }
}