#!/usr/bin/env bash
# parameters are available to this script


# common build entry script for all elasticsearch clients

# ./.ci/make.sh bump VERSION
# ./.ci/make.sh release VERSION

script_path=$(dirname "$(realpath -s "$0")")
repo=$(realpath "$script_path/../")

# shellcheck disable=SC1090
cmd=$1
VERSION=$2
STACK_VERSION=$VERSION
source "$script_path/functions/imports.sh"
set -euo pipefail

output_folder=".ci/output"
OUTPUT_DIR="$repo/${output_folder}"

DOTNET_VERSION=${DOTNET_VERSION-3.0.100}

echo -e "\033[34;1mINFO:\033[0m VERSION ${STACK_VERSION}\033[0m"
echo -e "\033[34;1mINFO:\033[0m OUTPUT_DIR ${OUTPUT_DIR}\033[0m"
echo -e "\033[34;1mINFO:\033[0m DOTNET_VERSION ${DOTNET_VERSION}\033[0m"
 
echo -e "\033[1m>>>>> Build [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>\033[0m"

docker build --file .ci/DockerFile --tag elastic/elasticsearch-net .

echo -e "\033[1m>>>>> Run [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>\033[0m"

mkdir -p "$OUTPUT_DIR"

docker run \
  --network=${network_name} \
  --env "DOTNET_VERSION" \
  --name test-runner \
  --volume "${OUTPUT_DIR}:/sln/${output_folder}" \
  --rm \
  elastic/elasticsearch-net \
  ./build.sh release "$VERSION" "$output_folder" "skiptests"
