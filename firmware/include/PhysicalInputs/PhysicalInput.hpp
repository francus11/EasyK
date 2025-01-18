#pragma once
#include <Arduino.h>
#include "Actions/Action.hpp"
// interface for physical inputs, like buttons, encoders etc.
class PhysicalInput
{
    protected:
    int id = 0;
    public:
    virtual int invoke();
    virtual std::string serialize();
};