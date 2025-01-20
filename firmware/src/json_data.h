#pragma once

static const char* json_data = R"({
    "name": "Example Config",
    "buttons": [
         {
            "id": 0,
            "action": {
                "type": "MacroAction",
                "actions":
                [
                    {
                        "type": "KeyboardAction",
                        "details": {
                            "key": 4,
                            "state" : 3
                        }
                    },
                    {
                        "type": "KeyboardAction",
                        "details": {
                            "key": 5,
                            "state" : 3
                        }
                    },
                    {
                        "type": "KeyboardAction",
                        "details": {
                            "key": 6,
                            "state" : 3
                        }
                    }
                ]
                 
            }
        },
        {
            "id": 1,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 5,
                    "state" : 3
                } 
            }
        },
        {
            "id": 2,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 6,
                    "state" : 3
                } 
            }
        },
        {
            "id": 3,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 7,
                    "state" : 3
                } 
            }
        },
        {
            "id": 4,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 8,
                    "state" : 3
                } 
            }
        },
        {
            "id": 5,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 9,
                    "state" : 3
                } 
            }
        },
        {
            "id": 6,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 10,
                    "state" : 3
                } 
            }
        },
        {
            "id": 7,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 11,
                    "state" : 3
                } 
            }
        },
        {
            "id": 8,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 12,
                    "state" : 3
                } 
            }
        },
        {
            "id": 9,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 13,
                    "state" : 3
                } 
            }
        },
        {
            "id": 10,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 14,
                    "state" : 3
                } 
            }
        },
        {
            "id": 11,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 15,
                    "state" : 3
                } 
            }
        },
        {
            "id": 12,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 16,
                    "state" : 3
                } 
            }
        },
        {
            "id": 13,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 17,
                    "state" : 3
                } 
            }
        },
        {
            "id": 14,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 18,
                    "state" : 3
                } 
            }
        },
        {
            "id": 15,
            "action": {
                "type": "KeyboardAction",
                "details": {
                    "key": 19,
                    "state" : 3
                } 
            }
        }
    ],


    "encoders": [
        {
            "id": 0,
            "actionLeft": {
               "type": "KeyboardAction",
                "details": {
                    "key": 5,
                    "state" : 3
                } 
            },
            "actionRight": {
                "type": "KeyboardAction",
                "details": {
                    "key": 6,
                    "state" : 3
                } 
            },
            "actionButton": {
                "type": "KeyboardAction",
                "details": {
                    "key": 7,
                    "state" : 3
                } 
            }
        }
        
    ]
})";
