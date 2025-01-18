#pragma once
#include <stdint.h>

enum class KeyState : uint8_t{
    None,
    Press,
    Release,
    Click,
    Hold
};