param (
    [string]
    [Parameter(Mandatory = $true)]
    $ELASTICSEARCH_VERSION,

    [string]
    [ValidateSet("oss", "xpack")]
    $TEST_SUITE = "oss",

    [string]
    $DOTNET_VERSION = "3.0.100"
)

trap {
  cleanup
}

$NODE_NAME = "es1"
$repo = $PWD
$elasticsearch_image= "elasticsearch"
$elasticsearch_url = "https://elastic:changeme@${NODE_NAME}:9200"

if ($TEST_SUITE -ne "xpack") {
  $elasticsearch_image= "elasticsearch-${TEST_SUITE}"
  $elasticsearch_url = "http://${NODE_NAME}:9200"
}

# set for run-elasticsearch.ps1
$env:ELASTICSEARCH_VERSION = "${elasticsearch_image}:$ELASTICSEARCH_VERSION"

function cleanup {
    $status=$?

    $runParams = @{
      NODE_NAME= $NODE_NAME
      NETWORK_NAME = "elasticsearch"
      CLEANUP = $true  
    }

    ./.ci/run-elasticsearch.ps1 @runParams

    # Report status and exit
    if ($status -eq 0) {
      Write-Output "SUCCESS run-tests"
      exit 0
    } else {
      Write-Output "FAILURE during run-tests"
      exit $status
    }
}


Write-Output ">>>>> Start [$env:ELASTICSEARCH_VERSION container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>"

$runParams = @{
  NODE_NAME= "$NODE_NAME"
  NETWORK_NAME = "elasticsearch"
  DETACH = $true  
}

./.ci/run-elasticsearch.ps1 @runParams

Write-Output ">>>>> Build [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>"

docker build --file .ci/DockerFile --tag elastic/elasticsearch-net .

Write-Output ">>>>> Run [elastic/elasticsearch-net container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>"

mkdir -p build/output -ErrorAction Ignore

docker run `
--network=elasticsearch `
--env "DOTNET_VERSION=$DOTNET_VERSION" `
--name test-runner `
--volume ${repo}/build/output:/sln/build/output `
--rm `
elastic/elasticsearch-net `
./build.sh rest-spec-tests -f create -e $elasticsearch_url -o /sln/build/output/rest-spec-junit.xml

cleanup