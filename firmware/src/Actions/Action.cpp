#include "Actions/Action.hpp"
#include "Actions/KeyboardAction.hpp"
#include "Actions/EmptyAction.hpp"

Action* Action::deserialize(std::string json)
{
    JsonDocument doc;
    deserializeJson(doc, json);
    if (doc["type"] == "KeyboardAction")
    {
        return KeyboardAction::deserialize(doc["details"]);
    }
    else if (doc["type"] == "empty")
    {
        return EmptyAction::deserialize("");
    }
    else
    {
        return nullptr;
    }

}