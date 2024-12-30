#pragma once

#include "PhysicalInput.hpp"
#define MAX_ENCODERS 10

static const int8_t enc_states[] = {0, -1, 1, 0, 1, 0, 0, -1, -1, 0, 0, 1, 0, 1, -1, 0};

class Encoder: PhysicalInput
{
    private:

        static Encoder* instances[MAX_ENCODERS];
        static int instanceCount;
        int instanceId;

        int pinEncA; // input_pullup
        int pinEncB; // input_pullup
        int pinButton; // input_pullup

        int encval = 0;
        int old_AB = 3;

        bool detected = false;
        bool direction = 0; // 0->left, 1->right

        Action* actionLeft;
        Action* actionRight;
        Action* actionButton;

        void checkRotation();
        
    public:
        Encoder(int pinEncA, int pinEncB, int pinButton, Action* actionLeft, Action* actionRight, Action* actionButton);
        int invoke();
        static void globalCheckRotation();
};


