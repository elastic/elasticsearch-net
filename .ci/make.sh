#!/usr/bin/env bash
# parameters are available to this script

# common build entry script for all elasticsearch clients

# ./.ci/make.sh assemble VERSION

script_path=$(dirname "$(realpath -s "$0")")
repo=$(realpath "$script_path/../")

# shellcheck disable=SC1090
CMD=$1
TASK=$1
TASK_ARGS=()
VERSION=$2
STACK_VERSION=$VERSION
set -euo pipefail

output_folder=".ci/output"
OUTPUT_DIR="$repo/${output_folder}"
REPO_BINDING="${OUTPUT_DIR}:/sln/${output_folder}"
mkdir -p "$OUTPUT_DIR"

DOTNET_VERSION=${DOTNET_VERSION-5.0.103}

echo -e "\033[34;1mINFO:\033[0m VERSION ${STACK_VERSION}\033[0m"
echo -e "\033[34;1mINFO:\033[0m OUTPUT_DIR ${OUTPUT_DIR}\033[0m"
echo -e "\033[34;1mINFO:\033[0m DOTNET_VERSION ${DOTNET_VERSION}\033[0m"
 
echo -e "\033[1m>>>>> Build [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>\033[0m"

docker build --file .ci/DockerFile --tag elastic/elasticsearch-net \
  --build-arg USER_ID="$(id -u)" \
  --build-arg GROUP_ID="$(id -g)" .

echo -e "\033[1m>>>>> Run [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>\033[0m"

case $CMD in
    clean)
        echo -e "\033[36;1mRemoving $output_folder\033[0m"
        TASK=clean
        rm -rf "$output_folder"
        ;;
    assemble)
        TASK=release
        TASK_ARGS=("$VERSION" "$output_folder" "skiptests")
        ;;
    codegen)
        TASK=codegen
        # VERSION is BRANCH here for now
        TASK_ARGS=("$VERSION") 
        REPO_BINDING="$repo:/sln"
        ;;
    *)
        echo -e "\nUsage:\n\t $CMD is not supported right now\n"
        exit 1
esac

# -u does not work need to be root inside the container, the chown hack at the end ensures
# we still own any new files at the end of this run

docker run \
 --env "DOTNET_VERSION" \
 --name test-runner \
 --volume $REPO_BINDING \
 --rm \
 elastic/elasticsearch-net \
 /bin/bash -c "./build.sh $TASK ${TASK_ARGS[*]} && chown -R $(id -u):$(id -g) ."