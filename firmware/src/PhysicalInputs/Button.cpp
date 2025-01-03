#include "PhysicalInputs/Button.hpp"

Button::Button(int pinA, int pinB, int type, Action* action)
{
    this->pinA = pinA;
    this->pinB = pinB;
    this->action = action;
    this->type = type;
    if (type == INPUT_PULLUP)
    {
        this->pressedState = LOW;
    }
    else
    {
        this->pressedState = HIGH;
    }
}

int Button::invoke()
{
    pinMode(pinA, type);
    pinMode(pinB, OUTPUT);
    digitalWrite(pinB, pressedState);
    int result = 0;
    if (digitalRead(pinA) == pressedState)
    {
        
        result =  action->invoke();
    }
    pinMode(pinA, OUTPUT);
    digitalWrite(pinA, !pressedState);
    digitalWrite(pinB, !pressedState);
    
    return result;
}

int Button::getPressedState()
{
    return pressedState;
}