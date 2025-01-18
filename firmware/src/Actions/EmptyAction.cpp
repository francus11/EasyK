#include "Actions/EmptyAction.hpp"

int EmptyAction::invoke()
{
    return 1;
}

EmptyAction* EmptyAction::deserialize(std::string json)
{
    return new EmptyAction();
}

std::string EmptyAction::serialize()
{
    return "{}";
}