#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <limits>
#include <map>
#include <numeric>

#define total 0
struct Student { //инициализируем структуру
    std::string surname; //фамилия
    std::string firstName; //имя
    std::string patronymic; //отчество
    int birthYear; //год рождения
    int course; //курс
    int groupNumber; //номер группы
    std::vector<int> grades; //оценки
};

bool compareStudents(const Student& s1, const Student& s2) { //сравниваем студентов
    if (s1.course != s2.course) { //если курсы не равны
        return s1.course < s2.course; //возвращаем курс
    }
    return s1.surname < s2.surname; //иначе возвращаем фамилию
}

void sortByCourseAndName(std::vector<Student>& students) {      //сортируем по курсу и имени
    std::sort(students.begin(), students.end(), compareStudents);
}

//. Найти средний балл каждой группы по каждому предмету (всего 5 предметов).
void findAverageGrade(const std::vector<Student>& students) { //находим средний балл
    std::map<int, std::vector<int>> gradesByGroup; //инициализируем карту
    for (const auto& student : students) { //для каждого студента
        gradesByGroup[student.groupNumber].push_back(student.grades[0]); //добавляем оценки
        gradesByGroup[student.groupNumber].push_back(student.grades[1]);
        gradesByGroup[student.groupNumber].push_back(student.grades[2]);
        gradesByGroup[student.groupNumber].push_back(student.grades[3]);
        gradesByGroup[student.groupNumber].push_back(student.grades[4]);
    }
    for (const auto& group : gradesByGroup) {   //для каждой группы
        double averageGrade = std::accumulate(group.second.begin(), group.second.end(), 0.0) / group.second.size(); //считаем средний балл
        std::cout << "Group " << group.first << ": " << averageGrade << std::endl; //выводим в консоль
    }
}



void findOldestAndYoungest(const std::vector<Student>& students) { //находим самого старшего и самого младшего студента
    int oldestYear = std::numeric_limits<int>::max(); //инициализируем переменные
    std::string oldestName; //инициализируем переменные
    int youngestYear = std::numeric_limits<int>::min(); //инициализируем переменные
    std::string youngestName; //инициализируем переменные
    for (const auto& student : students) { //для каждого студента
        if (student.birthYear < oldestYear) { //если год рождения меньше
            oldestYear = student.birthYear; //то присваиваем переменной год рождения
            oldestName = student.surname + " " + student.firstName + " " + student.patronymic; //иначе присваиваем переменной фамилию, имя и отчество
        }
        if (student.birthYear > youngestYear) { //если год рождения больше
            youngestYear = student.birthYear; //то присваиваем переменной год рождения
            youngestName = student.surname + " " + student.firstName + " " + student.patronymic; //иначе присваиваем переменной фамилию, имя и отчество
        }
    }
    std::cout << "Oldest student: " << oldestName << " (" << oldestYear << ")" << std::endl;
    std::cout << "Youngest student: " << youngestName << " (" << youngestYear << ")" << std::endl;
}

//найти лучшего студента и вывести его в консоль
void findBestStudent(const std::vector<Student>& students) { //находим лучшего студента
    int bestStudentIndex = -1; //инициализируем переменные
    int bestStudentGrade = std::numeric_limits<int>::min();     //инициализируем переменные
    for (size_t i = 0; i < students.size(); i++) { //для каждого студента
        int grade = students[i].grades[0] + students[i].grades[1] + students[i].grades[2] + students[i].grades[3] + students[i].grades[4];  //считаем сумму оценок
        if (grade > bestStudentGrade) { //если сумма оценок больше
            bestStudentGrade = grade; //то присваиваем переменной сумму оценок
            bestStudentIndex = i; //иначе присваиваем переменной индекс
        }
    }
    const Student& bestStudent = students[bestStudentIndex]; //присваиваем переменной студента
    std::cout << "Best student: " << bestStudent.surname << " " << bestStudent.firstName << " " << bestStudent.patronymic << std::endl;
}
void printStudents(const std::vector<Student>& students) { //выводим студентов в консоль
    for (size_t i = 0; i < students.size(); i++) { //для каждого студента
        const Student& student = students[i]; //присваиваем переменной студента
        std::cout << student.surname << " " << student.firstName << " " << student.patronymic << " "
            << student.birthYear << " " << student.course << " " << student.groupNumber << " "; //выводим в консоль
        for (size_t j = 0; j < student.grades.size(); j++) { //для каждой оценки
            std::cout << student.grades[j] << " "; //выводим в консоль
        }
        std::cout << std::endl;
    }
    std::cout << std::endl << std::endl;
}

int main() {
    std::string surname;
    std::string firstName;
    std::string patronymic;
    int birthYear;
    int course;
    int groupNumber;
    std::vector<int> grades(5);
    std::vector<Student> students = {
            {"Bankuzov", "Mikhail", "Olegovich", 2005, 1, 7, {5, 4, 3, 4, 5}},
            {"Plesko", "Max", "Petrovich", 2005, 2, 8, {4, 5, 4, 5, 4}},
            {"Dmuhovskiy", "Denis", "Viktorovich", 2005, 1, 9, {3, 4, 5, 4, 3}},
    }; //вектор студентов

    //функция для вывода всех значений вектора Students
    printStudents(students);
    sortByCourseAndName(students);
    printStudents(students);
    findOldestAndYoungest(students);
    findBestStudent(students);
    findAverageGrade(students);
    return 0;
}
