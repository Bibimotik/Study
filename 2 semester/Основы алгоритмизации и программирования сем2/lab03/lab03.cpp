#include <sstream>
#include <iostream>
#include <fstream>
#include <string>
#include <vector>

int main() {
    setlocale(LC_ALL, "russian");
    const std::string FILENAME = "input.txt";

    // Ввод строки с клавиатуры
    std::string inputString;
    std::cout << "Введите строку: ";
    std::getline(std::cin, inputString);

    // Запись строки в файл
    std::ofstream fileOut(FILENAME);
    if (!fileOut.is_open()) {
        std::cerr << "Ошибка при открытии файла" << std::endl;
        return 1;
    }
    fileOut << inputString << std::endl;
    fileOut.close();

    // Чтение файла и вывод слов, содержащих букву "р"
    std::ifstream fileIn(FILENAME);
    if (!fileIn.is_open()) {
        std::cerr << "Ошибка при открытии файла" << std::endl;
        return 1;
    }
    std::string line;
    while (std::getline(fileIn, line)) {
        std::istringstream iss(line);
        std::string word;
        std::vector<std::string> words;
        while (iss >> word) {
            if (word.find('p') != std::string::npos) {
                words.push_back(word);
            }
        }
        for (const auto& w : words) {
            std::cout << w << std::endl;
        }
    }
    fileIn.close();

    return 0;
}
