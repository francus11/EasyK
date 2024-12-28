#pragma once

#include "Action.hpp"
#include "Keyboard.hpp"

class KeyboardAction : public Action
{
private:
    KeyboardKeycode key;
public:
    KeyboardAction(KeyboardKeycode key);
    int invoke();
};