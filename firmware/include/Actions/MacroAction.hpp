#pragma once

#include "Action.hpp"
#include <vector>
#include <memory>

class MacroAction: public Action
{
    private:
    std::vector<Action*> actions;

    public:
    MacroAction();
    ~MacroAction();
    void addAction(Action* action);
    int invoke();

    static MacroAction* deserialize(std::string json);
    std::string serialize();
};