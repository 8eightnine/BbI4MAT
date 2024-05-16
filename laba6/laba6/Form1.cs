using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace laba6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Дифференциальное уравнение: y' = sin(x) - 2y
        private double DifferentialEquation(double x, double y)
        {
            return Math.Sin(x) - 2 * y;
        }

        // Точное решение
        private double ExactSolution(double x, double y0)
        {
            return 0.5 * (Math.Sin(x) + 2 * y0 * Math.Exp(-2 * x));
        }

        // Метод Эйлера
        private void EulerMethod(double x0, double y0, double xn, double h)
        {
            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            double x = x0;
            double y = y0;

            while (x <= xn)
            {
                xValues.Add(x);
                yValues.Add(y);

                y += h * DifferentialEquation(x, y);
                x += h;
            }

            PlotGraph(xValues, yValues, Color.Blue, "Метод Эйлера");
        }

        // Метод Рунге-Кутты-Мерсона
        private void RungeKuttaMersonMethod(double x0, double y0, double xn, double h)
        {
            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            double x = x0;
            double y = y0;

            while (x <= xn)
            {
                xValues.Add(x);
                yValues.Add(y);

                double k1 = h * DifferentialEquation(x, y);
                double k2 = h * DifferentialEquation(x + h / 3, y + k1 / 3);
                double k3 = h * DifferentialEquation(x + h / 3, y + k1 / 3 + k2 / 6);
                double k4 = h * DifferentialEquation(x + h / 2, y + k1 / 3 + k3 / 3);
                double k5 = h * DifferentialEquation(x + h, y + k1 / 2 - k3 + 2 * k4);

                double localErr = Math.Abs((k1 / 6 + (2 * k4 + k5) / 3) - (k1 / 6 + k4 / 2 + k5 / 3));

                if (localErr <= h * 32)
                {
                    y += k1 / 6 + (2 * k4 + k5) / 3;
                    x += h;
                }
                else
                {
                    h /= 2; // уменьшение шага интегрирования
                }
            }

            PlotGraph(xValues, yValues, Color.Red, "Метод Рунге-Кутты-Мерсона");
        }

        // Модифицированный метод Эйлера
        private void ModifiedEulerMethod(double x0, double y0, double xn, double h)
        {
            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            double x = x0;
            double y = y0;

            while (x <= xn)
            {
                xValues.Add(x);
                yValues.Add(y);

                double k1 = h * DifferentialEquation(x, y);
                double k2 = h * DifferentialEquation(x + h, y + k1);

                y += (k1 + k2) / 2;
                x += h;
            }

            PlotGraph(xValues, yValues, Color.Green, "Модифицированный метод Эйлера");
        }

        // Метод Адамса 4-ого порядка
        private void AdamsMethod(double x0, double y0, double xn, double h)
        {
            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            // Используем метод Рунге-Кутты для вычисления первых четырех точек
            double x = x0;
            double y = y0;
            double k1, k2, k3, k4;

            for (int i = 0; i < 3; i++)
            {
                k1 = h * DifferentialEquation(x, y);
                k2 = h * DifferentialEquation(x + h / 2, y + k1 / 2);
                k3 = h * DifferentialEquation(x + h / 2, y + k2 / 2);
                k4 = h * DifferentialEquation(x + h, y + k3);

                y += (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                x += h;

                xValues.Add(x);
                yValues.Add(y);
            }

            // Применяем метод Адамса для последующих точек
            while (x <= xn)
            {
                double f0 = DifferentialEquation(x, y); // это говно заранее считается и так, переделать
                double f1 = DifferentialEquation(x - h, yValues[yValues.Count - 1]);
                double f2 = DifferentialEquation(x - 2 * h, yValues[yValues.Count - 2]);
                double f3 = DifferentialEquation(x - 3 * h, yValues[yValues.Count - 3]);

                double nextY = y + h * (55 * f0 - 59 * f1 + 37 * f2 - 9 * f3) / 24;
                x += h;

                xValues.Add(x);
                yValues.Add(nextY);

                y = nextY;
            }

            PlotGraph(xValues, yValues, Color.Orange, "Метод Адамса 4-го порядка");
        }

        // Построение графика
        private void PlotGraph(List<double> xValues, List<double> yValues, Color color, string methodName)
        {
            if (xValues.Count != yValues.Count)
            {
                return;
            }

            Series series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.Color = color;
            series.Name = methodName;

            for (int i = 0; i < xValues.Count; i++)
            {
                series.Points.AddXY(xValues[i], yValues[i]);
            }

            chart1.Series.Add(series);
        }

        private void btnPlot_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            double x0 = double.Parse(txtX0.Text);
            double y0 = double.Parse(txtY0.Text);
            double xn = double.Parse(txtXn.Text);
            double h = double.Parse(txtStepSize.Text);

            // График точного решения
            List<double> exactXValues = new List<double>();
            List<double> exactYValues = new List<double>();

            for (double x = x0; x <= xn; x += 0.1)
            {
                exactXValues.Add(x);
                exactYValues.Add(ExactSolution(x, y0));
            }

            if (checkBox1.Checked)
            {
                PlotGraph(exactXValues, exactYValues, Color.Black, "Точное решение");
            }
            if (checkBox2.Checked)
            {
                EulerMethod(x0, y0, xn, h);
            }
            if (checkBox3.Checked)
            {
                ModifiedEulerMethod(x0, y0, xn, h);
            }
            if (checkBox4.Checked)
            {
                RungeKuttaMersonMethod(x0, y0, xn, h);
            }
            if (checkBox5.Checked)
            {
                AdamsMethod(x0, y0, xn, h);
            }
        }
    }
}