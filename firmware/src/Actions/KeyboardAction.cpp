#include "Actions/KeyboardAction.hpp"

KeyboardAction::KeyboardAction(KeyboardKeycode key, String type)
{
    this->key = key;
    if (type == "press")
    {
        actionMethod = &KeyboardAction::pressKey;
    }
    else if (type == "release")
    {
        actionMethod = &KeyboardAction::releaseKey;
    }
    else if (type == "click")
    {
        actionMethod = &KeyboardAction::clickKey;
    }
    else
    {
    }
}

int KeyboardAction::invoke()
{
    if (actionMethod)
    {
        return (this->*actionMethod)();
    }
    else
    {
        return -1;
    }
}

int KeyboardAction::pressKey()
{
    return Keyboard.press(key);
}

int KeyboardAction::releaseKey()
{
    return Keyboard.release(key);
}

int KeyboardAction::clickKey()
{
    return Keyboard.click(key);
}