#include <iostream>
#include <vector>

using namespace std;

void printSet(const vector<int>& set) {
    for (int num : set) {
        cout << num << " ";
    }
    cout << endl << endl;
}

// Функция для генерации размещений
void generatePermutations(vector<int>& elements, int index, int k) {
    // Проверяем, достигли ли мы конца вектора элементов
    if (index == k) {
        // Выводим текущее размещение
        for (int i = 0; i < k; i++) {
            cout << elements[i] << " ";
        }
        cout << endl;
        return;
    }

    // Генерируем размещения, начиная с текущего индекса
    for (int i = index; i < elements.size(); i++) {
        // Меняем текущий элемент с элементом на индексе i
        swap(elements[index], elements[i]);

        // Рекурсивно генерируем размещения, начиная со следующего индекса
        generatePermutations(elements, index + 1, k);

        // Возвращаем элементы в исходное состояние
        swap(elements[index], elements[i]);
    }
}

int main() {
    vector<int> elements = { 1, 2, 3, 4 };
    int k = 2;
    printSet(elements);
    generatePermutations(elements, 0, k);

    return 0;
}
