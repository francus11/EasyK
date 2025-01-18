#pragma once

#include <string>
#include "PhysicalInputs/Button.hpp"
#include "PhysicalInputs/Encoder.hpp"
#include "PhysicalInputs/ButtonSecured.hpp"
#include <ArduinoJson.h>

#define BUTTONS_COUNT 16
#define ENCODERS_COUNT 1

#define PIN_ENC_A 8
#define PIN_ENC_B 6
#define PIN_ENC_BUTTON 7

// static const int buttonPins[16][7] = 
// {
//     {10, 3, INPUT_PULLDOWN, 0, 1, 2, 9},
//     {3, 10, INPUT_PULLDOWN, 0, 1, 2, 9},
//     {3, 9, INPUT_PULLUP, 0, 1, 2, 10},
//     {9, 3, INPUT_PULLUP, 0, 1, 2, 10},
//     {10, 2, INPUT_PULLDOWN, 0, 1, 3, 9},
//     {2, 10, INPUT_PULLDOWN, 0, 1, 3, 9},
//     {2, 9, INPUT_PULLUP, 0, 1, 3, 10},
//     {9, 2, INPUT_PULLUP, 0, 1, 3, 10},
//     {10, 1, INPUT_PULLDOWN, 0, 2, 3, 9},
//     {1, 10, INPUT_PULLDOWN, 0, 2, 3, 9},
//     {1, 9, INPUT_PULLUP, 0, 2, 3, 10},
//     {9, 1, INPUT_PULLUP, 0, 2, 3, 10},
//     {10, 0, INPUT_PULLDOWN, 1, 2, 3, 9},
//     {0, 10, INPUT_PULLDOWN, 1, 2, 3, 9},
//     {0, 9, INPUT_PULLUP, 1, 2, 3, 10},
//     {9, 0, INPUT_PULLUP, 1, 2, 3, 10}
    
// };

static const int buttonPins[16][7] = 
{
    {3, 10, INPUT_PULLUP, 0, 1, 2, 9},
    {10, 3, INPUT_PULLUP, 0, 1, 2, 9},
    {3, 9, INPUT_PULLUP, 0, 1, 2, 10},
    {9, 3, INPUT_PULLUP, 0, 1, 2, 10},
    {2, 10, INPUT_PULLUP, 0, 1, 3, 9},
    {10, 2, INPUT_PULLUP, 0, 1, 3, 9},
    {2, 9, INPUT_PULLUP, 0, 1, 3, 10},
    {9, 2, INPUT_PULLUP, 0, 1, 3, 10},
    {1, 10, INPUT_PULLUP, 0, 2, 3, 9},
    {10, 1, INPUT_PULLUP, 0, 2, 3, 9},
    {1, 9, INPUT_PULLUP, 0, 2, 3, 10},
    {9, 1, INPUT_PULLUP, 0, 2, 3, 10},
    {0, 10, INPUT_PULLUP, 1, 2, 3, 9},
    {10, 0, INPUT_PULLUP, 1, 2, 3, 9},
    {0, 9, INPUT_PULLUP, 1, 2, 3, 10},
    {9, 0, INPUT_PULLUP, 1, 2, 3, 10}
    
};
class MacroPadRunner
{
    private:
        PhysicalInput* buttons[BUTTONS_COUNT] = {nullptr};
        Encoder* encoder;
    public:
    MacroPadRunner(PhysicalInput* buttons[16], Encoder* encoder);
    ~MacroPadRunner();
    static MacroPadRunner* deserialize(std::string json);
    void run();
    std::string serialize();
};