---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/audit-trail.html
---

# Audit trail [audit-trail]

Elasticsearch.Net and NEST provide an audit trail for the events within the request pipeline that occur when a request is made. This audit trail is available on the response as demonstrated in the following example.

We’ll use a Sniffing connection pool here since it sniffs on startup and pings before first usage, so we can get an audit trail with a few events out

```csharp
var pool = new SniffingConnectionPool(new []{ TestConnectionSettings.CreateUri() });
var connectionSettings = new ConnectionSettings(pool)
    .DefaultMappingFor<Project>(i => i
        .IndexName("project")
    );

var client = new ElasticClient(connectionSettings);
```

After issuing the following request

```csharp
var response = client.Search<Project>(s => s
    .MatchAll()
);
```

The audit trail is provided in the [Debug information](debug-information.md) in a human readable fashion, similar to

```
Valid NEST response built from a successful low level call on POST: /project/doc/_search
# Audit trail of this API call:
 - [1] SniffOnStartup: Took: 00:00:00.0360264
 - [2] SniffSuccess: Node: http://localhost:9200/ Took: 00:00:00.0310228
 - [3] PingSuccess: Node: http://127.0.0.1:9200/ Took: 00:00:00.0115074
 - [4] HealthyResponse: Node: http://127.0.0.1:9200/ Took: 00:00:00.1477640
# Request:
<Request stream not captured or already read to completion by serializer. Set DisableDirectStreaming() on ConnectionSettings to force it to be set on the response.>
# Response:
<Response stream not captured or already read to completion by serializer. Set DisableDirectStreaming() on ConnectionSettings to force it to be set on the response.>
```
to help with troubleshootin

```csharp
var debug = response.DebugInformation;
```

But can also be accessed manually:

```csharp
response.ApiCall.AuditTrail.Count.Should().Be(4, "{0}", debug);
response.ApiCall.AuditTrail[0].Event.Should().Be(SniffOnStartup, "{0}", debug);
response.ApiCall.AuditTrail[1].Event.Should().Be(SniffSuccess, "{0}", debug);
response.ApiCall.AuditTrail[2].Event.Should().Be(PingSuccess, "{0}", debug);
response.ApiCall.AuditTrail[3].Event.Should().Be(HealthyResponse, "{0}", debug);
```

Each audit has a started and ended `DateTime` on it that will provide some understanding of how long it took

```csharp
response.ApiCall.AuditTrail
    .Should().OnlyContain(a => a.Ended - a.Started >= TimeSpan.Zero);
```

