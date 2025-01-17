#pragma once

#include "Action.hpp"
#include <vector>
#include <memory>

class MacroAction: Action
{
    private:
    std::vector<Action*> actions;

    public:
    MacroAction();
    ~MacroAction();
    void addAction(Action* action);
    int invoke();

    static MacroAction* deserialize(std::string json);
};