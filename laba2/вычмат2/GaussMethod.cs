
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

        public static float[] SolveGaussMethodRow(float[,] originalMatrix, float[] originalB)
        {
            int n = originalB.Length;
            float[,] matrix = new float[n, n];
            float[] b = new float[n];
            Array.Copy(originalB, b, n);
            Array.Copy(originalMatrix, matrix, n * n);

            for (int i = 0; i < n; i++)
            {
                // Поиск строки с максимальным по модулю элементом в столбце
                int maxRow = i;
                float maxVal = Math.Abs(matrix[i, i]);
                for (int k = i + 1; k < n; k++)
                {
                    float absVal = Math.Abs(matrix[k, i]);
                    if (absVal > maxVal)
                    {
                        maxRow = k;
                        maxVal = absVal;
                    }
                }

                // Обмен строк
                SwapRows(matrix, b, i, maxRow);

                if (Math.Abs(matrix[i, i]) < 1e-7f)
                {
                    throw new Exception("Деление на ноль в методе Гаусса с выбором главного элемента по столбцу.");
                }

                // Приведение матрицы к треугольному виду
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

            // Обратный ход
            float[] solution = new float[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += matrix[i, j] * solution[j];
                }
                solution[i] = (float)(b[i] - sum) / matrix[i, i];
            }

            return solution;
        }

        private static void SwapRows(float[,] matrix, float[] b, int row1, int row2)
        {
            int n = matrix.GetLength(1);
            for (int col = 0; col < n; col++)
            {
                float temp = matrix[row1, col];
                matrix[row1, col] = matrix[row2, col];
                matrix[row2, col] = temp;
            }
            float tempB = b[row1];
            b[row1] = b[row2];
            b[row2] = tempB;
        }
    }
}