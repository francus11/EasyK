#include "Actions/KeyboardAction.hpp"

KeyboardAction::KeyboardAction(KeyboardKeycode key)
{
    this->key = key;
}

int KeyboardAction::invoke()
{
    return Keyboard.press(key);
}