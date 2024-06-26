#include <algorithm>
#include <iostream>
#include <ctime>
#include <iomanip>
#include "LCD.h"

#include <iostream>

int main() {
    setlocale(LC_ALL, "rus");
    char X[] = "ALBDACD", Y[] = "CDLDCA";
    std::cout << std::endl << "-- calculating the length of the LCS for X and Y (recursion)";
    std::cout << std::endl << "-- sequence X: " << X;
    std::cout << std::endl << "-- sequence Y: " << Y;
    int s = lcs(
            sizeof(X) - 1,  // длина   последовательности  X
            "ALBDACD",       // последовательность X
            sizeof(Y) - 1,  // длина   последовательности  Y
            "CDLDCA"       // последовательность Y
    );
    std::cout << std::endl << "-- length LCS: " << s << std::endl;


    //DYNAMIC
    char z[100] = "";
    char x[] = "ALBDACD",
            y[] = "CDLDCA";

    int l = lcsd(x, y, z);
    std::cout << std::endl
              << "-- the largest common subsequence is LCS(dynamic"
              << " programming)" << std::endl;
    std::cout << std::endl << "sequence X: " << x;
    std::cout << std::endl << "sequence Y: " << y;
    std::cout << std::endl << "                LCS: " << z;
    std::cout << std::endl << "          length LCS: " << l;
    std::cout << std::endl;


    system("pause");
    return 0;
}