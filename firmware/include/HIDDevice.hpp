#pragma once
#include <stdint.h>
#include <hid.h>

//abstract HID device class
class HIDDevice_ {
public:
    uint8_t* report;

    HIDDevice_(const uint8_t* descriptorReport, int descriptorSize, unsigned int reportID, uint8_t* report, int reportSize);
    
    //clears report by setting all values to 0
    //needs to be implemented in child class
    //it should have send() function called at the end
    virtual void clearReport();
    
    //sends report to host
    int send();
protected:
    unsigned int reportID;
    
    // need to be assigned in child class 
    int reportSize;
    
    const uint8_t* descriptorReport;
    
    int descriptorSize;
};