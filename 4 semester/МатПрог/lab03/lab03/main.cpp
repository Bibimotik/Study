#include <iostream>
#include <vector>
#include <algorithm>
#include <limits>
#include <iomanip>

using namespace std;

const int INF = numeric_limits<int>::max();

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

    // Замыкание пути к начальному городу
    bestPath.push_back(bestPath[0]);
    totalDistance = bestDistance;
    return bestPath;
}

int main() {
    vector<vector<int>> distances = {
            {INF, 2, 22, INF, 1},
            {1, INF, 16, 67, 83},
            {3, 3, INF, 86, 50},
            {18, 57, 4, INF, 3},
            {92, 67, 52, 14, INF}
    };

    int n = distances.size();

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

    int optimalDistance;
    vector<int> optimalPath = solveTravelingSalesmanProblem(distances, optimalDistance);

    cout << "Optimal path: ";
    for (int city : optimalPath) {
        cout << city << " ";
    }
    cout << endl;
    cout << "Best path: " << optimalDistance << endl;

    return 0;
}