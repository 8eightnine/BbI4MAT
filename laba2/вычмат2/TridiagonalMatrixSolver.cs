
namespace вычмат2
{
    class TridiagonalMatrixSolver
    {
        public static float[] Solve(float[] a, float[] b, float[] c, float[] d, float tolerance = 1e-10f, int maxIterations = 10000)
        {
            int n = d.Length;
            float[] x = new float[n];
            float[] prev = new float[n];
            bool converged = false;

            for (int iteration = 0; iteration < maxIterations && !converged; iteration++)
            {
                for (int i = 0; i < n; i++)
                {
                    float sum = 0.0f;

                    if (i > 0)
                    {
                        sum += a[i] * x[i - 1];
                    }

                    if (i < n - 1)
                    {
                        sum += c[i] * prev[i + 1];
                    }

                    x[i] = (d[i] - sum) / b[i];

                    // Для первой итерации предыдущее значение - это начальное приближение (0)
                    if (iteration == 0)
                    {
                        prev[i] = x[i];
                    }
                }

                converged = true;
                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(x[i] - prev[i]) > tolerance)
                    {
                        converged = false;
                        break;
                    }
                    prev[i] = x[i];
                }
            }

            if (!converged)
            {
                Console.WriteLine("Метод Зейделя не сошелся за заданное количество итераций.");
            }

            return x;
        }
    }
}
