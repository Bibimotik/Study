#define _CRT_SECURE_NO_WARNINGS // Защита от ошибок компиляции связанных с использованием небезопасных функций

#include "HashTable.h"
#include <iostream>
#include <cstdlib>
#include <cstring>

// Конструктор класса HashTable, который инициализирует таблицу заданного размера
HashTable::HashTable(int size) {
    tableSize = size;
    table = new std::vector<Item>[tableSize];
}

// Деструктор класса HashTable, который освобождает выделенную память
HashTable::~HashTable() {
    delete[] table;
}

// Функция hashFunction, которая использует метод универсального хеширования для вычисления хеш-значения ключа
int HashTable::hashFunction(int key) {
    int p = 1;
    int a = rand() % (key - 1) + 1;
    int b = rand() % key;
    int sum = 0;
    for (int i = 0; i < 4; i++) {
        sum += ((key / p) % 10) * a + b;
        p *= 10;
    }
    return sum % tableSize;
}

// Функция insertItem, которая вставляет элемент в таблицу
void HashTable::insertItem(char name[], int year) {
    Item item;
    strcpy(item.name, name);
    item.year = year;
    int index = hashFunction(year); // Вычисляем хеш-значение ключа
    table[index].push_back(item); // Вставляем элемент в соответствующий вектор
}

// Функция searchItem, которая ищет элемент в таблице
void HashTable::searchItem(int year) {
    int index = hashFunction(year); // Вычисляем хеш-значение ключа
    bool found = false;
    for (int i = 0; i < table[index].size(); i++) {
        if (table[index][i].year == year) { // Если элемент найден, выводим его название
            std::cout << "Название товара: " << table[index][i].name << std::endl;
            found = true;
        }
    }
    if (!found) { // Если элемент не найден, выводим сообщение об ошибке
        std::cout << "Элемент не найден" << std::endl;
    }
}