#pragma

#include "Action.hpp"

class DelayAction : public Action
{
    private:
    unsigned int start;
    bool isStarted = false;
    int duration;
    public:
    DelayAction(int duration);
    int invoke() override;
    static DelayAction* deserialize(std::string json);
    std::string serialize() override;
};