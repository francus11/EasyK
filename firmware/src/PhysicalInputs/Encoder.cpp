#include "PhysicalInputs/Encoder.hpp"

Encoder* Encoder::instances[MAX_ENCODERS] = {nullptr};
int Encoder::instanceCount = 0;

Encoder::Encoder(int pinEncA, int pinEncB, int pinButton, Action* actionLeft, Action* actionRight, Action* actionButton)
{
    this->pinEncA = pinEncA;
    this->pinEncB = pinEncB;
    this->pinButton = pinButton;

    this->actionLeft = actionLeft;
    this->actionRight = actionRight;
    this->actionButton = actionButton;

    pinMode(this->pinEncA, INPUT_PULLUP);
    pinMode(this->pinEncB, INPUT_PULLUP);
    pinMode(this->pinButton, INPUT_PULLUP);

    attachInterrupt(this->pinEncA, globalCheckRotation, CHANGE);
    attachInterrupt(this->pinEncB, globalCheckRotation, CHANGE);
    if(instanceCount < MAX_ENCODERS)
    {
        instances[instanceCount] = this;
        instanceCount++;
    }
}

void Encoder::checkRotation()
{

    old_AB <<= 2; // Remember previous state

    if (digitalRead(pinEncA))
        old_AB |= 0x02; // Add current state of pin A
    if (digitalRead(pinEncB))
        old_AB |= 0x01; // Add current state of pin B

    encval += enc_states[(old_AB & 0x0f)];

    if (encval > 3)
    { // Four steps forward
        direction = 1;
        detected = 1;
        encval = 0;
    }
    else if (encval < -3)
    { // Four steps backward
        direction = 0;
        detected = 1;
        encval = 0;
    }
}

void Encoder::globalCheckRotation()
{
    for (int i = 0; i < instanceCount; i++)
    {
        instances[i]->checkRotation();
    }
}

int Encoder::invoke()
{
    if (detected)
    {
        if (direction)
        {
            actionRight->invoke();
        }
        else
        {
            actionLeft->invoke();
        }
        detected = 0;
    }

    if (!digitalRead(pinButton))
    {
        actionButton->invoke();
    }
}
std::string Encoder::serialize()
{
    JsonDocument doc;
    doc["id"] = id;

    JsonDocument actionLeftDoc;
    deserializeJson(actionLeftDoc, actionLeft->serialize());
    doc["actionLeft"] = actionLeftDoc;
    JsonDocument actionRightDoc;
    deserializeJson(actionRightDoc, actionRight->serialize());
    doc["actionRight"] = actionRightDoc;
    JsonDocument actionButtonDoc;
    deserializeJson(actionButtonDoc, actionButton->serialize());
    doc["actionButton"] = actionButtonDoc;
    std::string output;
    serializeJson(doc, output);
    return output;
}