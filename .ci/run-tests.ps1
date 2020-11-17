param (
    [string]
    [Parameter(Mandatory = $true)]
    $ELASTICSEARCH_VERSION,

    [string]
    [ValidateSet("oss", "xpack")]
    $TEST_SUITE = "oss",

    [string]
    $DOTNET_VERSION = "5.0.100"
)

$ESC = [char]27
$NODE_NAME = "es1"
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


try {
    Write-Output "$ESC[1m>>>>> Start [$env:ELASTICSEARCH_VERSION container] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>$ESC[0m"

    $runParams = @{
        NODE_NAME= "$NODE_NAME"
        NETWORK_NAME = "elasticsearch"
        DETACH = $true
    }

    ./.ci/run-elasticsearch.ps1 @runParams

    Write-Output "$ESC[1m>>>>> Repository specific tests >>>>>>>>>>>>>>>>>>>>>>>>>>>>>$ESC[0m"

    $runParams = @{
        ELASTICSEARCH_VERSION = "$env:ELASTICSEARCH_VERSION"
        ELASTICSEARCH_CONTAINER = "$env:ELASTICSEARCH_VERSION"
        NETWORK_NAME = "elasticsearch"
        NODE_NAME= "$NODE_NAME"
        ELASTICSEARCH_URL = $elasticsearch_url
        DOTNET_VERSION = $DOTNET_VERSION
    }

    ./.ci/run-repository.ps1 @runParams

    cleanup   
}
catch {
    cleanup
}

