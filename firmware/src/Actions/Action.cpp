#include "Actions/Action.hpp"
#include "Actions/KeyboardAction.hpp"

Action* Action::deserialize(std::string json)
{
    nlohmann::json j = nlohmann::json::parse(json.c_str());
    if (j["type"] == "KeyboardAction")
    {
        return KeyboardAction::deserialize(j["key"].get<std::string>().c_str());
    }
    else
    {
        return nullptr;
    }

}