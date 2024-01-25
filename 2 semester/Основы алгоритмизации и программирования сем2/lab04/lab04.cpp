#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;
// определите структуру для представления санатория с названием, местоположением, медицинским профилем и количеством ваучеров
struct Sanatorium {
    string name;
    string location;
    string medical_profile;
    int num_vouchers;
};
// определите функцию для сравнения двух санаториев по названию
bool compareSanatoriumByName(const Sanatorium& s1, const Sanatorium& s2) {
    return s1.name < s2.name;
}

int main() {
    setlocale(LC_ALL, "Russian");
    // определите вектор санаториев
    vector<Sanatorium> sanatoriums = {
            {"Ynost", "Minsk", "Profile 1", 10},
            {"Andorium", "Borisov", "Profile 3", 5},
            {"Greck", "Vraclav", "Profile 2", 15},
            {"Earth", "Mogilev", "Profile 1", 20}
    };
    // // определите вектор для хранения уникальных медицинских профилей санатория
    vector<string> medicalProfiles;
    for (const auto& sanatorium : sanatoriums) {
        if (find(medicalProfiles.begin(), medicalProfiles.end(), sanatorium.medical_profile) == medicalProfiles.end()) {
            medicalProfiles.push_back(sanatorium.medical_profile);
        }
    }
    // отсортировать вектор медицинских профилей в алфавитном порядке
    sort(medicalProfiles.begin(), medicalProfiles.end());
    // выполнить итерацию по вектору медицинских профилей
    for (const auto& medicalProfile : medicalProfiles) {
        // вывести название медицинского профиля
        cout << "Medical Profile: " << medicalProfile << endl;
        // распечатать строку заголовка для таблицы санаториев для текущего медицинского профиля
        cout << "------------------------------------------------------------------" << endl;
        cout << "Название              | Место                | Количество         " << endl;
        cout << "------------------------------------------------------------------" << endl;
        // определите вектор для сохранения санатория для текущего медицинского профиля
        vector<Sanatorium> sanatoriumsForProfile;
        for (const auto& sanatorium : sanatoriums) {
        // если текущий санаторий имеет текущий медицинский профиль, добавьте его в вектор санаториев для текущего профиля
            if (sanatorium.medical_profile == medicalProfile) {
                sanatoriumsForProfile.push_back(sanatorium);
            }
        }
        // отсортировать вектор санаториев для текущего профиля по названию
        sort(sanatoriumsForProfile.begin(), sanatoriumsForProfile.end(), compareSanatoriumByName);
        for (const auto& sanatorium : sanatoriumsForProfile) {
            printf("| %-20s | %-20s | %-18d |\n", sanatorium.name.c_str(), sanatorium.location.c_str(), sanatorium.num_vouchers);
        }
        cout << "------------------------------------------------------------------" << endl;
        cout << endl;
    }

    return 0;
}
