Import("env")
import os

# Ścieżka do pliku JSON
json_file = "DoNotTrack/example_config2.json"
output_file = "src/json_data.h"

with open(json_file, "r") as f:
    json_content = f.read()

# Tworzenie nagłówka
with open(output_file, "w") as f:
    f.write('#pragma once\n\n')
    f.write(f'static const char* json_data = R"({json_content})";\n')
