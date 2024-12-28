#pragma once

#include "PhysicalInput.hpp"

class Button: PhysicalInput
{
private:
    int pinA; // input_pullup
    int pinB; // output low

    Action action;
public:
    Button(int pinA, int pinB, Action action);
    int invoke();
};
