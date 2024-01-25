#include <iostream>
#include <ctime>

using std::endl; using std::cout; using std::cin;

bool first = true;

struct Node
{
	int data;
	std::string color;
	Node* left, * right;
	Node* parent;

	Node() :left(nullptr), right(nullptr), parent(nullptr) {}
};

bool HeightBalanced(Node* root);
int isHeightBalanced(Node* root, bool& isBalanced);
void inOrder(Node*);
void preOrder(Node*);

void insert(int a, Node** t)
{
	if ((*t) == NULL)
	{
		(*t) = new Node;
		(*t)->data = a;
		(*t)->left = (*t)->right = NULL;
		(*t)->color = "black";
		return;
	}

	if (((*t)->left != nullptr || (*t)->right != nullptr) && !first)
	{
		(*t)->color = "red";
	}
	else { (*t)->color = "black"; first = false; }

	if (a > (*t)->data)
	{
		insert(a, &(*t)->left);
	}
	else insert(a, &(*t)->right);
}

void print(Node* t, int u)
{
	if (t == NULL)  return;
	else
	{
		print(t->left, ++u);
		for (int i = 0; i < u; ++i)
			cout << '\t';
		for (int i = 0; i < u; ++i)
			cout << '|';
		cout << t->color << " " << t->data << endl;
		u--;
	}
	print(t->right, ++u);
}

int isHeightBalanced(Node* root, bool& isBalanced)
{
	if (root == nullptr || !isBalanced)
	{
		return 0;
	}

	int left_height = isHeightBalanced(root->left, isBalanced);

	int right_height = isHeightBalanced(root->right, isBalanced);

	if (abs(left_height - right_height) > 1)
	{
		isBalanced = false;
	}

	return std::max(left_height, right_height) + 1;
}

bool HeightBalanced(Node* root)
{
	bool isBalanced = true;
	isHeightBalanced(root, isBalanced);

	return isBalanced;
}

int main()
{
	setlocale(LC_CTYPE, "Russian");
	srand((unsigned int)time(NULL));

	Node* tree = nullptr;
	int count, temp;
	cout << "Введите количество элементов  "; cin >> count;
	for (int i = 0; i < count; ++i)
	{
		temp = rand() % 10 + 1;
		cout << temp << '\t';
		insert(temp, &tree);
	}
	cout << endl;
	cout << "ваше дерево\n";
	print(tree, 0);
	cout << endl;
	cout << HeightBalanced(tree);
}