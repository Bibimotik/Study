// stack.cpp

#include <iostream>
#include <fstream>
#include "stack.h"

using namespace std;

Stack::Stack() {
    head = nullptr; // ������������� ��������� �� ������ ����� � �������� �������� ���������
}

Stack::~Stack() {
    clear(); // �������� ����� clear ��� ������������ ������, ���������� ��� ���� �����
}

void Stack::push(int data) {
    Node* newNode = new Node; // ������� ����� ���� �����
    newNode->data = data; // ������������� �������� ���� ������ ����
    newNode->next = head; // ������������� ��������� �� ��������� ���� � ������� �� ������� ������ �����
    head = newNode; // ������������� ��������� �� ������ ����� �� ������ ��� ��������� ����
}

int Stack::pop() {
    if (isEmpty()) { // ���������, ���� �� ����
        cerr << "Stack is empty" << endl; // ���� ���� ����, ������� ��������� �� ������
        return 0; // ���������� ������� ��������
    }
    Node* temp = head; // ������� ��������� ��������� �� ������ �����
    int data = temp->data; // ��������� �������� ���� ������ ���������� ����
    head = temp->next; // ������������� ��������� �� ������ ����� �� ��������� ���� � �������
    delete temp; // ����������� ������, ���������� ��� ��������� ����
    return data; // ���������� �������� ���� ������ ���������� ����
}

int Stack::peek() {
    if (isEmpty()) { // ���������, ���� �� ����
        cerr << "Stack is empty" << endl; // ���� ���� ����, ������� ��������� �� ������
        return 0; // ���������� ������� ��������
    }
    return head->data; // ���������� �������� ���� ������ ��������� ���� �����
}

bool Stack::isEmpty() {
    return head == nullptr; // ���������, ����� �� ��������� �� ������ ����� �������� ���������
}

void Stack::clear() {
    while (!isEmpty()) { // ���� ���� �� ����
        pop(); // ��������� ���� �� �����
    }
}

void Stack::saveToFile() {
    ofstream file("stack.txt"); // ��������� ���� ��� ������
    if (!file.is_open()) { // ���������, ������� �� ������� ����
        cerr << "Failed to open file for writing" << endl; // ���� �� �������, ������� ��������� �� ������
        return; // ������� �� �������
    }
    Node* current = head; // ������� ��������� �� ������ �����
    while (current != nullptr) { // ���� ��������� �� ������� ���� �� �������� ������� ����������
        file << current->data << endl; // ���������� �������� ���� ������ �������� ���� � ����
        current = current->next; // ������������� ���������
    }
    file.close();// ��������� ����
}

void Stack::loadFromFile() {
    ifstream file("stack.txt");// ��������� ���� ��� ������
    if (!file.is_open()) {// �������� ��������
        cerr << "Failed to open file for reading" << endl;
        return;
    }
    clear();// ������� ����
    int data;
    while (file >> data) {// ����������� �������� � ����������� � ����
        push(data);
    }
    file.close();// ��������� ����
}

void Stack::printFromFile() {
    loadFromFile();// ������� ��������
    if (isEmpty()) {// ��������
        cout << "Stack is empty" << endl;
        return;
    }
    cout << "Stack contents: ";
    Node* current = head;
    while (current != nullptr) {// ���������� ��� �������� �����
        cout << current->data << " ";
        current = current->next;
    }
    cout << endl;
}

void Stack::deleteNegative() {
    Node* current = head;//������� ��������� current �� ������ �����
    Node* prev = nullptr;//������� ��������� prev, ������� ����� ��������� �� ���������� ���� � �������
    while (current != nullptr) {//��������, �������� �� �������� ���� data �������� ���� �������������
        if (current->data < 0) {//���������, ��������� �� ��������� ������� � ������ ����� 
            if (prev == nullptr) { // ���� ��������� ������� ��������� � ������ �����
                head = current->next;//���� ��������� ������� ��������� � ������ �����, �� ������������� ��������� �� ������ ����� �� ��������� ������� ����� ����������
            }
            else {//���� ��������� ������� �� ��������� � ������ �����
                prev->next = current->next;//������������� ��������� �� ��������� ������� ����� ����������� ���� ���, ����� �� �������� �� ��������� ����� ����������
            }
            delete current;//������� ������� ���� �� ������
            return;
        }
        prev = current;
        current = current->next;
    }
}
