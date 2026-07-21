---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/debug-mode.html
---

# Debug mode [debug-mode]

The [Debug information](debug-information.md) explains that every response from Elasticsearch.Net and NEST contains a `DebugInformation` property, and properties on `ConnectionSettings` and `RequestConfiguration` can control which additional information is included in debug information, for all requests or on a per request basis, respectively.

During development, it can be useful to enable the most verbose debug information, to help identify and troubleshoot problems, or simply ensure that the client is behaving as expected. The `EnableDebugMode` setting on `ConnectionSettings` is a convenient shorthand for enabling verbose debug information, configuring a number of settings like

* disabling direct streaming to capture request and response bytes
* prettyfying JSON responses from Elasticsearch
* collecting TCP statistics when a request is made
* collecting thread pool statistics when a request is made
* including the Elasticsearch stack trace in the response if there is a an error on the server side

```csharp
IConnectionPool pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

var settings = new ConnectionSettings(pool)
    .EnableDebugMode(); <1>

var client = new ElasticClient(settings);

var response = client.Search<Project>(s => s
    .Query(q => q
        .MatchAll()
    )
);

var debugInformation = response.DebugInformation; <2>
```

1. configure debug mode
2. verbose debug information


In addition to exposing debug information on the response, debug mode will also cause the debug information to be written to the trace listeners in the `System.Diagnostics.Debug.Listeners` collection by default, when the request has completed. A delegate can be passed when enabling debug mode to perform a different action when a request has completed, using [`OnRequestCompleted`](logging-with-onrequestcompleted.md)

```csharp
var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
var client = new ElasticClient(new ConnectionSettings(pool)
    .EnableDebugMode(apiCallDetails =>
    {
        // do something with the call details e.g. send with logging framework
    })
);
```

