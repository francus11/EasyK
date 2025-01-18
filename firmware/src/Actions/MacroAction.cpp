#include "Actions/MacroAction.hpp"

MacroAction::MacroAction()
{
}

MacroAction::~MacroAction()
{
    for (Action* action : actions)
    {
        delete action;
    }
}

void MacroAction::addAction(Action* action)
{
    actions.push_back(action);
}

int MacroAction::invoke()
{
    for (Action* action : actions)
    {
        while (action->invoke() != 1)
        {
            // Keep invoking until it returns 1
        }
    }
    return 1;
}

MacroAction* MacroAction::deserialize(std::string json)
{
    JsonDocument doc;
    deserializeJson(doc, json.c_str());
    MacroAction* macroAction = new MacroAction();

    for (int i = 0; i < doc["actions"].size(); i++)
    {
        Action* action = Action::deserialize(doc["actions"][i]);
        macroAction->addAction(action);
    }

    return macroAction;
}

std::string MacroAction::serialize()
{
    JsonDocument doc;
    doc["type"] = "MacroAction";
    JsonArray actionsArray = doc.createNestedArray("actions");

    for (Action* action : actions)
    {
        JsonDocument actionDoc;
        deserializeJson(actionDoc, action->serialize());
        actionsArray.add(actionDoc.as<JsonObject>());
    }

    std::string output;
    serializeJson(doc, output);
    return output;
}