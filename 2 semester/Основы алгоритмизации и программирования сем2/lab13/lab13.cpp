// Подключение необходимых заголовочных файлов
#include <iostream>
#include <queue>
#include <vector>

using namespace std;

// Определение класса MedianHeap, который использует две бинарные кучи (maxHeap и minHeap) для хранения меньших и больших чисел соответственно
class MedianHeap {
private:
	priority_queue<int> maxHeap; // Максимальная куча для меньших чисел
	priority_queue<int, vector<int>, greater<int>> minHeap; // Минимальная куча для больших чисел

public:
	// Добавление числа в кучу
	void addNumber(int num) {
		if (maxHeap.empty() || num <= maxHeap.top()) {
			maxHeap.push(num);
		}
		else {
			minHeap.push(num);
		}
        // Балансировка куч
        if (maxHeap.size() > minHeap.size() + 1) {
            minHeap.push(maxHeap.top());
            maxHeap.pop();
        }
        else if (minHeap.size() > maxHeap.size() + 1) {
            maxHeap.push(minHeap.top());
            minHeap.pop();
        }
    }

    // Нахождение медианы
    double findMedian() {
        if (maxHeap.size() == minHeap.size()) {
            return (maxHeap.top() + minHeap.top()) / 2.0;
        }
        else if (maxHeap.size() > minHeap.size()) {
            return maxHeap.top();
        }
        else {
            return minHeap.top();
        }
    }

    // Нахождение среднего арифметического
    double findMean() {
        double sum = 0.0;
        int count = 0;

        priority_queue<int> maxCopy = maxHeap;
        priority_queue<int, vector<int>, greater<int>> minCopy = minHeap;

        // Вычисление суммы всех чисел в кучах и их количества
        while (!maxCopy.empty()) {
            sum += maxCopy.top();
            maxCopy.pop();
            count++;
        }

        while (!minCopy.empty()) {
            sum += minCopy.top();
            minCopy.pop();
            count++;
        }

        // Вычисление среднего арифметического
        return sum / count;
    }
};

// Основная функция программы
int main() {
    // Создание экземпляра класса MedianHeap
    MedianHeap medianHeap;
    // Добавление чисел в кучу
    medianHeap.addNumber(3);
    medianHeap.addNumber(2);
    medianHeap.addNumber(1);
    medianHeap.addNumber(5);
    medianHeap.addNumber(6);
    medianHeap.addNumber(4);

    // Вычисление медианы и среднего арифметического
    double median = medianHeap.findMedian();
    double mean = medianHeap.findMean();

    // Вывод результатов на экран
    cout << "Median: " << median << endl;
    cout << "Mean: " << mean << endl;

    // Возвращение нулевого значения, чтобы сообщить ОС об успешном завершении программы
    return 0;
}