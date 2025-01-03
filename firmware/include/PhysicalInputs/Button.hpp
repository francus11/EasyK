#pragma once

#include "PhysicalInput.hpp"

class Button: PhysicalInput
{
private:
    int pinA; // input
    int pinB; // output
    int type;
    bool pressedState;

    Action* action;
public:
    Button(int pinA, int pinB, int type, Action* action);
    int invoke();
    int getPressedState();
};
