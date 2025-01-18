#pragma once

#include "Action.hpp"
#include "Keyboard.hpp"
#include "KeyState.hpp"

class KeyboardAction : public Action
{
private:
    using ActionMethod = int (KeyboardAction::*)();
    KeyState state;
    ActionMethod actionMethod;
    KeyboardKeycode key;

    int pressKey();
    int releaseKey();
    int clickKey();
    
public:

    KeyboardAction(KeyboardKeycode key, KeyState state);
    int invoke();

    static KeyboardAction* deserialize(std::string json);
    std::string serialize();
};