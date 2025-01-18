#include "Actions/Action.hpp"
#include "Actions/KeyboardAction.hpp"
#include "Actions/EmptyAction.hpp"
#include "Actions/MacroAction.hpp"

Action* Action::deserialize(std::string json)
{
    JsonDocument doc;
    deserializeJson(doc, json);
    if (doc["type"] == "KeyboardAction")
    {
        return KeyboardAction::deserialize(doc["details"]);
    }
    else if (doc["type"] == "MacroAction")
    {
        return MacroAction::deserialize(json);
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