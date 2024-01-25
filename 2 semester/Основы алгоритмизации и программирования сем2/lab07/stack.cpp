// stack.cpp

#include <iostream>
#include <fstream>
#include "stack.h"

using namespace std;

Stack::Stack() {
    head = nullptr; // Устанавливаем указатель на голову стека в значение нулевого указателя
}

Stack::~Stack() {
    clear(); // Вызываем метод clear для освобождения памяти, выделенной под узлы стека
}

void Stack::push(int data) {
    Node* newNode = new Node; // Создаем новый узел стека
    newNode->data = data; // Устанавливаем значение поля данных узла
    newNode->next = head; // Устанавливаем указатель на следующий узел в цепочке на текущую голову стека
    head = newNode; // Устанавливаем указатель на голову стека на только что созданный узел
}

int Stack::pop() {
    if (isEmpty()) { // Проверяем, пуст ли стек
        cerr << "Stack is empty" << endl; // Если стек пуст, выводим сообщение об ошибке
        return 0; // Возвращаем нулевое значение
    }
    Node* temp = head; // Создаем временный указатель на голову стека
    int data = temp->data; // Сохраняем значение поля данных временного узла
    head = temp->next; // Устанавливаем указатель на голову стека на следующий узел в цепочке
    delete temp; // Освобождаем память, выделенную под временный узел
    return data; // Возвращаем значение поля данных временного узла
}

int Stack::peek() {
    if (isEmpty()) { // Проверяем, пуст ли стек
        cerr << "Stack is empty" << endl; // Если стек пуст, выводим сообщение об ошибке
        return 0; // Возвращаем нулевое значение
    }
    return head->data; // Возвращаем значение поля данных головного узла стека
}

bool Stack::isEmpty() {
    return head == nullptr; // Проверяем, равен ли указатель на голову стека нулевому указателю
}

void Stack::clear() {
    while (!isEmpty()) { // Пока стек не пуст
        pop(); // Извлекаем узел из стека
    }
}

void Stack::saveToFile() {
    ofstream file("stack.txt"); // Открываем файл для записи
    if (!file.is_open()) { // Проверяем, удалось ли открыть файл
        cerr << "Failed to open file for writing" << endl; // Если не удалось, выводим сообщение об ошибке
        return; // Выходим из функции
    }
    Node* current = head; // Создаем указатель на голову стека
    while (current != nullptr) { // Пока указатель на текущий узел не является нулевым указателем
        file << current->data << endl; // Записываем значение поля данных текущего узла в файл
        current = current->next; // Устанавливает указатель
    }
    file.close();// Закрывает файл
}

void Stack::loadFromFile() {
    ifstream file("stack.txt");// Открывает файл для чтения
    if (!file.is_open()) {// Проверка открытия
        cerr << "Failed to open file for reading" << endl;
        return;
    }
    clear();// Очищаем стек
    int data;
    while (file >> data) {// Считываются значения и добавляются в стек
        push(data);
    }
    file.close();// Закрываем файл
}

void Stack::printFromFile() {
    loadFromFile();// Функция загрузки
    if (isEmpty()) {// Проверка
        cout << "Stack is empty" << endl;
        return;
    }
    cout << "Stack contents: ";
    Node* current = head;
    while (current != nullptr) {// Перебираем все элементы стека
        cout << current->data << " ";
        current = current->next;
    }
    cout << endl;
}

void Stack::deleteNegative() {
    Node* current = head;//создаем указатель current на голову стека
    Node* prev = nullptr;//создаем указатель prev, который будет указывать на предыдущий узел в цепочке
    while (current != nullptr) {//роверяем, является ли значение поля data текущего узла отрицательным
        if (current->data < 0) {//проверяем, находится ли удаляемый элемент в начале стека 
            if (prev == nullptr) { // если удаляемый элемент находится в начале стека
                head = current->next;//если удаляемый элемент находится в начале стека, то устанавливаем указатель на голову стека на следующий элемент после удаляемого
            }
            else {//если удаляемый элемент не находится в начале стека
                prev->next = current->next;//устанавливаем указатель на следующий элемент после предыдущего узла так, чтобы он указывал на следующий после удаляемого
            }
            delete current;//удаляем текущий узел из памяти
            return;
        }
        prev = current;
        current = current->next;
    }
}
