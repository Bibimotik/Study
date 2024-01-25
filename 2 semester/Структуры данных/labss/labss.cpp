#include <iostream>
#include <string>
#include <stdlib.h>
#include <ctime>
#include <windows.h>
#include <fstream>

using namespace std;

const int MAX_GUEST = 100;
struct Guest {
    string name;
    string secondname;
    string patronymic;
    string grade;
    string date;
    string date1;
};

void addGuest(Guest Guests[], int& GuestCount) {
    if (GuestCount < MAX_GUEST) {
        cout << "Guest name: ";
        cin >> Guests[GuestCount].name;
        cout << "Enter secondname: ";
        cin >> Guests[GuestCount].secondname;
        cout << "Enter patronymic: ";
        cin >> Guests[GuestCount].patronymic;
        cout << "Enter grade of room: ";
        cin >> Guests[GuestCount].grade;
        cout << "Enter arrival date: ";
        cin >> Guests[GuestCount].date;
        cout << "Enter departure date: ";
        cin >> Guests[GuestCount].date1;
        cout << endl;
        GuestCount++;
        system("cls");
    }
    else {
        cout << "The array is full" << endl;
    }
}
void printGuests(Guest Guests[], int GuestCount) {
    for (int i = 0; i < GuestCount; i++) {
        cout << "Guest name: " << Guests[i].name << " " << Guests[i].secondname << " " << Guests[i].patronymic << endl;
        cout << "Grade of room: " << Guests[i].grade << endl;
        cout << "Arrival date: " << Guests[i].date << endl;
        cout << "Departure date: " << Guests[i].date1 << endl;
        cout << endl;
    }
}
void standartSearchGuest(Guest Guests[], int GuestCount) {
    string secondname;
    cout << "Enter secondname of Guest to search: ";
    cin >> secondname;
    for (int i = 0; i < GuestCount; i++) {
        if (Guests[i].secondname == secondname) {
            cout << "Guest name: " << Guests[i].name << " " << Guests[i].secondname << " " << Guests[i].patronymic << endl;
            cout << "Class of room: " << Guests[i].grade << endl;
            cout << "Arrival date: " << Guests[i].date << endl;
            cout << "Departure date: " << Guests[i].date1 << endl;
            cout << endl;
        }
    }
}
void writeToFile(Guest Guests[], int GuestCount) {
    ofstream fout("Guests.txt");
    for (int i = 0; i < GuestCount; i++) {
        fout << "Guest name: " << Guests[i].name << " " << Guests[i].secondname << " " << Guests[i].patronymic << endl;
        fout << "Class of room: " << Guests[i].grade << endl;
        fout << "Arrival date: " << Guests[i].date << endl;
        fout << "Departure date: " << Guests[i].date1 << endl;
        fout << endl;
    }
    fout.close();
}
void readFromFile(Guest Guests[], int& GuestCount) {
    ifstream fin("Guests.txt");
    string line;
    while (getline(fin, line)) {
        cout << line << endl;
    }
    fin.close();
}
void deleteGuest(Guest Guests[], int& GuestCount) {
    string secondname;
    cout << "Enter secondname of Guest to delete: ";
    cin >> secondname;
    for (int i = 0; i < GuestCount; i++) {
        if (Guests[i].secondname == secondname) {
            for (int j = i; j < GuestCount - 1; j++) {
                Guests[j] = Guests[j + 1];
            }
            GuestCount--;
        }
    }
    system("cls");
}
void deleteAllGuest(Guest Guests[], int& GuestCount) {
    for (int i = 0; i < MAX_GUEST; i++) {
        Guests[i].~Guest();
        GuestCount = 0;
    }
    system("cls");
}
void sortAllGuests(Guest Guests[], int& GuestCount) {
    int choise;
    cout << "1. Sort by top grade" << endl;
    cout << "2. Sort by low grade" << endl;
    cin >> choise;
    switch (choise)
    {
    case 1:
        for (int i = 0; i < MAX_GUEST; i++) {
            if (Guests[i].grade == "lux") {
                cout << "Guest name: " << Guests[i].name << " " << Guests[i].secondname << " " << Guests[i].patronymic << endl;
                cout << "Class of room: " << Guests[i].grade << endl;
                cout << "Arrival date: " << Guests[i].date << endl;
                cout << "Departure date: " << Guests[i].date1 << endl;
                cout << endl;
            }
        }
        for (int i = 0; i < MAX_GUEST; i++) {
            if (Guests[i].grade == "medium") {
                cout << "Guest name: " << Guests[i].name << " " << Guests[i].secondname << " " << Guests[i].patronymic << endl;
                cout << "Class of room: " << Guests[i].grade << endl;
                cout << "Arrival date: " << Guests[i].date << endl;
                cout << "Departure date: " << Guests[i].date1 << endl;
                cout << endl;
            }
        }
        for (int i = 0; i < MAX_GUEST; i++) {
            if (Guests[i].grade == "standart") {
                cout << "Guest name: " << Guests[i].name << " " << Guests[i].secondname << " " << Guests[i].patronymic << endl;
                cout << "Class of room: " << Guests[i].grade << endl;
                cout << "Arrival date: " << Guests[i].date << endl;
                cout << "Departure date: " << Guests[i].date1 << endl;
                cout << endl;
            }
        }
        break;
    case 2:
        for (int i = 0; i < MAX_GUEST; i++) {
            if (Guests[i].grade == "standart") {
                cout << "Guest name: " << Guests[i].name << " " << Guests[i].secondname << " " << Guests[i].patronymic << endl;
                cout << "Class of room: " << Guests[i].grade << endl;
                cout << "Arrival date: " << Guests[i].date << endl;
                cout << "Departure date: " << Guests[i].date1 << endl;
                cout << endl;
            }
        }
        for (int i = 0; i < MAX_GUEST; i++) {
            if (Guests[i].grade == "medium") {
                cout << "Guest name: " << Guests[i].name << " " << Guests[i].secondname << " " << Guests[i].patronymic << endl;
                cout << "Class of room: " << Guests[i].grade << endl;
                cout << "Arrival date: " << Guests[i].date << endl;
                cout << "Departure date: " << Guests[i].date1 << endl;
                cout << endl;
            }
        }
        for (int i = 0; i < MAX_GUEST; i++) {
            if (Guests[i].grade == "lux") {
                cout << "Guest name: " << Guests[i].name << " " << Guests[i].secondname << " " << Guests[i].patronymic << endl;
                cout << "Class of room: " << Guests[i].grade << endl;
                cout << "Arrival date: " << Guests[i].date << endl;
                cout << "Departure date: " << Guests[i].date1 << endl;
                cout << endl;
            }
        }
        break;
    default:
        cout << "Enter right choice" << endl;
        break;
    }
}
void searchBySubstring(Guest Guests[], int& GuestCount) {
    string substring;
    cout << "Enter substring: " << endl;
    cin >> substring;
    for (int i = 0; i < MAX_GUEST; i++) {
        size_t pos = Guests[i].secondname.find(substring);
        if (pos != string::npos) {
            cout << "Guest name: " << Guests[i].name << " " << Guests[i].secondname << " " << Guests[i].patronymic << endl;
            cout << "Class of room: " << Guests[i].grade << endl;
            cout << "Arrival date: " << Guests[i].date << endl;
            cout << "Departure date: " << Guests[i].date1 << endl;
        }
    }
}
int searchGuestIndex(Guest Guests[], int GuestCount, string secondname) {
    for (int i = 0; i < GuestCount; i++) {
        if (Guests[i].secondname == secondname) {
            return i;
        }
    }
    return -1;
}

