#pragma once

#include "Action.hpp"

class EmptyAction : Action
{
    public:
    int invoke();
    static EmptyAction* deserialize(std::string json);
};