#include "MacroPadRunner.hpp"

MacroPadRunner::MacroPadRunner(PhysicalInput* buttons[16], Encoder* encoder)
{
    for (int i = 0; i < 16; i++)
    {
        this->buttons[i] = buttons[i];
    }

    this->encoder = encoder;
}

MacroPadRunner::~MacroPadRunner()
{
    delete this->encoder;

    for (int i = 0; i < 16; i++)
    {
        delete &buttons[i];
    }
}

MacroPadRunner* MacroPadRunner::deserialize(std::string json)
{
    JsonDocument doc;
    deserializeJson(doc, json.c_str());
    PhysicalInput* buttons[16];

    for (int i = 0; i < 16; i++)
    {
        int* offs = new int[4]{buttonPins[i][3], buttonPins[i][4], buttonPins[i][5], buttonPins[i][6]};
        Action* action = Action::deserialize(doc["buttons"][i]["action"]);
        buttons[i] = new ButtonSecured(buttonPins[i][0], buttonPins[i][1], buttonPins[i][2], action, offs, 4);
    }

    Action* actionLeft = Action::deserialize(doc["encoders"][0]["actionLeft"]);
    Action* actionRight = Action::deserialize(doc["encoders"][0]["actionRight"]);
    Action* actionButton = Action::deserialize(doc["encoders"][0]["actionButton"]);

    Encoder* encoder = new Encoder(PIN_ENC_A, PIN_ENC_B, PIN_ENC_BUTTON, actionLeft, actionRight, actionButton);

    return new MacroPadRunner(buttons, encoder);
}

void MacroPadRunner::run()
{
    for (int i = 0; i < 16; i++)
    {
        buttons[i]->invoke();
    }

    encoder->invoke();

}

std::string MacroPadRunner::serialize()
{
    JsonDocument doc;
    JsonArray buttonsArray = doc.createNestedArray("buttons");

    for (int i = 0; i < 16; i++)
    {
        buttonsArray.add(buttons[i]->serialize());
    }

    // JsonArray encoderArray = doc.createNestedArray("encoders");
    // encoderArray.add(encoder->serialize());

    std::string output;
    serializeJson(doc, output);
    return output;
}