void searchGuest(Guest Guests[], int GuestCount) {
    string secondname;
    cout << "Enter secondname of Guest to search: ";
    cin >> secondname;

    int index = searchGuestIndex(Guests, GuestCount, secondname);
    if (index != -1) {
        cout << "Guest name: " << Guests[index].name << " " << Guests[index].secondname << " " << Guests[index].patronymic << endl;
        cout << "Grade of room: " << Guests[index].grade << endl;
        cout << "Arrival date: " << Guests[index].date << endl;
        cout << "Departure date: " << Guests[index].date1 << endl;
        cout << endl;
    }
    else {
        cout << "Guest not found." << endl;
    }
}
void sortGuests(Guest Guests[], int GuestCount) {
    bool sorted = false;
    while (!sorted) {
        sorted = true;
        for (int i = 0; i < GuestCount - 1; i++) {
            if (Guests[i].secondname > Guests[i + 1].secondname) {
                swap(Guests[i], Guests[i + 1]);
                sorted = false;
            }
        }
    }
}
void binarySearchGuest(Guest Guests[], int GuestCount) {
    string secondname;
    cout << "Enter secondname of Guest to search: ";
    cin >> secondname;

    sortGuests(Guests, GuestCount);

    int left = 0;
    int right = GuestCount - 1;
    while (left <= right) {
        int mid = (left + right) / 2;
        if (Guests[mid].secondname == secondname) {
            cout << "Guest name: " << Guests[mid].name << " " << Guests[mid].secondname << " " << Guests[mid].patronymic << endl;
            cout << "Class of room: " << Guests[mid].grade << endl;
            cout << "Arrival date: " << Guests[mid].date << endl;
            cout << "Departure date: " << Guests[mid].date1 << endl;
            cout << endl;
            return;
        }
        else if (Guests[mid].secondname < secondname) {
            left = mid + 1;
        }
        else {
            right = mid - 1;
        }
    }
    cout << "Guest not found" << endl;
}
int main() {
    Guest Guests[MAX_GUEST];
    int GuestCount = 0;
    int choice;
    int people;
    cout << "Enter how many people: " << endl;
    cin >> people;
    do {
        cout << "MENU" << endl;
        cout << "1. Add Guest" << endl;
        cout << "2. Print all Guests" << endl;
        cout << "3. Search by secondname" << endl;
        cout << "4. Write to file" << endl;
        cout << "5. Read from file" << endl;
        cout << "6. Delete guest by secondname" << endl;
        cout << "7. Delete all guests" << endl;
        cout << "8. Sort all guests by the grade" << endl;
        cout << "9. Search by substring in the surname" << endl;
        cout << "10. Search by index" << endl;
        cout << "11. Binary search" << endl;
        cout << "0. Exit" << endl;
        cout << "Enter your choice: ";
        cin >> choice;

        switch (choice) {
        case 1:
            system("cls");
            for (int i = 0; i < people; i++) {
                addGuest(Guests, GuestCount);
            }
            break;
        case 2:
            system("cls");
            printGuests(Guests, GuestCount);
            break;
        case 3:
            system("cls");
            standartSearchGuest(Guests, GuestCount);
            break;
        case 4:
            system("cls");
            writeToFile(Guests, GuestCount);
            break;
        case 5:
            system("cls");
            readFromFile(Guests, GuestCount);
            break;
        case 6:
            system("cls");
            deleteGuest(Guests, GuestCount);
            break;
        case 7:
            system("cls");
            deleteAllGuest(Guests, GuestCount);
            break;
        case 8:
            system("cls");
            sortAllGuests(Guests, GuestCount);
            break;
        case 9:
            system("cls");
            searchBySubstring(Guests, GuestCount);
        case 10:
            system("cls");
            searchGuest(Guests, GuestCount);
        case 11:
            system("cls");
            binarySearchGuest(Guests, GuestCount);
        case 0:
            break;
        default:
            cout << "Invalid choice" << endl;
        }
    } while (choice != 0);

    return 0;
}
