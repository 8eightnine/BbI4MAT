
namespace вычмат2
{
    class TridiagonalMatrixSolver
    {
        public static float[] Solve(float[] a, float[] b, float[] c, float[] d) // метод прогонки
        {
            int n = d.Length;
            float[] P = new float[n]; // Массив коэффициентов P
            float[] Q = new float[n]; // Массив коэффициентов Q
            float[] x = new float[n]; // Решение

            // Прямой проход (вычисление коэффициентов P и Q)
            P[0] = -a[0] / b[0];
            Q[0] = d[0] / b[0];
            for (int i = 1; i < n; i++)
            {
                float denom = b[i] + c[i] * P[i - 1];
                P[i] = -a[i] / denom;
                Q[i] = (d[i] - c[i] * Q[i - 1]) / denom;
            }

            // Обратный проход (вычисление решения)
            x[n - 1] = Q[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                x[i] = P[i] * x[i + 1] + Q[i];
            }

            return x;
        }
    }
}