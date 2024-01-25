#include <iostream>
#include <fstream>
#include <queue>
#include <cctype>

using namespace std;

int main() {
    // Открываем файлы для чтения и записи
    ifstream f("f.txt");
    ofstream g("g.txt");

    // Проверяем, что файлы открылись успешно
    if (!f || !g) {
        cout << "Ошибка при открытии файлов" << endl;
        return 1;
    }

    // Создаем две очереди для хранения символов и цифр
    queue<char> chars;
    queue<char> digits;

    // Читаем файл f посимвольно
    char c;
    while (f.get(c)) {
        // Если символ - цифра, добавляем его в очередь digits
        if (isdigit(c)) {
            digits.push(c);
        }
        // Иначе, добавляем его в очередь chars
        else {
            chars.push(c);
        }
        // Если символ - перевод строки или конец файла, записываем содержимое очередей в файл g
        if (c == '\n' || f.eof()) {
            // Пока очередь chars не пуста, записываем ее первый элемент в файл g и удаляем его из очереди
            while (!chars.empty()) {
                g << chars.front();
                chars.pop();
            }
            // Пока очередь digits не пуста, записываем ее первый элемент в файл g и удаляем его из очереди
            while (!digits.empty()) {
                g << digits.front();
                digits.pop();
            }
            // Записываем перевод строки в файл g
            g << '\n';
        }
    }

    // Закрываем файлы
    f.close();
    g.close();

    return 0;
}