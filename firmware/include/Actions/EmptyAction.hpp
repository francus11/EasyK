#pragma once

#include "Action.hpp"

class EmptyAction : public Action
{
    public:
    int invoke();
    static EmptyAction* deserialize(std::string json);
};