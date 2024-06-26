#include <algorithm>
#include <cstring>

enum Dart { TOP, LEFT, LEFTTOP };

int lcs(int lenx, const char x[],
        int leny, const char y[])
{
    int rc = 0;
    if (lenx > 0 && leny > 0)
    {
        if (x[lenx - 1] == y[leny - 1])
            rc = 1 + lcs(lenx - 1, x, leny - 1, y);
        else
            rc = std::max(lcs(lenx, x, leny - 1, y), lcs(lenx - 1, x, leny, y));
    }
    return rc;        //длина LCS
}

void getLCScontent(int lenx, int leny, const char x[],
                   const Dart* B,
                   int n, int i, int j, char z[])
{
    if (i > 0 && j > 0 && n > 0)
    {
        if (B[(i * (leny + 1)) + j] == LEFTTOP)
        {
            getLCScontent(lenx, leny, x, B, n - 1, i - 1, j - 1, z);
            z[n - 1] = x[i - 1];
            z[n] = '\0';
        }
        else if (B[(i * (leny + 1)) + j] == TOP)
        {
            getLCScontent(lenx, leny, x, B, n, i - 1, j, z);
        }
        else
        {
            getLCScontent(lenx, leny, x, B, n, i, j - 1, z);
        }
    }
}

int lcsd(const char x[], const char y[], char z[])
{
    int lenx = strlen(x), leny = strlen(y);
    int* C = new int[(lenx + 1) * (leny + 1)];
    Dart* B = new Dart[(lenx + 1) * (leny + 1)];
    memset(C, 0, sizeof(int) * (lenx + 1) * (leny + 1));

    for (int i = 1; i <= lenx; i++)
    {
        for (int j = 1; j <= leny; j++)
        {
            if (x[i - 1] == y[j - 1])
            {
                C[(i * (leny + 1)) + j] = C[((i - 1) * (leny + 1)) + (j - 1)] + 1;
                B[(i * (leny + 1)) + j] = LEFTTOP;
            }
            else if (C[((i - 1) * (leny + 1)) + j] >= C[(i * (leny + 1)) + (j - 1)])
            {
                C[(i * (leny + 1)) + j] = C[((i - 1) * (leny + 1)) + j];
                B[(i * (leny + 1)) + j] = TOP;
            }
            else
            {
                C[(i * (leny + 1)) + j] = C[(i * (leny + 1)) + (j - 1)];
                B[(i * (leny + 1)) + j] = LEFT;
            }
        }
    }

    getLCScontent(lenx, leny, x, B, C[lenx * (leny + 1) + leny], lenx, leny, z);

    delete[] C;
    delete[] B;

    return C[lenx * (leny + 1) + leny];
}