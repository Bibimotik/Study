// Подключение заголовочного файла "Tree.h".
#include "Tree.h"

// Подключение заголовочного файла <iostream>.
#include <iostream>

// Функция Хеш-функции, которая принимает ключ, размер таблицы и параметр p.
int HashFunction(int key, int size, int p)
{
	double key2 = 5 * ((0.6180339887499 * key) - int((0.6180339887499 * key)));
	return (p + key) % size;
}

// Функция Next_hash, которая принимает хеш-код, размер таблицы и параметр p.
int Next_hash(int hash, int size, int p)
{
	// Вариант 1 функции Next_hash.
	return (hash + 5 * p + 3 * p * p) % size;
}
// Универсальная хеш-функция
int universal(int key, int size, int p)
{
	// Инициализируем переменные h, a, b
	int h = 0, a = 314445, b = 37542;

	// Вычисляем значение h с помощью формулы (a * h + key) % size
	h = (a * h + key) % size;

	// Если значение h отрицательное, то прибавляем к нему значение size
	return (h < 0) ? (h + size) : h;
}

// Функция create для создания нового объекта хеш-таблицы с заданным размером и указателем на функцию getKey.
Object create(int size, int(*getkey)(void*))
{
	return *(new Object(size, getkey));
}

// Конструктор для объекта хеш-таблицы, который принимает размер и указатель на функцию getKey.
Object::Object(int size, int(*getkey)(void*))
{
	// Инициализация количества элементов и размера таблицы.
	N = 0;
	this->size = size;
	// Указатель на функцию getKey.
	this->getKey = getkey;

	// Выделение памяти для массива данных и инициализация его значениями NULL.
	this->data = new void* [size];
	for (int i = 0; i < size; ++i)
		data[i] = NULL;
}
// Функция insert для вставки элемента в хеш-таблицу.
bool Object::insert(void* d)
{
	bool b = false;

	// Если количество элементов N не равно размеру таблицы size, то добавляем новый элемент в таблицу
	if (N != size)
		for (int i = 0, t = getKey(d), j = universal(t, size, 0); i != size && !b; j = Next_hash(j, size, ++i))
			if (data[j] == NULL || data[j] == DEL)
			{
				data[j] = d;
				N++;
				b = true;
			}

	// Возвращаем значение b
	return b;
}
// Функция searchInd для поиска индекса элемента в хеш-таблице по ключу.
int Object::searchInd(int key)
{
	int t = -1;
	bool b = false;

	// Если количество элементов N не равно нулю, то ищем элемент с заданным ключом в таблице
	if (N != 0)
		for (int i = 0, j = universal(key, size, 0); data[j] != NULL && i != size && !b; j = universal(key, size, ++i))
			if (data[j] != DEL)
				if (getKey(data[j]) == key)
				{
					t = j; b = true;
				}

	// Возвращаем индекс найденного элемента или -1, если элемент не найден
	return t;
}
// Функция search для поиска элемента в хеш-таблице по ключу.
void* Object::search(int key)
{
	// Поиск индекса элемента в таблице.
	int t = searchInd(key);
	// Возврат элемента, если он найден, и NULL в противном случае.
	return(t >= 0) ? (data[t]) : (NULL);
}
// Функция deleteByKey для удаления элемента из хеш-таблицы по ключу.
void* Object::deleteByKey(int key)
{
	// Поиск индекса элемента в таблице по ключу.
	int i = searchInd(key);
	// Сохранение ссылки на элемент в переменную t.
	void* t = data[i];

	// Если элемент найден, помечаем его как удаленный и уменьшаем количество элементов в таблице.
	if (t != NULL)
	{
		data[i] = DEL;
		N--;
	}

	// Возврат ссылки на удаленный элемент.
	return t;
}
// Функция deleteByValue для удаления элемента из хеш-таблицы по значению.
bool Object::deleteByValue(void* d)
{
	// Удаление элемента из таблицы по ключу, полученному с помощью функции getKey.
	return(deleteByKey(getKey(d)) != NULL);
}

// Функция scan для применения заданной функции к каждому элементу в хеш-таблице.
void Object::scan(void(*f)(void*))
{
	// Обход всех элементов в таблице.
	for (int i = 0; i < this->size; i++)
	{
		// Вывод информации о текущем элементе.
		std::cout << " Элемент" << i;
		if ((this->data)[i] == NULL)
			std::cout << " пусто" << std::endl;
		else
			if ((this->data)[i] == DEL)
				std::cout << " удален" << std::endl;
			else
				f((this->data)[i]);
	}
}
