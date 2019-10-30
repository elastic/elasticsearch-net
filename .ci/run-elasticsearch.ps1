# Launch one or more Elasticsearch nodes via the Docker image,
# to form a cluster suitable for running the REST API tests.
#
# Export the ELASTICSEARCH_VERSION variable, eg. 'elasticsearch:8.0.0-SNAPSHOT'.

param(
    [string]
    $NODE_NAME,

    [string]
    $MASTER_NODE_NAME,

    [string]
    $CLUSTER_NAME,

    [int]
    $HTTP_PORT = 9200,

    [string]
    $ELASTIC_PASSWORD = "changeme",

    [string]
    $SSL_CERT = "$PWD/certs/testnode.crt",

    [string]
    $SSL_KEY = "$PWD/certs/testnode.key",

    [string]
    $SSL_CA = "$PWD/certs/ca.crt",

    [switch]
    $DETACH,

    [switch]
    $CLEANUP,

    [string]
    $NETWORK_NAME

)

trap {
  cleanup
}

$ESC = [char]27

if ($null -eq $env:ELASTICSEARCH_VERSION) {
  Write-Error "ERROR: Required environment variable [ELASTICSEARCH_VERSION] not set"
  exit 1
}

$moniker = $env:ELASTICSEARCH_VERSION -replace "[^a-zA-Z\d]", "-" 
$suffix = "rest-test"

if (!$NODE_NAME) {
  $NODE_NAME = "${moniker}node1"
}

if (!$MASTER_NODE_NAME) {
  $MASTER_NODE_NAME = $NODE_NAME
}

if (!$CLUSTER_NAME) {
  $CLUSTER_NAME = "${moniker}${suffix}"
}

$volume_name = "${NODE_NAME}-${suffix}-data"

if (!$NETWORK_NAME) {
  $NETWORK_NAME= "${moniker}${suffix}"
}


function cleanup_volume {
  param(
    [string]
    $Name
  )

  if ("$(docker volume ls --quiet --filter name="$Name")") {
    Write-Output "$ESC[34;1mINFO:$ESC[0m Removing volume $Name$ESC[0m"
    docker volume rm "$Name"
  }
}
function cleanup_node {
  param(
    [string]
    $Name
  )

  if ("$(docker ps --quiet --filter name="$Name")") {
    Write-Output "$ESC[34;1mINFO:$ESC[0m Removing container $Name$ESC[0m"
    docker container rm --force --volumes "$Name"
    cleanup_volume "$Name-${suffix}-data"
  }
}
function cleanup_network {
  param(
    [string]
    $Name
  )

  if ("$(docker network ls --quiet --filter name="$Name")") {
    Write-Output "$ESC[34;1mINFO:$ESC[0m Removing network ${Name}$ESC[0m"
    docker network rm "$Name"
  }
}

function cleanup {
  param(
    [switch]
    $Cleanup
  )

  if ((-not $DETACH) -or $Cleanup) {
    Write-Output "$ESC[34;1mINFO:$ESC[0m clean the node and volume on startup (1) OR on exit if not detached$ESC[0m"
    cleanup_node "$NODE_NAME"
  }
  if (-not $DETACH) {
    Write-Output "$ESC[34;1mINFO:$ESC[0m clean the network if not detached (start and exit)$ESC[0m"
    cleanup_network "$NETWORK_NAME"
  }
}

if ($CLEANUP) {
  #trap - EXIT
  if ("$(docker network ls --quiet --filter name=${NETWORK_NAME})" -eq "") {
    Write-Output "$ESC[34;1mINFO:$ESC[0m $NETWORK_NAME is already deleted$ESC[0m"
    exit 0
  }
  $containers = $(docker network inspect --format '{{ range $key, $value := .Containers }}{{ printf "%s\n" .Name}}{{ end }}' ${NETWORK_NAME})

  foreach($container in $containers) {
    cleanup_node "$container"
  }

  cleanup_network "$NETWORK_NAME"
  Write-Output "$ESC[32;1mSUCCESS:$ESC[0m Cleaned up and exiting$ESC[0m"
  exit 0
}

Write-Output "$ESC[34;1mINFO:$ESC[0m Making sure previous run leftover infrastructure is removed$ESC[0m"
cleanup -Cleanup

Write-Output "$ESC[34;1mINFO:$ESC[0m Creating network $NETWORK_NAME if it does not exist already$ESC[0m"

docker network inspect "$NETWORK_NAME" | Out-Null
if ($LASTEXITCODE -ne 0) {
  docker network create "$NETWORK_NAME" 
}

