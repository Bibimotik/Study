#include <iostream>
#include <fstream>
using namespace std;

struct Node {
    int data;
    Node* next;
};

Node* head = NULL;

void insertNode(int value) {
    Node* newNode = new Node();
    newNode->data = value;
    newNode->next = NULL;

    if (head == NULL) {
        head = newNode;
    }
    else {
        Node* current = head;
        while (current->next != NULL) {
            current = current->next;
        }
        current->next = newNode;
    }
}

void deleteNode(int value) {
    if (head == NULL) {
        cout << "List is empty." << endl;
    }
    else if (head->data == value) {
        Node* temp = head;
        head = head->next;
        delete temp;
    }
    else {
        Node* current = head;
        while (current->next != NULL && current->next->data != value) {
            current = current->next;
        }
        if (current->next == NULL) {
            cout << "Element not found." << endl;
        }
        else {
            Node* temp = current->next;
            current->next = current->next->next;
            delete temp;
        }
    }
}

void displayList() {
    if (head == NULL) {
        cout << "List is empty." << endl;
    }
    else {
        Node* current = head;
        while (current != NULL) {
            cout << current->data << " ";
            current = current->next;
        }
        cout << endl;
    }
}

void writeToFile() {
    ofstream file("list.txt");
    if (file.is_open()) {
        Node* current = head;
        while (current != NULL) {
            file << current->data << endl;
            current = current->next;
        }
        file.close();
        cout << "List written to file successfully." << endl;
    }
    else {
        cout << "Unable to open file." << endl;
    }
}

void readFromFile() {
    ifstream file("list.txt");
    if (file.is_open()) {
        int value;
        while (file >> value) {
            insertNode(value);
        }
        file.close();
        cout << "List read from file successfully." << endl;
    }
    else {
        cout << "Unable to open file." << endl;
    }
}

int sumOfPositiveTwoDigitElements() {
    int sum = 0;
    Node* current = head;
    while (current != NULL) {
        if (current->data > 9 && current->data < 100) {
            sum += current->data;
        }
        current = current->next;
    }
    return sum;
}

int main() {
    int choice, value;
    cout << "Menu:" << endl;
    cout << "1. Add element" << endl;
    cout << "2. Delete element" << endl;
    cout << "3. Display list" << endl;
    cout << "4. Write to file" << endl;
    cout << "5. Read from file" << endl;
    cout << "6. Calculate" << endl;
    cin >> choice;
    switch (choice) {
    case 1:
        cout << "Enter value to add: ";
        cin >> value;
        insertNode(value);
        main();
        break;
    case 2:
        cout << "Enter value to delete: ";
        cin >> value;
        deleteNode(value);
        main();
        break;
    case 3:
        displayList();
        main();
        break;
    case 4:
        writeToFile();
        main();
        break;
    case 5:
        readFromFile();
        main();
        break;
    case 6:
        cout << "Sum of positive two-digit elements in the list: " << sumOfPositiveTwoDigitElements() << endl;
        main();
        break;
    default:
        cout << "Invalid choice." << endl;
        main();
        break;
    }
    return 0;
}
