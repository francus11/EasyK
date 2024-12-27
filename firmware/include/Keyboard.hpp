#pragma once

#include "HIDDevice.hpp"
#include "KeyLayout.h"


class Keyboard_ : public HIDDevice_ {
private:

public:
    Keyboard_(void);
    int add(KeyboardKeycode k);
    int remove(KeyboardKeycode k);
    int removeAll();
    int press(KeyboardKeycode key);
    int release(KeyboardKeycode key);
    int releaseAll();
    int click(KeyboardKeycode key);
    int set(KeyboardKeycode key, bool s);
};
extern Keyboard_ Keyboard;