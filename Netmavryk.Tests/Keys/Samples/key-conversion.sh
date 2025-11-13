#!/bin/bash

# Set the input and output JSON file names
INPUT_JSON_FILE="nistp256.json"
OUTPUT_JSON_FILE="output.json"

# Read the original JSON content
json_content=$(cat "$INPUT_JSON_FILE")

# Get the length of the JSON array
length=$(jq length <<< "$json_content")

# Loop through each entry and prompt for a new pkh value
for ((i = 0; i < length; i++)); do
  current_pk=$(jq -r ".[$i].pub" <<< "$json_content")
  echo "Current pk for entry $i is $current_pk. New pkh:"
  key=$(kubectl exec -it mavryk-baking-node-0 -c mavkit-node -- mavkit-client import public key taquito unencrypted:$current_pk --force > key)
  new_pkh=$(grep -o '\<mv3[^[:space:]]*' key)
  echo $new_pkh

  # Update the pkh value in the JSON content
  json_content=$(jq --arg new_pkh "$new_pkh" ".[$i].pkh = \$new_pkh" <<< "$json_content")
done

# Output the modified JSON content to the output file
echo "$json_content" > "$OUTPUT_JSON_FILE"

echo "Modified JSON saved to $OUTPUT_JSON_FILE."
