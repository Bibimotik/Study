#include <iostream>
#include <vector>

using namespace std;

void printSet(const vector<int>& set) {
    for (int num : set) {
        cout << num << " ";
    }
    cout << endl << endl;
}

// Функция для генерации сочетаний
void generateCombinations(const vector<int>& set, vector<int>& combination, int index, int k) {
    // Проверяем, достигли ли мы размера k для сочетания
    if (combination.size() == k) {
        for (int num : combination) {
            cout << num << " ";
        }
        cout << endl;
        return;
    }

    // Генерируем сочетания, начиная с текущего индекса
    for (int i = index; i < set.size(); i++) {
        // Добавляем элемент в сочетание
        combination.push_back(set[i]);

        // Рекурсивно генерируем сочетания, начиная со следующего элемента
        generateCombinations(set, combination, i + 1, k);

        // Удаляем последний добавленный элемент для генерации следующего сочетания
        combination.pop_back();
    }
}

int main() {
    vector<int> set = { 1, 2, 3, 4 };
    int k = 3; // Размер сочетания
    vector<int> combination;
    printSet(set);
    generateCombinations(set, combination, 0, k);

    return 0;
}
