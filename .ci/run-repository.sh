#!/usr/bin/env bash
# parameters are available to this script


# STACK_VERSION -- version e.g Major.Minor.Patch(-Prelease)
# TEST_SUITE -- which test suite to run: oss or xpack
# ELASTICSEARCH_URL -- The url at which elasticsearch is reachable, a default is composed based on STACK_VERSION and TEST_SUITE
# DOTNET_VERSION -- SDK version (defined in test-matrix.yml, a default is hardcoded here)
script_path=$(dirname $(realpath -s $0))
source $script_path/functions/imports.sh
set -euo pipefail

DOTNET_VERSION=${DOTNET_VERSION-5.0.100}
ELASTICSEARCH_URL=${ELASTICSEARCH_URL-"$elasticsearch_url"}
elasticsearch_container=${elasticsearch_container-}

TEST_FOLDER=${TEST_FOLDER-}
TEST_FILE=${TEST_FILE-}
TEST_SECTION=${TEST_SECTION-}

run_script_args=""
if [[ "$TEST_FOLDER" != "" ]]; then
  run_script_args="-f ${TEST_FOLDER}"
fi
if [[ "$TEST_FILE" != "" ]]; then
  run_script_args="${run_script_args} -t ${TEST_FILE}"
fi
if [[ "$TEST_SECTION" != "" ]]; then
  run_script_args="${run_script_args} -s ${TEST_SECTION}"
fi
 
echo -e "\033[34;1mINFO:\033[0m VERSION ${STACK_VERSION}\033[0m"
echo -e "\033[34;1mINFO:\033[0m TEST_SUITE ${TEST_SUITE}\033[0m"
echo -e "\033[34;1mINFO:\033[0m URL ${ELASTICSEARCH_URL}\033[0m"
echo -e "\033[34;1mINFO:\033[0m CONTAINER ${elasticsearch_container}\033[0m"
echo -e "\033[34;1mINFO:\033[0m DOTNET_VERSION ${DOTNET_VERSION}\033[0m"
 
echo -e "\033[1m>>>>> Build [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>\033[0m"

docker build --file .ci/DockerFile --tag elastic/elasticsearch-net .

echo -e "\033[1m>>>>> Run [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>\033[0m"

mkdir -p build/output

repo=$(realpath $(dirname $(realpath -s $0))/../)

docker run \
  --network=${network_name} \
  --env "DOTNET_VERSION" \
  --name test-runner \
  --volume ${repo}/build/output:/sln/build/output \
  --rm \
  elastic/elasticsearch-net \
  ./build.sh rest-spec-tests $TEST_SUITE -e ${ELASTICSEARCH_URL} -o /sln/build/output/rest-spec-junit.xml ${run_script_args}
