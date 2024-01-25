#define _CRT_SECURE_NO_WARNINGS // ������ �� ������ ���������� ��������� � �������������� ������������ �������

#include "HashTable.h"
#include <iostream>
#include <cstdlib>
#include <cstring>

// ����������� ������ HashTable, ������� �������������� ������� ��������� �������
HashTable::HashTable(int size) {
    tableSize = size;
    table = new std::vector<Item>[tableSize];
}

// ���������� ������ HashTable, ������� ����������� ���������� ������
HashTable::~HashTable() {
    delete[] table;
}

// ������� hashFunction, ������� ���������� ����� �������������� ����������� ��� ���������� ���-�������� �����
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

// ������� insertItem, ������� ��������� ������� � �������
void HashTable::insertItem(char name[], int year) {
    Item item;
    strcpy(item.name, name);
    item.year = year;
    int index = hashFunction(year); // ��������� ���-�������� �����
    table[index].push_back(item); // ��������� ������� � ��������������� ������
}

// ������� searchItem, ������� ���� ������� � �������
void HashTable::searchItem(int year) {
    int index = hashFunction(year); // ��������� ���-�������� �����
    bool found = false;
    for (int i = 0; i < table[index].size(); i++) {
        if (table[index][i].year == year) { // ���� ������� ������, ������� ��� ��������
            std::cout << "�������� ������: " << table[index][i].name << std::endl;
            found = true;
        }
    }
    if (!found) { // ���� ������� �� ������, ������� ��������� �� ������
        std::cout << "������� �� ������" << std::endl;
    }
}