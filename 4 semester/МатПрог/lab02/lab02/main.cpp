#include <iostream>
#include <vector>
#include <algorithm>
#include <cstdlib>
#include <ctime>
#include <limits>
#include <iomanip>

using namespace std;

const int INF = numeric_limits<int>::max();

// Генерация случайных расстояний между городами
void generateDistances(vector<vector<int>>& distances, int n) {
    distances.resize(n, vector<int>(n, INF));
    for (int i = 0; i < n; i++) {
        distances[i][i] = INF;
        for (int j = i + 1; j < n; j++) {
            distances[i][j] = distances[j][i] = (rand() % 291) + 10; // Случайное число от 10 до 300
        }
    }

    // Установка случайного расстояния между 3 городами как бесконечное
    int city1 = rand() % n;
    int city2 = rand() % n;
    int city3 = rand() % n;
    while (city2 == city1) {
        city2 = rand() % n;
    }
    while (city3 == city1 || city3 == city2) {
        city3 = rand() % n;
    }
    distances[city1][city2] = distances[city2][city1] = INF;
    distances[city1][city3] = distances[city3][city1] = INF;
    distances[city2][city3] = distances[city3][city2] = INF;
}

vector<int> solveTravelingSalesmanProblem(const vector<vector<int>>& distances, int& totalDistance) {
    int n = distances.size();
    vector<int> cities(n);
    for (int i = 0; i < n; i++) {
        cities[i] = i + 1;
    }

    vector<int> bestPath;
    int bestDistance = INF;

    // Генерация всех перестановок городов
    do {
        int distance = 0;
        bool validPath = true;

        // Вычисление длины пути
        for (int i = 0; i < n - 1; i++) {
            int city1 = cities[i] - 1;
            int city2 = cities[i + 1] - 1;
            if (distances[city1][city2] == INF) {
                validPath = false;
                break;
            }
            distance += distances[city1][city2];
        }

        // Замыкаем путь к начальному городу
        if (validPath && distances[cities[n - 1] - 1][cities[0] - 1] != INF) {
            distance += distances[cities[n - 1] - 1][cities[0] - 1];
            if (distance < bestDistance) {
                bestDistance = distance;
                bestPath = cities;
            }
        }
    } while (next_permutation(cities.begin() + 1, cities.end()));

    totalDistance = bestDistance;
    return bestPath;
}

int main() {
    srand(time(nullptr));

    int n = 10;
    vector<vector<int>> distances;
    generateDistances(distances, n);

    // Вывод номеров городов сверху
    cout << "     ";
    for (int i = 1; i <= n; i++) {
        cout << setw(7) << left << i;
    }
    cout << endl;

    // Вывод матрицы расстояний и номеров городов слева
    for (int i = 1; i <= n; i++) {
        cout << setw(4) << left << i;
        for (int j = 1; j <= n; j++) {
            if (distances[i - 1][j - 1] == INF) {
                cout << setw(7) << left << "INF";
            } else {
                cout << setw(7) << left << distances[i - 1][j - 1];
            }
        }
        cout << endl;
    }
    cout << endl;

    clock_t startTime = clock();

    int optimalDistance;
    vector<int> optimalPath = solveTravelingSalesmanProblem(distances, optimalDistance);

    clock_t endTime = clock();
    double executionTime = double(endTime - startTime) / CLOCKS_PER_SEC;

    cout << "Optimal path: ";
    for (int city : optimalPath) {
        cout << city << " ";
    }
    cout << endl;
    cout << "Best path: " << optimalDistance << endl;
    cout << "Execution time: " << executionTime << " seconds" << endl;
    cout << endl;

    return 0;
}