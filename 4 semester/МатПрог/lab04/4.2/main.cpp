#include <algorithm>
#include <iostream>
#include <ctime>
#include <iomanip>
#include "Levenshtein.h"
using namespace std;
int main()
{
    setlocale(LC_ALL, "rus");
    clock_t t1 = 0, t2 = 0, t3, t4;
    char x[25] = "abcdefghdd", y[] = "xyabcdeshrtrh";
    int  lx = sizeof(x) - 1, ly = sizeof(y) - 1;
    std::cout << std::endl;
    std::cout << std::endl << "-- Levenshtein distance -----" << std::endl;
    std::cout << std::endl << "--length --- recursion -- din.programs. ---"
              << std::endl;
    int u = 1;
    for (int i = 6; i < std::min(lx, ly); i++)
    {


        t1 = clock(); levenshtein_r(i, x, i - 2, y); t2 = clock();
        t3 = clock(); levenshtein(i, x, i - 2, y); t4 = clock();

        cout << right << setw(2) << 1 << "/" << setw(2) << u << "        " << left << setw(10) << (t2 - t1) << "   " << setw(10) << (t4 - t3) << endl;
        if (u > 0) {

            if (u == 20) { u = u + 5; }
            if (u == 15) { u = u + 5; }
            if (u == 10) { u = u + 5; }
            if (u == 5) { u = u + 5; }
            if (u == 2) { u = u + 3; }
            if (u == 1) { u = u + 1; }
        }

    }
    system("pause");
    return 0;
}
