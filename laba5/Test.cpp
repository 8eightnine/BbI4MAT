#include <stdio.h>
#include <stdlib.h>
#define _USE_MATH_DEFINES
#include <math.h>


double lower, upper;
int step;

double f(double x)
{
    return (1 + x) / (pow((2 + 3 * x), 2) * sqrt(2 + x));
}
double simpson(double lower, double upper);
int main()
{
    printf("Please enter lower bound:");
    scanf("%lf", &lower);
    printf("Please enter upper bound:");
    scanf("%lf", &upper);
    printf("Please enter n:");
    scanf("%d", &step);
    double result = 0;

    double functionIncrement = (upper - lower) / step;

    for (int i = 0; i < step; i++)
    {
        double low = lower + i * functionIncrement;
        double up = low + functionIncrement;
        result += simpson(low, up);
        printf("%lf %lf\n", low, up);

    }

    printf("Integration result is: %.10lf\n", result);
    system("PAUSE");
    return 0;
}

double simpson(double lower, double upper)
{
    double h = (upper - lower) / 3;
    double mid = lower + h;
    double mid2 = mid + h;
    double res = (3 * h / 8) * (f(lower) + 3 * f(mid) + 3 * f(mid2) + f(upper)) - 3 * pow(h, 5) / 80;
    return res;
}