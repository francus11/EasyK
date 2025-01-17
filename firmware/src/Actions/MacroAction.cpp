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