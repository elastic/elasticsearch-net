#!/usr/bin/env bash
# parameters are available to this script

# ELASTICSEARCH_VERSION -- version e.g Major.Minor.Patch(-Prelease)
# ELASTICSEARCH_CONTAINER -- the docker moniker as a reference to know which docker image distribution is used
# ELASTICSEARCH_URL -- The url at which elasticsearch is reachable
# NETWORK_NAME -- The docker network name
# NODE_NAME -- The docker container name also used as Elasticsearch node name
# DOTNET_VERSION -- SDK version (defined in test-matrix.yml, a default is hardcoded here)

script_path=$(dirname $(realpath -s $0))
source $script_path/functions/imports.sh
set -euo pipefail

DOTNET_VERSION=${DOTNET_VERSION-3.0.100}

echo -e "\033[34;1mINFO:\033[0m VERSION ${STACK_VERSION}\033[0m"
echo -e "\033[34;1mINFO:\033[0m TEST_SUITE ${TEST_SUITE}\033[0m"
echo -e "\033[34;1mINFO:\033[0m DOTNET_VERSION ${DOTNET_VERSION}\033[0m"
echo -e "\033[34;1mINFO:\033[0m URL ${elasticsearch_url}\033[0m"
echo -e "\033[34;1mINFO:\033[0m CONTAINER ${elasticsearch_container}\033[0m"


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
  ./build.sh rest-spec-tests $TEST_SUITE -e ${elasticsearch_url} -o /sln/build/output/rest-spec-junit.xml
