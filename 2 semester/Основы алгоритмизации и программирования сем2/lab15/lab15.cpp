#define _CRT_SECURE_NO_WARNINGS // Защита от ошибок компиляции связанных с использованием небезопасных функций

#include <iostream>
#include <chrono>
#include "HashTable.h"

using namespace std;

int main() {
    setlocale(LC_ALL, "Rus"); // Установка локали для вывода русских символов
    // Создаем хеш-таблицы размерами 10, 100 и 1000
    HashTable hashTable10(10);
    HashTable hashTable100(100);
    HashTable hashTable1000(1000);

    // Вставляем случайные элементы в хеш-таблицы
    srand(time(NULL));
    for (int i = 0; i < 1000; i++) {
        char name[50];
        int year = rand() % 10000;
        sprintf(name, "Товар %d", i);
        hashTable10.insertItem(name, year);
        hashTable100.insertItem(name, year);
        hashTable1000.insertItem(name, year);
    }

    // Замеряем время поиска элемента в каждой хеш-таблице
    int searchYear = rand() % 10000;
    cout << "Ищем элемент с годом выпуска " << searchYear << endl;

    auto start10 = chrono::high_resolution_clock::now();
    hashTable10.searchItem(searchYear);
    auto end10 = chrono::high_resolution_clock::now();
    cout << "Время поиска в хеш-таблице размером 10: "
        << chrono::duration_cast<chrono::microseconds>(end10 - start10).count() << " микросекунд" << endl;

    auto start100 = chrono::high_resolution_clock::now();
    hashTable100.searchItem(searchYear);
    auto end100 = chrono::high_resolution_clock::now();
    cout << "Время поиска в хеш-таблице размером 100: "
        << chrono::duration_cast<chrono::microseconds>(end100 - start100).count() << " микросекунд" << endl;

    auto start1000 = chrono::high_resolution_clock::now();
    hashTable1000.searchItem(searchYear);
    auto end1000 = chrono::high_resolution_clock::now();
    cout << "Время поиска в хеш-таблице размером 1000: "
        << chrono::duration_cast<chrono::microseconds>(end1000 - start1000).count() << " микросекунд" << endl;

    return 0;
}