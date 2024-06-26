#include <algorithm>
#include <iostream>
#include <ctime>
#include <iomanip>
#include "Levenshtein.h"

int main()
{
    setlocale(LC_ALL, "rus");
    clock_t t1 = 0, t2 = 0, t3, t4;
    char x[301], y[251];

    srand(time(0));

    for (int i = 0; i < 300; i++)
    {
        x[i] = ('a' + rand() % ('z' - 'a'));
        std::cout << x[i];
    }

    std::cout << std::endl;

    for (int i = 0; i < 250; i++)
    {
        y[i] = ('a' + rand() % ('z' - 'a'));
        std::cout << y[i];
    }
    system("pause");
    return 0;
}