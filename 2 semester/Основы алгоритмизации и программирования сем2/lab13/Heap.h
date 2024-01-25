#pragma once // Директива препроцессора, которая указывает компилятору на то, чтобы включить этот файл только один раз.

struct AAA // Объявляем структуру AAA.
{
	int x;
	void print(); // Объявляем метод print().
	int getPriority() const; // Объявляем метод getPriority().
};

namespace heap // Объявляем пространство имен heap.
{
	enum CMP // Перечисление, которое определяет три возможных результата сравнения двух элементов кучи.
	{
		LESS = -1, // Меньше.
		EQUAL = 0, // Равно.
		GREAT = 1  // Больше.
	};

	struct Heap // Объявляем структуру Heap, которая представляет собой бинарную кучу.
	{
		int size; // Текущий размер кучи.
		int maxSize; // Максимальный размер кучи.
		void** storage; // Указатель на массив данных.
		CMP(*compare)(void*, void*); // Указатель на функцию сравнения двух элементов кучи.

		// Конструктор.
		Heap(int maxsize, CMP(*f)(void*, void*))
		{
			size = 0;
			storage = new void* [maxSize = maxsize]; // Выделяем память под массив данных.
			compare = f; // Инициализируем указатель на функцию сравнения.
		};

		// Методы для получения индексов дочерних и родительского узлов.
		int left(int ix);
		int right(int ix);
		int parent(int ix);

		// Методы для проверки полноты и пустоты кучи.
		bool isFull() const
		{
			return (size >= maxSize);
		};
		bool isEmpty() const
		{
			return (size <= 0);
		};

		// Методы для проверки отношения порядка между двумя элементами кучи.
		bool isLess(void* x1, void* x2) const
		{
			return compare(x1, x2) == LESS;
		};
		bool isGreat(void* x1, void* x2) const
		{
			return compare(x1, x2) == GREAT;
		};
		bool isEqual(void* x1, void* x2) const
		{
			return compare(x1, x2) == EQUAL;
		};

		// Метод для обмена элементов двух узлов.
		void swap(int i, int j);

		// Метод для приведения поддерева с корнем в узле ix к виду кучи.
		void heapify(int ix);

		// Метод для вставки нового элемента в кучу.
		void insert(void* x);

		// Методы для извлечения максимального и минимального элементов из кучи.
		void* extractMax();
		void* extractMin();

		// Метод для извлечения элемента с заданным индексом из кучи.
		void* extractI(int index);

		// Метод для объединения двух куч.
		Heap* unionHeap(Heap* heap);

		// Метод для вывода на экран содержимого кучи.
		void scan(int i) const;
	};

	// Функция для создания новой кучи.
	Heap create(int maxsize, CMP(*f)(void*, void*));
};