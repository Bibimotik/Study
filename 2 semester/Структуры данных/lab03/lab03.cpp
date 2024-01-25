#include <fstream>
#include <iostream>
#include <string>
#include <Windows.h>

int _operation;

void Record_data(const std::string& filename) {
    std::ofstream outFile(filename);
    if (outFile.is_open()) {
        std::string data;
        std::cout << "Enter the data to write to the file: " << std::endl;
        while (std::getline(std::cin, data) && data != "end") {
            outFile << data << std::endl;
        }
        outFile.close();
        std::cout << "The data has been successfully written to the file " << filename << std::endl;
    }
    else {
        std::cerr << "Could not open the file " << filename << std::endl;
    }
}

void Read_data(const std::string& filename) {
    std::ifstream file(filename);
    if (!file) {
        std::cerr << "File opening error " << filename << std::endl;
        return;
    }
    std::cout << "File Contents " << filename << ":\n";
    std::string line;
    while (std::getline(file, line)) {
        std::cout << line << std::endl;
    }
    file.close();
}

void Delete_data(const std::string& filename) {
    std::ofstream file(filename, std::ios::out | std::ios::trunc);
    if (!file) {
        std::cerr << "File opening error " << filename << std::endl;
        return;
    }
    std::cout << "File Contents " << filename << " delete" << std::endl;
    file.close();
}

void Delete_file(const std::string& filename) {
    if (std::remove(filename.c_str()) != 0) {
        std::cerr << "File Deletion error " << filename << std::endl;
    }
    else {
        std::cout << "File " << filename << " delete" << std::endl;
    }
}

int main()
{
    std::string filename;
    std::cout << "Choose: " << std::endl;
    std::cout << " 1. record data" << std::endl;
    std::cout << " 2. read data" << std::endl;
    std::cout << " 3. delete data" << std::endl;
    std::cout << " 4. delete file" << std::endl;
    std::cin >> _operation;
    std::cout << "Write file name: " << std::endl;
    std::cin >> filename;
    switch (_operation) {
        case 1:
            
            system("cls");
            Record_data(filename);
            system("cls");
            main();
            break;
        case 2:
            system("cls");
            Read_data(filename);
            main();
            break;
        case 3:
            system("cls");
            Delete_data(filename);
            system("cls");
            main();
            break;
        case 4:
            system("cls");
            Delete_file(filename);
            system("cls");
            main();
            break;
        default:
            system("cls");
            std::cout << "Choose correct option!" << std::endl;
            main();
    }
}