#pragma once

#include "Action.hpp"
#include "Keyboard.hpp"

class KeyboardAction : public Action
{
private:
    using ActionMethod = int (KeyboardAction::*)();
    ActionMethod actionMethod;
    KeyboardKeycode key;

    int pressKey();
    int releaseKey();
    int clickKey();
    
public:

    KeyboardAction(KeyboardKeycode key, std::string type);
    int invoke();

    static KeyboardAction* deserialize(std::string json);
    std::string serialize();
};