#include "Actions/KeyboardAction.hpp"

KeyboardAction::KeyboardAction(KeyboardKeycode key, KeyState state)
{
    this->key = key;
    this->state = state;
    if (state == KeyState::Press)
    {
        actionMethod = &KeyboardAction::pressKey;
    }
    else if (state == KeyState::Release)
    {
        actionMethod = &KeyboardAction::releaseKey;
    }
    else if (state == KeyState::Click)
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
    JsonDocument doc;
    deserializeJson(doc, json.c_str());
    return new KeyboardAction(static_cast<KeyboardKeycode>(static_cast<uint8_t>(doc["key"])), static_cast<KeyState>(static_cast<uint8_t>(doc["state"])));
}

std::string KeyboardAction::serialize()
{
    JsonDocument doc;
    doc["type"] = "KeyboardAction";
    JsonObject details = doc.createNestedObject("details");
    details["key"] = static_cast<uint8_t>(key);
    details["state"] = static_cast<uint8_t>(state);
    std::string output;
    serializeJson(doc, output);
    return output;
}