#include  "PhysicalInputs/ButtonSecured.hpp"

int ButtonSecured::invoke()
{
    for (int i = 0; i < turnedOffPinsSize; i++)
    {
        pinMode(turnedOffPins[i], OUTPUT);
        digitalWrite(turnedOffPins[i], !button->getPressedState());
    }

    return this->button->invoke();
}

ButtonSecured::ButtonSecured(int pinA, int pinB, int type, Action* action, int* turnedOffPins, int turnedOffPinsSize)
{
    button = new Button(pinA, pinB, type, action);
    this->turnedOffPins = turnedOffPins;
    this->turnedOffPinsSize = turnedOffPinsSize;
}

