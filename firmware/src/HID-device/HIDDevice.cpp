#include "HIDDevice.hpp"

HIDDevice_::HIDDevice_(const uint8_t* descriptorReport, int descriptorSize, unsigned int reportID, uint8_t* report, int reportSize) {
    this->descriptorReport = descriptorReport;
    this->descriptorSize = descriptorSize;
    this->reportID = reportID;
    this->report = report;
    this->reportSize = reportSize;
    static HIDSubDescriptor node(descriptorReport, descriptorSize);
    HID().AppendDescriptor(&node);
    clearReport();
}

int HIDDevice_::send() {
    return HID().SendReport(reportID, report, reportSize);
}