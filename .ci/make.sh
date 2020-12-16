#!/usr/bin/env bash
# parameters are available to this script

# common build entry script for all elasticsearch clients

# ./.ci/make.sh assemble VERSION

script_path=$(dirname "$(realpath -s "$0")")
repo=$(realpath "$script_path/../")

# shellcheck disable=SC1090
CMD=$1
TASK=$1
VERSION=$2
STACK_VERSION=$VERSION
set -euo pipefail

output_folder=".ci/output"
OUTPUT_DIR="$repo/${output_folder}"
mkdir -p "$OUTPUT_DIR"

DOTNET_VERSION=${DOTNET_VERSION-5.0.100}

echo -e "\033[34;1mINFO:\033[0m VERSION ${STACK_VERSION}\033[0m"
echo -e "\033[34;1mINFO:\033[0m OUTPUT_DIR ${OUTPUT_DIR}\033[0m"
echo -e "\033[34;1mINFO:\033[0m DOTNET_VERSION ${DOTNET_VERSION}\033[0m"
 
echo -e "\033[1m>>>>> Build [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>\033[0m"

docker build --file .ci/DockerFile --tag elastic/elasticsearch-net .

echo -e "\033[1m>>>>> Run [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>\033[0m"

case $CMD in
    assemble)
        TASK=release
        ;;
    *)
        echo -e "\nUsage:\n\t $CMD is not supported right now\n"
        exit 1
esac


docker run \
  --env "DOTNET_VERSION" \
  --name test-runner \
  --volume "${OUTPUT_DIR}:/sln/${output_folder}" \
  --rm \
  elastic/elasticsearch-net \
  ./build.sh $TASK "$VERSION" "$output_folder" "skiptests"
