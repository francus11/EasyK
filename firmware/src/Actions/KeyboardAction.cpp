#include "Actions/KeyboardAction.hpp"

KeyboardAction::KeyboardAction(KeyboardKeycode key, std::string type)
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

KeyboardAction* KeyboardAction::deserialize(std::string json)
{
    nlohmann::json j = nlohmann::json::parse(json.c_str());
    return new KeyboardAction(static_cast<KeyboardKeycode>(static_cast<uint8_t>(j["key"])), j["type"].get<std::string>().c_str());
}