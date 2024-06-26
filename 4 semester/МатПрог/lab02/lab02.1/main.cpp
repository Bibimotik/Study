#include <iostream>
#include <vector>

using namespace std;

void printSet(const vector<int>& set) {
    for (int num : set) {
        cout << num << " ";
    }
    cout << endl;
}

// Функция для генерации подмножеств
void generateSubsets(const vector<int>& set, vector<int>& subset, int index) {
    // Выводим текущее подмножество
    for (int num : subset) {
        cout << num << " ";
    }
    cout << endl;

    int i = index;
    // Генерируем подмножества, начиная с текущего индекса
    for (i; i < set.size(); i++) {
        // Добавляем элемент в подмножество
        subset.push_back(set[i]);

        // Рекурсивно генерируем подмножества, начиная с следующего элемента
        generateSubsets(set, subset, i + 1);

        // Удаляем последний добавленный элемент для генерации следующего подмножества
        subset.pop_back();
    }
}

int main() {
    vector<int> set = { 1, 2, 3, 4 };
    vector<int> subset;
    printSet(set);
    generateSubsets(set, subset, 0);

    return 0;
}
