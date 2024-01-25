#ifndef QUEUE_H
#define QUEUE_H

class Queue {
public:
	Queue(int size); // �����������
	~Queue(); // ����������
	bool isFull() const; // ���������, ��������� �� �������
	bool isEmpty() const; // ���������, ����� �� �������
	void enqueue(char c); // ��������� ������� � �������
	char dequeue(); // ������� ������� �� �������
	void printQueue() const; // ������� ���������� ������� � �������
	int front; // ������ ������� �������� � �������
	int rear; // ������ ���������� �������� � �������

private:
	char* buffer; // ������ ��������
	int capacity; // ������������ ������ �������
};

#endif
