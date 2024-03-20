using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace вычмат2
{
    class GaussMethodTemp
    {
        //static void Main(string[] args)
        //{

        //    float[,] A = {
        //        { 2.83333f, 5.0f, 1.0f },
        //        { 1.7f, 3.0f, 7.0f },
        //        { 8.0f, 1.0f, 1.0f }
        //    };

        //    float[] b = { 11.66666f, 13.4f, 18.0f };

        //    float[] result = GaussMethodRow(A, b);

        //    Console.WriteLine("Решение системы:");
        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        Console.WriteLine($"x{i + 1} = {result[i]:F9}");
        //    }
        //}

        //static float[] GaussMethodRow(float[,] matrix, float[] b)
        //{
        //    int n = b.Length;
        //    // Инициализация массива индексов переменных
        //    int[] indexes = new int[n];
        //    for (int i = 0; i < n; i++) indexes[i] = i;

        //    for (int i = 0; i < n; ++i)
        //    {
        //        // Выбор главного элемента в строке
        //        int maxIndex = i;
        //        float maxValue = Math.Abs(matrix[i, i]);
        //        for (int j = i + 1; j < n; ++j)
        //        {
        //            float absValue = Math.Abs(matrix[i, j]);
        //            if (absValue > maxValue)
        //            {
        //                maxValue = absValue;
        //                maxIndex = j;
        //            }
        //        }

        //        // Перестановка столбцов
        //        if (maxIndex != i)
        //        {
        //            for (int k = 0; k < n; ++k)
        //            {
        //                float temp = matrix[k, i];
        //                matrix[k, i] = matrix[k, maxIndex];
        //                matrix[k, maxIndex] = temp;
        //            }
        //            int tempIndex = indexes[i];
        //            indexes[i] = indexes[maxIndex];
        //            indexes[maxIndex] = tempIndex;
        //        }

        //        // Преобразование остальных строк
        //        for (int j = i + 1; j < n; ++j)
        //        {
        //            float factor = matrix[j, i] / matrix[i, i];
        //            for (int k = i; k < n; ++k)
        //            {
        //                matrix[j, k] -= factor * matrix[i, k];
        //            }
        //            b[j] -= factor * b[i];
        //        }
        //    }

        //    // Обратный ход
        //    float[] solution = new float[n];
        //    for (int i = n - 1; i >= 0; --i)
        //    {
        //        float sum = b[i];
        //        for (int j = i + 1; j < n; ++j)
        //        {
        //            sum -= matrix[i, j] * solution[j];
        //        }
        //        solution[i] = sum / matrix[i, i];
        //    }

        //    // Переставляем решения в соответствии с перестановкой столбцов
        //    float[] orderedSolution = new float[n];
        //    for (int i = 0; i < n; i++)
        //    {
        //        orderedSolution[indexes[i]] = solution[i];
        //    }

        //    return orderedSolution;
        //}
    }
}
