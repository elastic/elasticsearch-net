---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/connecting.html
---

# Connecting [connecting]

This page contains the information you need to create an instance of the .NET Client for {{es}} that connects to your {{es}} cluster.

It’s possible to connect to your {{es}} cluster via a single node, or by specifying multiple nodes using a node pool. Using a node pool has a few advantages over a single node, such as load balancing and cluster failover support. The client provides convenient configuration options to connect to an Elastic Cloud deployment.

::::{important}

Client applications should create a single instance of `ElasticsearchClient` that is used throughout your application for its entire lifetime. Internally the client manages and maintains HTTP connections to nodes, reusing them to optimize performance. If you use a dependency injection container for your application, the client instance should be registered with a singleton lifetime.

::::

## Connecting to a cloud deployment [cloud-deployment]

[Elastic Cloud](docs-content://deploy-manage/deploy/elastic-cloud/cloud-hosted.md) is the easiest way to get started with {{es}}. When connecting to Elastic Cloud with the .NET {{es}} client you should always use the Cloud ID. You can find this value within the "Manage Deployment" page after you’ve created a cluster (look in the top-left if you’re in Kibana).

We recommend using a Cloud ID whenever possible because your client will be automatically configured for optimal use with Elastic Cloud, including HTTPS and HTTP compression.

Connecting to an Elasticsearch Service deployment is achieved by providing the unique Cloud ID for your deployment when configuring the `ElasticsearchClient` instance. You also require suitable credentials, either a username and password or an API key that your application uses to authenticate with your deployment.

As a security best practice, it is recommended to create a dedicated API key per application, with permissions limited to only those required for any API calls the application is authorized to make.

The following snippet demonstrates how to create a client instance that connects to an {{es}} deployment in the cloud.

```csharp
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

var client = new ElasticsearchClient("<CLOUD_ID>", new ApiKey("<API_KEY>")); <1>
```

1. Replace the placeholder string values above with your cloud ID and the API key configured for your application to access your deployment.

## Connecting to a single node [single-node]

Single node configuration is best suited to connections to a multi-node cluster running behind a load balancer or reverse proxy, which is exposed via a single URL. It may also be convenient to use a single node during local application development. If the URL represents a single {{es}} node, be aware that this offers no resiliency should the server be unreachable or unresponsive.

By default, security features such as authentication and TLS are enabled on {{es}} clusters. When you start {{es}} for the first time, TLS is configured automatically for the HTTP layer. A CA certificate is generated and stored on disk which is used to sign the certificates for the HTTP layer of the {{es}} cluster.

In order for the client to establish a connection with the cluster over HTTPS, the CA certificate must be trusted by the client application. The simplest choice is to use the hex-encoded SHA-256 fingerprint of the CA certificate. The CA fingerprint is output to the terminal when you start {{es}} for the first time. You’ll see a distinct block like the one below in the output from {{es}} (you may have to scroll up if it’s been a while):

```sh
----------------------------------------------------------------
-> Elasticsearch security features have been automatically configured!
-> Authentication is enabled and cluster connections are encrypted.

->  Password for the elastic user (reset with `bin/elasticsearch-reset-password -u elastic`):
  lhQpLELkjkrawaBoaz0Q

->  HTTP CA certificate SHA-256 fingerprint:
  a52dd93511e8c6045e21f16654b77c9ee0f34aea26d9f40320b531c474676228
...
----------------------------------------------------------------
```

Note down the `elastic` user password and HTTP CA fingerprint for the next sections.

The CA fingerprint can also be retrieved at any time from a running cluster using the following command:

```shell
openssl x509 -fingerprint -sha256 -in config/certs/http_ca.crt
```

The command returns the security certificate, including the fingerprint. The `issuer` should be `Elasticsearch security auto-configuration HTTP CA`.

```shell
issuer= /CN=Elasticsearch security auto-configuration HTTP CA
SHA256 Fingerprint=<FINGERPRINT>
```

Visit the [Start the Elastic Stack with security enabled automatically](docs-content://deploy-manage/deploy/self-managed/installing-elasticsearch.md) documentation for more information.

The following snippet shows you how to create a client instance that connects to your {{es}} cluster via a single node, using the CA fingerprint:

```csharp
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

var settings = new ElasticsearchClientSettings(new Uri("https://localhost:9200"))
    .CertificateFingerprint("<FINGERPRINT>")
    .Authentication(new BasicAuthentication("<USERNAME>", "<PASSWORD>"));

var client = new ElasticsearchClient(settings);
```

The preceding snippet demonstrates configuring the client to authenticate by providing a username and password with basic authentication. If preferred, you may also use `ApiKey` authentication as shown in the cloud connection example.

## Connecting to multiple nodes using a node pool [multiple-nodes]

To provide resiliency, you should configure multiple nodes for your cluster to which the client attempts to communicate. By default, the client cycles through nodes for each request in a round robin fashion. The client also tracks unhealthy nodes and avoids sending requests to them until they become healthy.

This configuration is best suited to connect to a known small sized cluster, where you do not require sniffing to detect the cluster topology.

The following snippet shows you how to connect to multiple nodes by using a static node pool:

```csharp
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

var nodes = new Uri[]
{
    new Uri("https://myserver1:9200"),
    new Uri("https://myserver2:9200"),
    new Uri("https://myserver3:9200")
};

var pool = new StaticNodePool(nodes);

var settings = new ElasticsearchClientSettings(pool)
    .CertificateFingerprint("<FINGERPRINT>")
    .Authentication(new ApiKey("<API_KEY>"));

var client = new ElasticsearchClient(settings);
```
