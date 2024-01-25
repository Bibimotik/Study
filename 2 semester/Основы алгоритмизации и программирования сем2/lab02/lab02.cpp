#define  _CRT_SECURE_NO_WARNINGS
#define MAX_SIZE 10
#include <stdio.h>
int main() {
    int A[MAX_SIZE][MAX_SIZE], B[MAX_SIZE][MAX_SIZE], C[MAX_SIZE][MAX_SIZE];
    int n, m, k;
    FILE* fileA, * fileB, * fileC;
    int i, j, p, sum;

    // Открытие файлов
    fileA = fopen("A.txt", "r");
    fileB = fopen("B.txt", "r");
    fileC = fopen("C.txt", "w");

    // Считывание размерности матриц A и B из файлов
    fscanf(fileA, "%d", &n);
    fscanf(fileB, "%d", &m);

    // Считывание матриц A и B из файлов
    for (i = 0; i < n; i++) {
        for (j = 0; j < n; j++) {
            fscanf(fileA, "%d", &A[i][j]);
        }
    }
    for (i = 0; i < m; i++) {
        for (j = 0; j < m; j++) {
            fscanf(fileB, "%d", &B[i][j]);
        }
    }

    // Вычисление произведения матриц A и B
    for (i = 0; i < n; i++) {
        for (j = 0; j < m; j++) {
            sum = 0;
            for (p = 0; p < n; p++) {
                sum += A[i][p] * B[p][j];
            }
            C[i][j] = sum;
        }
    }

    // Запись матрицы C в файл
    fprintf(fileC, "%d\n", m);
    for (i = 0; i < n; i++) {
        for (j = 0; j < m; j++) {
            fprintf(fileC, "%d ", C[i][j]);
        }
        fprintf(fileC, "\n");
    }

    // Закрытие файлов
    fclose(fileA);
    fclose(fileB);
    fclose(fileC);

    return 0;
}
