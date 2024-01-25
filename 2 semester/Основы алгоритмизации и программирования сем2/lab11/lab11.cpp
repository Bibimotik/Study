#include <iostream>
#include <conio.h>
using namespace std;

struct  Node
{
    int x;
    Node* Next;
};

class List
{
public:
    Node* Head, * Tail;
    int size;

    List() : Head(NULL), Tail(NULL), size(0) {};
    ~List();
    void Add(int x);
    void Show(int size);
    int Count();
    void Clear();
};

List::~List()
{
    while (size != 0)
    {
        Node* temp = Head->Next;
        delete Head;
        Head = temp;
        size--;
    }
}

int List::Count()
{
    return size;
}

void List::Add(int x)
{
    size++;
    Node* temp = new Node;
    temp->Next = Head;
    temp->x = x;

    if (Head != NULL)
    {
        Tail->Next = temp;
        Tail = temp;
    }
    else Head = Tail = temp;
}

void List::Show(int temp)
{
    if (size != 0)
    {
        Node* tempHead = Head;

        temp = size;
        while (temp != 0)
        {
            cout << tempHead->x << " ";
            tempHead = tempHead->Next;
            temp--;
        }
    }
    else cout << "В списке нет элементов: " << size;
}

void List::Clear()
{
    while (size)
    {
        Node* temp = Head->Next;
        delete Head;
        Head = temp;
        size--;
    }
}

void main()
{
    setlocale(LC_ALL, "Russian");
    int n;
    List lst;
    cout << "Введите кол-во элементов в списке" << endl;
    cin >> n;
    for (int i = 0; i < n; i++)
    {
        lst.Add(i);
    }
    cout << "Список:" << endl;
    lst.Show(lst.Count());
    cout << endl;
    cout << endl << lst.Count() << endl;
    lst.Clear();
    lst.Show(lst.Count());
}
