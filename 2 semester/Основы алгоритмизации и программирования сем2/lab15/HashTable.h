#ifndef HASHTABLE_H
#define HASHTABLE_H

#include <vector>

struct Item {
    char name[50];
    int year;
};

class HashTable {
private:
    int tableSize;
    std::vector<Item>* table;

    int hashFunction(int key);

public:
    HashTable(int size);
    ~HashTable();

    void insertItem(char name[], int year);
    void searchItem(int year);
};

#endif