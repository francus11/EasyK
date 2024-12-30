#pragma once
#include <ArduinoJson.h>
#include <string>


// interface for actions in responce for physical input

class Action
{
    public:
    virtual int invoke();
    static Action* deserialize(std::string json);
};