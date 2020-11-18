# ELASTICSEARCH_URL -- The url at which elasticsearch is reachable
# NETWORK_NAME -- The docker network name
# NODE_NAME -- The docker container name also used as Elasticsearch node name
# DOTNET_VERSION -- SDK version (defined in test-matrix.yml, a default is hardcoded here)

param(
    [System.Uri]
    $ELASTICSEARCH_URL,
    
    [string]
    $NETWORK_NAME,
    
    [string]
    $NODE_NAME,
    
    [string]
    $DOTNET_VERSION = "5.0.100"
)

$ESC = [char]27

Write-Output "$ESC[34;1mINFO:$ESC[0m URL ${ELASTICSEARCH_URL}$ESC[0m"
Write-Output "$ESC[34;1mINFO:$ESC[0m DOTNET_VERSION ${DOTNET_VERSION}$ESC[0m"

Write-Output "$ESC[1m>>>>> Build [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>$ESC[0m"

docker build --file .ci/DockerFile --tag elastic/elasticsearch-net .

Write-Output "$ESC[1m>>>>> Run [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>$ESC[0m"

mkdir -p build/output -ErrorAction Ignore

$repo = Get-Location

docker run `
    --network="`"$NETWORK_NAME`"" `
    --env "DOTNET_VERSION=$DOTNET_VERSION" `
    --name test-runner `
    --volume $repo/build/output:/sln/build/output `
    --rm `
        elastic/elasticsearch-net `
        ./build.sh rest-spec-tests -f count -e $ELASTICSEARCH_URL -o /sln/build/output/rest-spec-junit.xml