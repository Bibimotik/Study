#include <iostream>
#include <vector>

using namespace std;

// Функция для генерации перестановок
void generatePermutations(vector<int>& elements, int index) {
    // Проверяем, достигли ли мы конца вектора элементов
    if (index == elements.size() - 1) {
        // Выводим текущую перестановку
        for (int num : elements) {
            cout << num << " ";
        }
        cout << endl;
        return;
    }

    // Генерируем перестановки, начиная с текущего индекса
    for (int i = index; i < elements.size(); i++) {
        // Меняем текущий элемент с элементом на индексе i
        swap(elements[index], elements[i]);

        // Рекурсивно генерируем перестановки, начиная со следующего индекса
        generatePermutations(elements, index + 1);

        // Возвращаем элементы в исходное состояние
        swap(elements[index], elements[i]);
    }
}

int main() {
    vector<int> elements = { 1, 2, 3, 4 };
    generatePermutations(elements, 0);

    return 0;
}
