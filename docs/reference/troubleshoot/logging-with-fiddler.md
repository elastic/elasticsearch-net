---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/logging-with-fiddler.html
---

# Logging with Fiddler [logging-with-fiddler]

A web debugging proxy such as [Fiddler](http://www.telerik.com/fiddler) is a useful way to capture HTTP traffic from a machine, particularly whilst developing against a local Elasticsearch cluster.

## Capturing traffic to a remote cluster [_capturing_traffic_to_a_remote_cluster]

To capture traffic against a remote cluster is as simple as launching Fiddler! You may want to also filter traffic to only show requests to the remote cluster by using the filters tab

:::{image} /troubleshoot/images/elasticsearch-client-net-api-capture-requests-remotehost.png
:alt: Capturing requests to a remote host
:::


## Capturing traffic to a local cluster [_capturing_traffic_to_a_local_cluster]

The .NET Framework is hardcoded not to send requests for `localhost` through any proxies and as a proxy Fiddler will not receive such traffic.

This is easily circumvented by using `ipv4.fiddler` as the hostname instead of `localhost`

```csharp
var isFiddlerRunning = Process.GetProcessesByName("fiddler").Any();
var host = isFiddlerRunning ? "ipv4.fiddler" : "localhost";

var connectionSettings = new ConnectionSettings(new Uri($"http://{host}:9200"))
    .PrettyJson(); <1>

var client = new ElasticClient(connectionSettings);
```

1. prettify json requests and responses to make them easier to read in Fiddler


With Fiddler running, the requests and responses will now be captured and can be inspected in the Inspectors tab

:::{image} /troubleshoot/images/elasticsearch-client-net-api-inspect-requests.png
:alt: Inspecting requests and responses
:::

As before, you may also want to filter traffic to only show requests to `ipv4.fiddler` on the port on which you are running Elasticsearch.

:::{image} /troubleshoot/images/elasticsearch-client-net-api-capture-requests-localhost.png
:alt: Capturing requests to localhost
:::


