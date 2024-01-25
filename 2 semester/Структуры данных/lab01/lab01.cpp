#include <iostream>

enum PCState {
    ON,
    OFF,
    SLEEP,
    WRONG
};

int main() {
    int state;
    int choice;
    std::cout << "Enter the state of your PC:" << std::endl;
    std::cout << "1. ON" << std::endl;
    std::cout << "2. OFF" << std::endl;
    std::cout << "3. SLEEP" << std::endl;
    std::cin >> choice;

    switch (choice) {
    case 1:
        state = ON;
        break;
    case 2:
        state = OFF;
        break;
    case 3:
        state = SLEEP;
        break;
    default:
        state = WRONG;
    }

    switch (state) {
    case ON:
        std::cout << "Your PC is ON" << std::endl;
        break;
    case OFF:
        std::cout << "Your PC is OFF" << std::endl;
        break;
    case SLEEP:
        std::cout << "Your PC is in sleep mode" << std::endl;
        break;
    case WRONG:
        std::cout << "You enter wrong PC state" << std::endl;
    }

    return 0;
}