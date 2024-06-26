#include <iostream>
#include <ctime>
#include <string>

using namespace std;
//task 1
void start()
{
	srand(time(NULL));
}

double dget(double rmin, double rmax)
{
	return ((double) rand() / (double) RAND_MAX) * (rmax - rmin) + rmin;
}

int iget(int rmin, int rmax)
{
	return (int)dget((double)rmin, (double)rmax); 
}
//task 2
#define CYCLE 1000000
string multiplyStrings(string num1, string num2)
{
    int len1 = num1.length();
    int len2 = num2.length();
    string result(len1 + len2, '0');

    for (int i = len1 - 1; i >= 0; --i)
    {
        int carry = 0;
        for (int j = len2 - 1; j >= 0; --j)
        {
            int digit1 = num1[i] - '0';
            int digit2 = num2[j] - '0';

            int product = digit1 * digit2 + carry + (result[i + j + 1] - '0');

            carry = product / 10;
            result[i + j + 1] = '0' + (product % 10);
        }

        result[i] += carry;
    }

    size_t startpos = result.find_first_not_of('0');
    if (startpos != string::npos)
    {
	    return result.substr(startpos);
    }
    return "0";
}

string strFactorial(const string& n)
{
	if (n == "0" || n == "1")
	{
		return "1";
	}

	string prevFactorial = strFactorial(to_string(stoll(n) - 1));
	return multiplyStrings(n, prevFactorial);
}

int main(int argc, char** argv)
{	
	int n = 10;
	constexpr int n2 = 5;
	double av1 = 0, av2 = 0;
	clock_t t1 = 0, t2 = 0;

	start();

	t1 = clock();
	for(int i = 0; i < CYCLE; ++i)
	{
		av1 += (double)iget(-100, 100);
		av2 += dget(-100, 100);
	}
	t2 = clock();

	cout << endl << "number of cycles:         " << CYCLE; //количество циклов
	cout << endl << "the average value (int):    " << av1/CYCLE; //среднее значение int
	cout << endl << "the average value (double): " << av2/CYCLE; //среднее значение double
	cout << endl << "duration (y.e):   " << (t2-t1);
	cout << endl << "                  (sek):   " << ((double)(t2-t1))/((double)CLOCKS_PER_SEC); //сек
  	cout << endl << endl; 

	t1 = clock();
	cout << strFactorial("100") << endl;
	t2 = clock();

	cout << endl << "                  (sek):   " << ((double)(t2-t1))/((double)CLOCKS_PER_SEC); //сек
	cout << endl << "duration (y.e):   " << (t2-t1);
  	cout << endl << endl; 

	return 0;
}