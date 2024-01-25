#include <iostream>
using namespace std;

void bubble_sort(int arr[], int n) { // сортировка пузырьком
    for (int i = 0; i < n - 1; i++) {
        for (int j = 0; j < n - i - 1; j++) {
            if (arr[j] > arr[j + 1]) {
                swap(arr[j], arr[j + 1]);
            }
        }
    }
}

void remove_duplicates(int arr[], int n) {
    int k = 0;
    for (int i = 0; i < n - 1; i++) {
        if (arr[i] != arr[i + 1]) {
            arr[k++] = arr[i];
        }
    }
    arr[k++] = arr[n - 1]; // последний элемент всегда уникальный, поэтому его нужно добавить в массив
    for (int i = k; i < n; i++) {
        arr[i] = 0; // заполняем нулями оставшуюся часть массива
    }

}

int main() {
    int arr[15] = { 5, -3, 4, -2, 0, 1, 3, -1, -5, -4, 2, 0, -2, 1, 5 };
    bubble_sort(arr, 15); // сортировка массива
    remove_duplicates(arr, 15); // удаление повторяющихся элементов
    for (int i = 0; i < 15; i++) {
        cout << arr[i] << " ";
    }
    cout << endl;
    return 0;
}
