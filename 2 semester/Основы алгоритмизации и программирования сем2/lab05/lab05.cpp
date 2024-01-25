#include <iostream>
#include <vector>
#include <Windows.h>
#include <string>
#include <algorithm>
#include <ranges>

using namespace std;
// определяет структуру даты, представленной в виде битовых полей
struct day_bytes{
    unsigned int week_day : 3;
    unsigned int day : 5;
    unsigned int month : 4;
    unsigned int year : 12;
};
// описание структуры
struct citizen{
    std::string name_surname;
    std::string product;
    int mark_size;
    int sum;
    day_bytes date_of_return;
    int shelf_life;
};
// определяет перегрузку оператора для вывода записи о работнике в стандартный поток вывода
std::ostream& operator<<(std::ostream& os, const citizen& citizens){
    os << citizens.name_surname << '\n' << citizens.product << '\n' << citizens.mark_size << '\n' << citizens.sum
        << '\n' << citizens.shelf_life << '\n' << citizens.date_of_return.day << '.' << citizens.date_of_return.month
        << '.' << citizens.date_of_return.year << '\n';
    return os;
}
//функция для ввода данных о работниках в базу данных
void include_citizens(std::vector<citizen>& guesses, int& n){
    for (int i = 0; i < n; i++)
    {
        int count = NULL;
        int gender = NULL;
        int day = NULL, month = NULL, year = NULL;
        cout << endl << "ФИО: "; cin >> guesses[i].name_surname;
        cout << endl << "Наименование товара: "; cin >> guesses[i].product;
        cout << endl << "Оценочная стоимость: "; cin >> guesses[i].mark_size;
        cout << endl << "Сумма, выданная под залог: "; cin >> guesses[i].sum;
        cout << endl << "Дата сдачи: "; cin >> day; cin >> month; cin >> year;
        guesses[i].date_of_return.day = day;
        guesses[i].date_of_return.month = month;
        guesses[i].date_of_return.year = year;
        cout << endl << "Срок хранения: "; cin >> guesses[i].shelf_life;
    }
}
//функция для поиска товара по дате
std::vector<citizen> search_by_date(const std::vector<citizen>& citizens, const day_bytes& date) {
    std::vector<citizen> result;
    for (const auto& c : citizens)
    {
        if (c.date_of_return.day == date.day && c.date_of_return.month == date.month && c.date_of_return.year == date.year)//если совпадает то выводит структуру
        {
            result.push_back(c);
        }
    }
    return result;
}

// выводит все структуры
void print_all_citizens(const std::vector<citizen>& citizens){
    for (const auto& c : citizens) {
        std::cout << c << std::endl;
    }
}
// удаляет структуры по критерию (фамилии)
void delete_citizen_by_surname(std::vector<citizen>& citizens){
    std::string surname_to_delete;
    cout << "Введите фамилию товара, которого хотите удалить: ";
    cin >> surname_to_delete;
    citizens.erase(std::remove_if(citizens.begin(), citizens.end(), [surname_to_delete](const citizen& c) {return c.name_surname == surname_to_delete; }), citizens.end());
}
int main(){
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    std::vector<citizen> citizens;
    int n = 0;
    day_bytes date;

    int choice = 0;
    do {
        cout << "\nМеню:" << endl;
        cout << "1. Ввод данных о товарах" << endl;
        cout << "2. Поиск товара по дате" << endl;
        cout << "3. Вывод всех товаров" << endl;
        cout << "4. Удаление товара по фамилии" << endl;
        cout << "5. Выход" << endl;
        cout << "Выберите действие: ";
        cin >> choice;
        switch (choice)
        {
        case 1:
            cout << "Введите количество товаров: ";
            cin >> n;
            citizens.resize(n);
            include_citizens(citizens, n);
            break;
        case 2:

        case 3:
            print_all_citizens(citizens);
            break;
        case 4:
            delete_citizen_by_surname(citizens);
            break;
        case 5:
            cout << "До свидания!" << endl;
            break;
        default:
            cout << "Некорректный ввод. Попробуйте еще раз." << endl;
            break;
        }
    } while (choice != 5);

    return 0;
}