$environment = @(
  "--env", "node.name=`"$NODE_NAME`"",
  "--env", "cluster.name=`"$CLUSTER_NAME`"",
  "--env", "cluster.initial_master_nodes=`"$MASTER_NODE_NAME`"",
  "--env", "discovery.seed_hosts=`"$MASTER_NODE_NAME`"",
  "--env", "cluster.routing.allocation.disk.threshold_enabled=false",
  "--env", "bootstrap.memory_lock=true",
  "--env", "node.attr.testattr=test",
  "--env", "path.repo=/tmp",
  "--env", "repositories.url.allowed_urls=http://snapshot.test*"
)

$volumes = @(
  "--volume", "${volume_name}:/usr/share/elasticsearch/data"
)

if (-not $env:ELASTICSEARCH_VERSION -contains "oss") {
  $environment += @(
    "--env", "ELASTIC_PASSWORD=`"$ELASTIC_PASSWORD`"",
    "--env", "xpack.license.self_generated.type=trial",
    "--env", "xpack.security.enabled=true",
    "--env", "xpack.security.http.ssl.enabled=true",
    "--env", "xpack.security.http.ssl.verification_mode=certificate",
    "--env", "xpack.security.http.ssl.key=certs/testnode.key",
    "--env", "xpack.security.http.ssl.certificate=certs/testnode.crt",
    "--env", "xpack.security.http.ssl.certificate_authorities=certs/ca.crt",
    "--env", "xpack.security.transport.ssl.enabled=true",
    "--env", "xpack.security.transport.ssl.key=certs/testnode.key",
    "--env", "xpack.security.transport.ssl.certificate=certs/testnode.crt",
    "--env", "xpack.security.transport.ssl.certificate_authorities=certs/ca.crt"
  )

  $volumes += @(
    "--volume", "`"${SSL_CERT}`":/usr/share/elasticsearch/config/certs/testnode.crt",
    "--volume", "`"${SSL_KEY}`":/usr/share/elasticsearch/config/certs/testnode.key",
    "--volume", "`"${SSL_CA}`":/usr/share/elasticsearch/config/certs/ca.crt"
  )
}

$url="http://$NODE_NAME"
if (-not $env:ELASTICSEARCH_VERSION -contains "oss") {
  $url="https://elastic:$ELASTIC_PASSWORD@$NODE_NAME"
}

Write-Output "$ESC[34;1mINFO:$ESC[0m Starting container $NODE_NAME $ESC[0m"

if ($DETACH) {
  $d = "true"
} else {
  $d = "false"
}

docker run `
  --name "`"$NODE_NAME`"" `
  --network "`"$NETWORK_NAME`"" `
  --env ES_JAVA_OPTS=-"`"Xms1g -Xmx1g`"" `
  $environment `
  $volumes `
  --publish ${HTTP_PORT}:9200 `
  --ulimit nofile=65536:65536 `
  --ulimit memlock=-1:-1 `
  --detach="$d" `
  --health-cmd="`"curl --silent --fail ${url}:9200/_cluster/health || exit 1`"" `
  --health-interval=2s `
  --health-retries=20 `
  --health-timeout=2s `
  --rm docker.elastic.co/elasticsearch/"$($env:ELASTICSEARCH_VERSION)"


if ($DETACH) {
  while("$(docker inspect -f '{{.State.Health.Status}}' ${NODE_NAME})" -eq "starting") {
    Start-Sleep 2;
    Write-Output ""
    Write-Output "$ESC[34;1mINFO:$ESC[0m waiting for node $NODE_NAME to be up$ESC[0m"
  }

  # Always show the node getting started logs, this is very useful both on CI as well as while developing
  docker logs "$NODE_NAME"

  if ("$(docker inspect -f '{{.State.Health.Status}}' ${NODE_NAME})" -ne "healthy") {
    cleanup -Cleanup
    Write-Output ""
    Write-Output "$ESC[31;1mERROR:$ESC[0m Failed to start $($env:ELASTICSEARCH_VERSION) in detached mode beyond health checks$ESC[0m"
    Write-Output "$ESC[31;1mERROR:$ESC[0m dumped the docker log before shutting the node down$ESC[0m"
    exit 1
  } else {
    Write-Output ""
    Write-Output "$ESC[32;1mSUCCESS:$ESC[0m Detached and healthy: ${NODE_NAME} on docker network: ${NETWORK_NAME}$ESC[0m"
    $es_host = $url
    if (!$es_host) {
      $es_host = $NODE_NAME
    }
    if (!$es_host) {
      $es_host = "localhost"
    }

    Write-Output "$ESC[32;1mSUCCESS:$ESC[0m Running on: ${es_host}:${HTTP_PORT}$ESC[0m"
    exit 0
  }
}
