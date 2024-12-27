#include "Keyboard.hpp"
#include <stdint.h>

#define KEYBOARD_REPORT_ID 0x01
#define KEYBOARD_KEY_COUNT 104

struct ATTRIBUTE_PACKED {
    uint8_t modifiers;
    uint8_t keys[KEYBOARD_KEY_COUNT / 8];
} KeyboardReport;

const uint8_t keyboardDescriptorReport[] PROGMEM = {
    0x05, 0x01,                      /* USAGE_PAGE (Generic Desktop)	  47 */
    0x09, 0x06,                      /* USAGE (Keyboard) */
    0xa1, 0x01,                      /* COLLECTION (Application) */
    0x85, KEYBOARD_REPORT_ID,	 /*   REPORT_ID */
    0x05, 0x07,                      /*   USAGE_PAGE (Keyboard) */

    /* Keyboard Modifiers (shift, alt, ...) */
    0x19, 0xe0,                      /*   USAGE_MINIMUM (Keyboard LeftControl) */
    0x29, 0xe7,                      /*   USAGE_MAXIMUM (Keyboard Right GUI) */
    0x15, 0x00,                      /*   LOGICAL_MINIMUM (0) */
    0x25, 0x01,                      /*   LOGICAL_MAXIMUM (1) */
    0x75, 0x01,                      /*   REPORT_SIZE (1) */
	0x95, 0x08,                      /*   REPORT_COUNT (8) */
    0x81, 0x02,                      /*   INPUT (Data,Var,Abs) */

	/* 104 Keys as bitmap */
	0x19, 0x00,						/*   Usage Minimum (0) */
	0x29, KEYBOARD_KEY_COUNT - 1,	/*   Usage Maximum (103) */
	0x15, 0x00,						/*   Logical Minimum (0) */
	0x25, 0x01,						/*   Logical Maximum (1) */
	0x75, 0x01,						/*   Report Size (1) */
	0x95, KEYBOARD_KEY_COUNT,		/*   Report Count (104) */
	0x81, 0x02,						/*   Input (Data, Variable, Absolute) */

    /* End */
	0xC0						     /*   End Collection */
};

Keyboard_::Keyboard_(void) : HIDDevice_(keyboardDescriptorReport, sizeof(keyboardDescriptorReport), (unsigned int)KEYBOARD_REPORT_ID, reinterpret_cast<uint8_t*>(&KeyboardReport), sizeof(KeyboardReport))
{
    report = reinterpret_cast<uint8_t*>(&KeyboardReport);
	removeAll();
	send();
}

int Keyboard_::add(KeyboardKeycode key) 
{
	// Add key to report
	return set(key, true);
}
int Keyboard_::remove(KeyboardKeycode key) 
{
    // Remove key from report
    return set(key, false);
}

int Keyboard_::press(KeyboardKeycode key)
{
    int ret = add(key);
	if(ret){
		send();
	}
	return ret;
}

int Keyboard_::release(KeyboardKeycode key)
{
    int ret = remove(key);
	if(ret){
		send();
	}
	return ret;
}

int Keyboard_::releaseAll()
{
    for (uint8_t i = 0; i < sizeof(KeyboardReport.keys); i++)
    {
        KeyboardReport.keys[i] = 0;
    }
    return 1;
}

int Keyboard_::click(KeyboardKeycode key)
{
    int ret = press(key);
	if(ret){
		release(key);
	}
	return ret;
}

int Keyboard_::set(KeyboardKeycode key, bool s) 
{
    if (key < KEYBOARD_KEY_COUNT){
		uint8_t bit = 1 << (uint8_t(key) % 8);
		if(s){
			KeyboardReport.keys[key / 8] |= bit;
		}
		else{
			KeyboardReport.keys[key / 8] &= ~bit;
		}
		return 1;
	}

	// It's a modifier key
	if(key >= KEY_LEFT_CTRL && key <= KEY_RIGHT_GUI)
	{
		// Convert key into bitfield (0 - 7)
		key = KeyboardKeycode(uint8_t(key) - uint8_t(KEY_LEFT_CTRL));
		if(s){
			KeyboardReport.modifiers |= (1 << key);
		}
		else{
			KeyboardReport.modifiers &= ~(1 << key);
		}
		return 1;
	}
	
	// No empty/pressed key was found
	return 0;
}

int Keyboard_::removeAll(void)
{
	// Release all keys
	for (uint8_t i = 0; i < sizeof(KeyboardReport.keys); i++)
	{
		KeyboardReport.keys[i] = 0;
	}
	KeyboardReport.modifiers = 0;
	return 1;
}

Keyboard_ Keyboard;