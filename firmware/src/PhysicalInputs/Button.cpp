#include "PhysicalInputs/Button.hpp"

Button::Button(int pinA, int pinB, Action action)
{
    this->pinA = pinA;
    this->pinB = pinB;
    this->action = action;
}

int Button::invoke()
{
    pinMode(pinA, INPUT_PULLUP);
    pinMode(pinB, OUTPUT);
    digitalWrite(pinB, LOW);

    if (!digitalRead(pinA))
    {
        return action.invoke();
    }
    
    return 0;
}