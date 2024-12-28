#pragma once
#include <Arduino.h>
#include "Actions/Action.hpp"
// interface for physical inputs, like buttons, encoders etc.
class PhysicalInput
{
    public:
    virtual int invoke();
};