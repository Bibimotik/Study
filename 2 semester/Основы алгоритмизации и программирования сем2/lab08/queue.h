#ifndef QUEUE_H
#define QUEUE_H

class Queue {
public:
	Queue(int size); // конструктор
	~Queue(); // деструктор
	bool isFull() const; // проверяет, заполнена ли очередь
	bool isEmpty() const; // проверяет, пуста ли очередь
	void enqueue(char c); // добавляет элемент в очередь
	char dequeue(); // удаляет элемент из очереди
	void printQueue() const; // выводит содержимое очереди в консоль
	int front; // индекс первого элемента в очереди
	int rear; // индекс последнего элемента в очереди

private:
	char* buffer; // массив символов
	int capacity; // максимальный размер очереди
};

#endif
