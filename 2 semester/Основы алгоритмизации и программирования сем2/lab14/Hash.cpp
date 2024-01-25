// ����������� ������������� ����� "Tree.h".
#include "Tree.h"

// ����������� ������������� ����� <iostream>.
#include <iostream>

// ������� ���-�������, ������� ��������� ����, ������ ������� � �������� p.
int HashFunction(int key, int size, int p)
{
	double key2 = 5 * ((0.6180339887499 * key) - int((0.6180339887499 * key)));
	return (p + key) % size;
}

// ������� Next_hash, ������� ��������� ���-���, ������ ������� � �������� p.
int Next_hash(int hash, int size, int p)
{
	// ������� 1 ������� Next_hash.
	return (hash + 5 * p + 3 * p * p) % size;
}
// ������������� ���-�������
int universal(int key, int size, int p)
{
	// �������������� ���������� h, a, b
	int h = 0, a = 314445, b = 37542;

	// ��������� �������� h � ������� ������� (a * h + key) % size
	h = (a * h + key) % size;

	// ���� �������� h �������������, �� ���������� � ���� �������� size
	return (h < 0) ? (h + size) : h;
}

// ������� create ��� �������� ������ ������� ���-������� � �������� �������� � ���������� �� ������� getKey.
Object create(int size, int(*getkey)(void*))
{
	return *(new Object(size, getkey));
}

// ����������� ��� ������� ���-�������, ������� ��������� ������ � ��������� �� ������� getKey.
Object::Object(int size, int(*getkey)(void*))
{
	// ������������� ���������� ��������� � ������� �������.
	N = 0;
	this->size = size;
	// ��������� �� ������� getKey.
	this->getKey = getkey;

	// ��������� ������ ��� ������� ������ � ������������� ��� ���������� NULL.
	this->data = new void* [size];
	for (int i = 0; i < size; ++i)
		data[i] = NULL;
}
// ������� insert ��� ������� �������� � ���-�������.
bool Object::insert(void* d)
{
	bool b = false;

	// ���� ���������� ��������� N �� ����� ������� ������� size, �� ��������� ����� ������� � �������
	if (N != size)
		for (int i = 0, t = getKey(d), j = universal(t, size, 0); i != size && !b; j = Next_hash(j, size, ++i))
			if (data[j] == NULL || data[j] == DEL)
			{
				data[j] = d;
				N++;
				b = true;
			}

	// ���������� �������� b
	return b;
}
// ������� searchInd ��� ������ ������� �������� � ���-������� �� �����.
int Object::searchInd(int key)
{
	int t = -1;
	bool b = false;

	// ���� ���������� ��������� N �� ����� ����, �� ���� ������� � �������� ������ � �������
	if (N != 0)
		for (int i = 0, j = universal(key, size, 0); data[j] != NULL && i != size && !b; j = universal(key, size, ++i))
			if (data[j] != DEL)
				if (getKey(data[j]) == key)
				{
					t = j; b = true;
				}

	// ���������� ������ ���������� �������� ��� -1, ���� ������� �� ������
	return t;
}
// ������� search ��� ������ �������� � ���-������� �� �����.
void* Object::search(int key)
{
	// ����� ������� �������� � �������.
	int t = searchInd(key);
	// ������� ��������, ���� �� ������, � NULL � ��������� ������.
	return(t >= 0) ? (data[t]) : (NULL);
}
// ������� deleteByKey ��� �������� �������� �� ���-������� �� �����.
void* Object::deleteByKey(int key)
{
	// ����� ������� �������� � ������� �� �����.
	int i = searchInd(key);
	// ���������� ������ �� ������� � ���������� t.
	void* t = data[i];

	// ���� ������� ������, �������� ��� ��� ��������� � ��������� ���������� ��������� � �������.
	if (t != NULL)
	{
		data[i] = DEL;
		N--;
	}

	// ������� ������ �� ��������� �������.
	return t;
}
// ������� deleteByValue ��� �������� �������� �� ���-������� �� ��������.
bool Object::deleteByValue(void* d)
{
	// �������� �������� �� ������� �� �����, ����������� � ������� ������� getKey.
	return(deleteByKey(getKey(d)) != NULL);
}

// ������� scan ��� ���������� �������� ������� � ������� �������� � ���-�������.
void Object::scan(void(*f)(void*))
{
	// ����� ���� ��������� � �������.
	for (int i = 0; i < this->size; i++)
	{
		// ����� ���������� � ������� ��������.
		std::cout << " �������" << i;
		if ((this->data)[i] == NULL)
			std::cout << " �����" << std::endl;
		else
			if ((this->data)[i] == DEL)
				std::cout << " ������" << std::endl;
			else
				f((this->data)[i]);
	}
}
