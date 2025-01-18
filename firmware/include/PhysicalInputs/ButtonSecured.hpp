#pragma once

#include "PhysicalInput.hpp"
#include "Button.hpp"

/* Wrapper for Button class that also turns off pins that can interrupt reading proper value of press */
class ButtonSecured : public PhysicalInput
{
    private:
        Button* button;
        int* turnedOffPins;
        int turnedOffPinsSize;

    public:
        int invoke() override;
        ButtonSecured(int pinA, int pinB, int type, Action* action, int* turnedOffPins, int turnedOffPinsSize);
        std::string serialize();
};