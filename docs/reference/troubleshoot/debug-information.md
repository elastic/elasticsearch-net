---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/debug-information.html
---

# Debug information [debug-information]

Every response from Elasticsearch.Net and NEST contains a `DebugInformation` property that provides a human readable description of what happened during the request for both successful and failed requests

```csharp
var response = client.Search<Project>(s => s
    .Query(q => q
        .MatchAll()
    )
);

response.DebugInformation.Should().Contain("Valid NEST response");
```

This can be useful in tracking down numerous problems and can also be useful when filing an [issue](https://github.com/elastic/elasticsearch-net/issues) on the GitHub repository.

## Request and response bytes [_request_and_response_bytes]

By default, the request and response bytes are not available within the debug information, but can be enabled globally on Connection Settings by setting `DisableDirectStreaming`. This disables direct streaming of

1. the serialized request type to the request stream
2. the response stream to a deserialized response type

```csharp
var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

var settings = new ConnectionSettings(connectionPool)
    .DisableDirectStreaming(); <1>

var client = new ElasticClient(settings);
```

1. disable direct streaming for **all** requests


or on a *per request* basis

```csharp
var response = client.Search<Project>(s => s
    .RequestConfiguration(r => r
        .DisableDirectStreaming() <1>
    )
    .Query(q => q
        .MatchAll()
    )
);
```

1. disable direct streaming for **this** request only


Configuring `DisableDirectStreaming` on an individual request takes precedence over any global configuration.

There is typically a performance and allocation cost associated with disabling direct streaming since both the request and response bytes must be buffered in memory, to allow them to be exposed on the response call details.


## TCP statistics [_tcp_statistics]

It can often be useful to see the statistics for active TCP connections, particularly when trying to diagnose issues with the client. The client can collect the states of active TCP connections just before making a request, and expose these on the response and in the debug information.

Similarly to `DisableDirectStreaming`, TCP statistics can be collected for every request by configuring on `ConnectionSettings`

```csharp
var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

var settings = new ConnectionSettings(connectionPool)
    .EnableTcpStats(); <1>

var client = new ElasticClient(settings);
```

1. collect TCP statistics for **all** requests


or on a *per request* basis

```csharp
var response = client.Search<Project>(s => s
    .RequestConfiguration(r => r
        .EnableTcpStats() <1>
    )
    .Query(q => q
        .MatchAll()
    )
);

var debugInformation = response.DebugInformation;
```

1. collect TCP statistics for **this** request only


With `EnableTcpStats` set, the states of active TCP connections will now be included on the response and in the debug information.

The client includes a `TcpStats` class to help with retrieving more detail about active TCP connections should it be required

```csharp
var tcpStatistics = TcpStats.GetActiveTcpConnections(); <1>
var ipv4Stats = TcpStats.GetTcpStatistics(NetworkInterfaceComponent.IPv4); <2>
var ipv6Stats = TcpStats.GetTcpStatistics(NetworkInterfaceComponent.IPv6); <3>

var response = client.Search<Project>(s => s
    .Query(q => q
        .MatchAll()
    )
);
```

1. Retrieve details about active TCP connections, including local and remote addresses and ports
2. Retrieve statistics about IPv4
3. Retrieve statistics about IPv6


::::{note} 
Collecting TCP statistics may not be accessible in all environments, for example, Azure App Services. When this is the case, `TcpStats.GetActiveTcpConnections()` returns `null`.

::::



## ThreadPool statistics [_threadpool_statistics]

It can often be useful to see the statistics for thread pool threads, particularly when trying to diagnose issues with the client. The client can collect statistics for both worker threads and asynchronous I/O threads, and expose these on the response and in debug information.

Similar to collecting TCP statistics, ThreadPool statistics can be collected for all requests by configuring `EnableThreadPoolStats` on `ConnectionSettings`

```csharp
var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

var settings = new ConnectionSettings(connectionPool)
     .EnableThreadPoolStats(); <1>

var client = new ElasticClient(settings);
```

1. collect thread pool statistics for **all** requests


or on a *per request* basis

```csharp
var response = client.Search<Project>(s => s
     .RequestConfiguration(r => r
             .EnableThreadPoolStats() <1>
     )
     .Query(q => q
         .MatchAll()
     )
 );

var debugInformation = response.DebugInformation; <2>
```

1. collect thread pool statistics for **this** request only
2. contains thread pool statistics


With `EnableThreadPoolStats` set, the statistics of thread pool threads will now be included on the response and in the debug information.


