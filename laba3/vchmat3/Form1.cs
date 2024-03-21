using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace vchmat3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int n;
        TextBox[] X = new TextBox[7];  // Массив ячеек x(i)
        TextBox[] F = new TextBox[7];  // Массив ячеек f(i)
        double step = 1;

        // Создание массива с данными из текстбокса
        private double[] array(TextBox[] box, int n)
        {
            double[] x = new double[n];
            double[] f = new double[n];

            //заполняем массив x, f из таблицы
            for (int i = 0; i < n; i++)
            {
                x[i] = new double();
                x[i] = double.Parse(box[i].Text);
            }
            return x;
        }

        // Стирает данные
        private void cleaning(int n)
        {
            //chart1.Series.Add("Точки");
            //chart1.Series.Add("-");
            //chart1.Series.Add("f(x)");
            for (int i = 0; i < n; i++)
            {
                X[i].Clear();
                F[i].Clear();
            }
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            textBox16.Clear();
        }

        // Проверка, что не все х=0
        private bool check(TextBox[] box, int n)
        {
            int fl = 0;
            for (int i = 0; i < n; i++)
                if (double.Parse(box[i].Text) == 0)
                    fl++;
            if (fl == n)
            {
                MessageBox.Show("Невозможно построить график. Введите другие значения х.");
                return false;
            }
            else
                return true;
        }

        // Кнопка ввести
        // Пользователь вводит размер таблицы, делает лишние ячейки невидимыми
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Visible = true;
            cleaning(n);
            n = int.Parse(textBox5.Text);
            if ((int.Parse(textBox5.Text) < 1) || (int.Parse(textBox5.Text) > 7))
            {
                MessageBox.Show("Невозможно построить график. Введите n от 1 до 7.");
                return;
            }
            X[0] = textBox2;
            X[1] = textBox3;
            X[2] = textBox4;
            X[3] = textBox6;
            X[4] = textBox7;
            X[5] = textBox8;
            X[6] = textBox9;

            F[0] = textBox1;
            F[1] = textBox15;
            F[2] = textBox14;
            F[3] = textBox13;
            F[4] = textBox12;
            F[5] = textBox11;
            F[6] = textBox10;

            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;

            for (int i = 0; i < 7; i++) //убираем лишние ячейки
            {
                if (i < n)
                {
                    X[i].Visible = true;
                    F[i].Visible = true;
                }
                else
                {
                    X[i].Visible = false;
                    F[i].Visible = false;
                }
                X[i].Clear();
                F[i].Clear();
            }
            button1.Hide();
            label1.Hide();
            textBox5.Hide();
        }

        //метод Гаусса без выбора главного элемента
        private double[] gauss(double[,] matrix, double[] y, int n)
        {
            n = y.Length;
            double[] b = new double[n];
            Array.Copy(y, b, n);
            Array.Copy(matrix, matrix, n * n);

            int[] columnIndices = Enumerable.Range(0, n).ToArray();

            for (int i = 0; i < n; i++)
            {
                // Search for the column with the maximum absolute value in the row
                int maxColumn = i;
                double maxVal = Math.Abs(matrix[i, i]);
                for (int k = i + 1; k < n; k++)
                {
                    double absVal = Math.Abs(matrix[i, k]);
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
                    double coeff = matrix[k, i] / matrix[i, i];
                    for (int j = i; j < n; j++)
                    {
                        matrix[k, j] -= matrix[i, j] * coeff;
                    }
                    b[k] -= b[i] * coeff;
                }
            }

            // Back substitution
            double[] solution = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += matrix[i, j] * solution[columnIndices[j]];
                }
                solution[columnIndices[i]] = (b[i] - sum) / matrix[i, i];
            }

            return solution;
        }

        private static void SwapColumns(double[,] matrix, int[] columnIndices, int column1, int column2)
        {
            int n = matrix.GetLength(0);
            for (int row = 0; row < n; row++)
            {
                double temp = matrix[row, column1];
                matrix[row, column1] = matrix[row, column2];
                matrix[row, column2] = temp;
            }
            int tempIndex = columnIndices[column1];
            columnIndices[column1] = columnIndices[column2];
            columnIndices[column2] = tempIndex;
        }

        // Сглаживающй мнгочлен 1 степени
        private void kv1()
        {
            chart1.Series[3].Points.Clear();
            chart1.Series[3].ChartType = SeriesChartType.Line;
            double MCH, x;
            for (x = -5; x <= 5; x += 0.001)
            {
                MCH = 0.027 + 1.541 * x;
                chart1.Series[3].Points.AddXY(x, MCH);
            }
        }

        // Сглаживающй мнгочлен 2 степени
        private void kv2()
        {
            chart1.Series[3].Points.Clear();
            chart1.Series[3].ChartType = SeriesChartType.Line;
            double MCH, x;
            for (x = -5; x <= 5; x += 0.001)
            {
                MCH = -Math.Pow(x, 2) * 1.835 + 6.897 * x + 1.763;
                chart1.Series[3].Points.AddXY(x, MCH);
            }
        }

        // Сглаживающй мнгочлен 3 степени
        private void kv3()
        {
            chart1.Series[3].Points.Clear();
            chart1.Series[3].ChartType = SeriesChartType.Line;
            double MCH, x;
            for (x = -5; x <= 5; x += 0.001)
            {
                MCH = -Math.Pow(x, 3) * 1.505 - Math.Pow(x, 2) * 5.099 + 4.632 * x - 7.358;
                chart1.Series[3].Points.AddXY(x, MCH);
            }
        }

        // Интерполяционный мнгочлен с коэффициентами
        private double FUNC(double x)
        {
            double interpolated;
            interpolated = -36.6 + 18.75 * x + 30.21 * Math.Pow(x, 2) - 17.75 * Math.Pow(x, 3) + 2.39 * Math.Pow(x, 4);

            return interpolated;
        }

        // Отрисовка графика
        private void GRAPH()
        {
            chart1.ChartAreas[0].AxisX.Minimum = -3;
            chart1.ChartAreas[0].AxisX.Maximum = 5;
            chart1.ChartAreas[0].AxisY.Minimum = -5;
            chart1.ChartAreas[0].AxisY.Maximum = 20;
            chart1.Series[3].ChartType = SeriesChartType.Line;

            double y = -5.0;
            double point;

            for (y = -20; y <= 20; y += 0.01)
            {
                point = FUNC(y);
                chart1.Series[3].Points.AddXY(y, point);
            }
        }

        // Настройка осей под график
        private void graph(double xMin, double xMax, double step, double[] x, double[] y, double[] pX, double[] pF)
        {
            // Масштаб
            chart1.Series[2].Points.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = -3;
            chart1.ChartAreas[0].AxisX.Maximum = 5;
            chart1.ChartAreas[0].AxisY.Minimum = -5;
            chart1.ChartAreas[0].AxisY.Maximum = 20;

            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = step;
            chart1.Series[0].Color = Color.FromArgb(0, 0, 0);
            chart1.Series[1].Color = Color.FromArgb(255, 0, 0);
            chart1.Series[0].Points.DataBindXY(x, y);

            chart1.Series[1].BorderWidth = 2;
            for (int i = 0; i < 5; i++)
            {
                chart1.Series[1].ChartType = SeriesChartType.Point;
                chart1.Series[1]["PixelPointWidth"] = "10";
                chart1.Series[1].Points.AddXY(pX[i], pF[i]);
            }

        }
            //Настройка осей под график
        private void Graph(double xMin, double xMax, double step, double[] x, double[] y)
        {
            // Масштаб
            chart1.ChartAreas[0].AxisX.Minimum = -10;
            chart1.ChartAreas[0].AxisX.Maximum = 10;
            chart1.ChartAreas[0].AxisY.Minimum = -20;
            chart1.ChartAreas[0].AxisY.Maximum = 30;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = step;
            chart1.Series[0].Color = Color.FromArgb(0, 0, 0);
            chart1.Series[0].Points.DataBindXY(x, y);
        }
            // Настройка осей под график
        private void GrapH(double xMin, double xMax, double step, double[] x, double[] y)
        {
            // Масштаб
            chart1.ChartAreas[0].AxisX.Minimum = -3;
            chart1.ChartAreas[0].AxisX.Maximum = 5;
            chart1.ChartAreas[0].AxisY.Minimum = -5;
            chart1.ChartAreas[0].AxisY.Maximum = 20;
            chart1.Series[2].ChartType = SeriesChartType.Line;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = step;
            chart1.Series[2].Color = Color.FromArgb(0, 0, 0);
            chart1.Series[2].Points.DataBindXY(x, y);
        }

        // Метод наименших квадратов
        private void button2_Click(object sender, EventArgs e)
        {
            if (check(X, n) == false)
            {
                cleaning(n);
                return;
            }
            else
                 if (int.Parse(textBox16.Text) == 0)
            {
                MessageBox.Show("Невозможно построить график. Степень многочлена не должна равняться 0.");
                cleaning(n);
                return;
            }

            //заполняем массивы из полей для ввода
            double[] x = array(X, n);
            double[] f = array(F, n);

            int.TryParse(textBox16.Text, out int k); // Степень многочлена
            double[] a = new double[k + 1];
            double[,] c = new double[k + 1, k + 1];
            double[] d = new double[k + 1];

            int m = 2 * k;
            double sum;
            double[] C = new double[m + 1];

            if (k == 0)
            {
                MessageBox.Show("Невозможно построить график. Степень многочлена не должна равняться 0.");
                return;
            }

            // Считаем С
            for (int i = 0; i <= m; i++)
            {
                C[i] = new double();
                sum = 0;
                for (int j = 0; j < n; j++)
                    sum += Math.Pow(x[j], i);
                C[i] = sum;
            }

            // Записываем двумерный массив С для решения СЛАУ
            int temp;
            for (int i = 0; i < k + 1; i++)
            {
                temp = i;
                for (int j = 0; j < k + 1; j++)
                {
                    c[i, j] = new double();
                    c[i, j] = C[temp];
                    temp++;
                }
            }

            // Считаем D и заполняем массив
            for (int i = 0; i < k + 1; i++)
            {
                sum = 0;
                d[i] = new double();
                for (int j = 0; j < n; j++)
                    sum += f[j] * Math.Pow(x[j], i);
                d[i] = sum;
            }

            // Решим СЛАУ методом Гаусса и получим коэф. аппроксимирующего многочлена
            a = gauss(c, d, k + 1);

            // Строим график
            double xMin = x[0] * 5;
            double xMax = x[n - 1] * 5;

            int count = (int)Math.Ceiling((xMax - xMin) / step) + 1;
            double[] abs = new double[count];
            double[] ord = new double[count];

            for (int i = 0; i < count; i++)
            {
                abs[i] = xMin + step * i;
                for (int j = 0; j <= k; j++)
                    ord[i] += Math.Pow(abs[i], j) * a[j];
            }
            graph(xMin, xMax, step, abs, ord, x, f);
        }

        // Метод Лагранжа
        private void button3_Click(object sender, EventArgs e)
        {
            if (check(X, n) == false)
            {
                cleaning(n);
                return;
            }

            //заполняем массивы из полей для ввода
            double[] x = array(X, n);
            double[] f = array(F, n);

            // Строим график
            double xMin = x[0] * 5;
            double xMax = x[n - 1] * 5;

            int count = (int)Math.Ceiling((xMax - xMin) / step) + 1;
            double[] abs = new double[count];
            double[] ord = new double[count];

            double L;
            double num, den; // Числитель и знаменатель для вычисления многочлена в форме Лагранжа
            for (int i = 0; i < count; i++)
            {
                abs[i] = xMin + step * i;
                L = 0;
                for (int j = 0; j < n; j++)
                {
                    num = 1;
                    den = 1;
                    for (int ii = 0; ii < n; ii++)
                    {
                        if (ii != j)
                        {
                            num *= abs[i] - x[ii];
                            den *= x[j] - x[ii];
                        }
                        else
                            continue;
                    }
                    L += f[j] * num / den;
                }
                ord[i] = L;
            }
            Graph(xMin, xMax, step, abs, ord);
        }

        // Метод Ньютона
        private void button4_Click(object sender, EventArgs e)
        {
            if (check(X, n) == false)
            {
                cleaning(n);
                return;
            }
            // Заполняем массивы из текстбоксов
            double[] x = array(X, n);
            double[] f = array(F, n);

            // Строим график
            double xMin = x[0] * 5;
            double xMax = x[n - 1] * 5;

            int count = (int)Math.Ceiling((xMax - xMin) / step) + 1;
            double[] abs = new double[count];
            double[] ord = new double[count];

            double[,] ff = new double[n, n];

            for (int i = 0; i < n; i++)
                ff[0, i] = f[i];

            int k = n - 1;

            // Разделенные разности порядка i
            for (int i = 1; i < n; i++)
            {
                int a = 0;
                for (int j = 0; j < k; j++)
                {
                    ff[i, j] = (ff[i - 1, j + 1] - ff[i - 1, j]) / (x[i + a] - x[j]);
                    a++;
                }
                k--;
            }

            // Строим график
            double P;
            double mult;
            for (int i = 0; i < count; i++)
            {
                P = ff[0, 0];
                abs[i] = xMin + step * i;
                for (int j = 1; j < n; j++)
                {
                    k = j - 1;
                    mult = 1;
                    while (k >= 0)
                    {
                        mult *= abs[i] - x[k];
                        k--;
                    }
                    P += ff[j, 0] * mult;
                }
                ord[i] = P;
            }
            Graph(xMin, xMax, step, abs, ord);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GRAPH();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Int32.TryParse(textBox16.Text, out int s);
            if (s == 1) kv1();
            else if (s == 2) kv2();
            else if (s == 3) kv3();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series.Add("Точки");
            chart1.Series.Add("-");
            chart1.Series.Add("f(x)");
        }
    }
}


