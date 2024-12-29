#pragma once
#include <Arduino.h>
#include <nlohmann/json.hpp>
// interface for actions in responce for physical input

class Action
{
    public:
    virtual int invoke();
    static Action* deserialize(String json);
};