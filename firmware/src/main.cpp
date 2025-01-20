#include <Arduino.h>
#include <string>
#include <Keyboard.hpp>
#include "MacroPadRunner.hpp"
#include "Actions/KeyboardAction.hpp"
#include "KeyLayout.h"
#include "json_data.h"


#define BUTTON_A 3
#define BUTTON_B 10
#define BUTTON_C 9


static const int pins[16][3] = 
{
    {3, 10, INPUT_PULLUP},
    {3, 10, INPUT_PULLDOWN},
    {3, 9, INPUT_PULLUP},
    {3, 9, INPUT_PULLDOWN},
    {2, 10, INPUT_PULLUP},
    {2, 10, INPUT_PULLDOWN},
    {2, 9, INPUT_PULLUP},
    {2, 9, INPUT_PULLDOWN},
    {1, 10, INPUT_PULLUP},
    {1, 10, INPUT_PULLDOWN},
    {1, 9, INPUT_PULLUP},
    {1, 9, INPUT_PULLDOWN},
    {0, 10, INPUT_PULLUP},
    {0, 10, INPUT_PULLDOWN},
    {0, 9, INPUT_PULLUP},
    {0, 9, INPUT_PULLDOWN},
};


MacroPadRunner* runner;
PhysicalInput* buttons[16];

Encoder* encoder;
void setup()
{
    runner = MacroPadRunner::deserialize(json_data);
    
    // encoder = new Encoder(8, 6, 7, new KeyboardAction(KEY_A, "click"), new KeyboardAction(KEY_B, "click"), new KeyboardAction(KEY_C, "click"));
    // for (int i = 0; i < 16; i++)
    // {
    //     int* offs = new int[4]{buttonPins[i][3], buttonPins[i][4], buttonPins[i][5], buttonPins[i][6]};
    //     buttons[i] = new ButtonSecured(
    //         buttonPins[i][0], 
    //         buttonPins[i][1], 
    //         buttonPins[i][2], 
    //         new KeyboardAction(static_cast<KeyboardKeycode>(4+i), "click"), 
    //         offs, 
    //         4);
    // }
    // runner = new MacroPadRunner(buttons, encoder);
    Serial.begin(9600);
}
int buttons_x[2] = {10, 9};
int buttons_y[4] = {3, 2, 1, 0};
bool valid = true;
void loop()
{
    // encoder->invoke();

    runner->run();
    delay(50);
    pinMode(BUTTON_A, INPUT_PULLUP);
    pinMode(BUTTON_B, OUTPUT);
    digitalWrite(BUTTON_B, LOW);
    if (!digitalRead(BUTTON_A) && valid)
    {
        Serial.println("A");
        runner->serialize().c_str();
        Serial.println(runner->serialize().c_str());
        Serial.println("b");
        valid = false;
    }



    // pinMode(BUTTON_A, OUTPUT);aa
    // pinMode(BUTTON_B, INPUT_PULLDOWN);
    // digitalWrite(BUTTON_A, HIGH);
    // if (digitalRead(BUTTON_B))
    // {
    //     Keyboard.click(KEY_A);
    // }
    // pinMode(BUTTON_B, OUTPUT);
    // digitalWrite(BUTTON_B, HIGH);
    // delay(50);

    // pinMode(BUTTON_A, INPUT_PULLDOWN);
    // pinMode(BUTTON_B, OUTPUT);
    // digitalWrite(BUTTON_B, HIGH);
    // if (digitalRead(BUTTON_A))
    // {
    //     Keyboard.click(KEY_B);
    // }
    // pinMode(BUTTON_A, OUTPUT);
    // digitalWrite(BUTTON_A, HIGH);
    // delay(50);

    // pinMode(BUTTON_A, INPUT_PULLUP);
    // pinMode(BUTTON_C, OUTPUT);
    // digitalWrite(BUTTON_C, LOW);
    // if (!digitalRead(BUTTON_A))
    // {
    //     Keyboard.click(KEY_C);
    // }
    // pinMode(BUTTON_A, OUTPUT);
    // digitalWrite(BUTTON_A, LOW);
    // delay(50);

    // pinMode(BUTTON_A, OUTPUT);
    // pinMode(BUTTON_C, INPUT_PULLUP);
    // digitalWrite(BUTTON_A, LOW);
    // if (!digitalRead(BUTTON_C))
    // {
    //     Keyboard.click(KEY_D);
    // }
    // pinMode(BUTTON_C, OUTPUT);
    // digitalWrite(BUTTON_C, LOW);
    // delay(50);
    
    // if (!digitalRead(BUTTON_A))
    // {
    //     Keyboard.press(KEY_A);
    //     Serial.println("Dziala");

    //     // Serial.println("A");
    //     // for(int i=0; i<Keyboard.size; i++){
    //     //     Serial.println(Keyboard.report[i], HEX);
    //     // }
    //     // Serial.println();

    //     // Serial.println(Keyboard.size);
    //     delay(300);
    // }
    // else
    // {
    //     Keyboard.release(KEY_A);;
    // }

    // if (detected)
    // {
    //     if (direction)
    //     {
    //         Serial.println("Right");
    //     }
    //     else
    //     {
    //         Serial.println("Left");
    //     }
    //     detected = 0;
    // }
    // delay(1000);
}

