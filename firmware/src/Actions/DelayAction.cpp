#include "Actions/DelayAction.hpp"

DelayAction::DelayAction(int duration)
{
    this->duration = duration;
}

int DelayAction::invoke()
{
    if (!isStarted)
    {
        start = millis();
        isStarted = true;
    }
    if (millis() - start < duration)
    {
        return 0;
    }
    isStarted = false;
    return 1;
}

DelayAction* DelayAction::deserialize(std::string json)
{
    JsonDocument doc;
    deserializeJson(doc, json.c_str());
    return new DelayAction((int)(doc["duration"]));
}

std::string DelayAction::serialize()
{
    JsonDocument doc;
    doc["type"] = "DelayAction";
    JsonObject details = doc.createNestedObject("details");
    details["duration"] = duration;
    std::string output;
    serializeJson(doc, output);
    return output;
}