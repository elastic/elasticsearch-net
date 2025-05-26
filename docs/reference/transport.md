---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/transport.html
---

# Low level Transport example [low-level-transport]

This page demonstrates how to use the low level transport to send requests.

```csharp
public class MyRequestParameters : RequestParameters
{
    public bool Pretty
    {
        get => Q<bool>("pretty");
        init => Q("pretty", value);
    }
}
```

```csharp
using Elastic.Transport;

var body = """
           {
             "name": "my-api-key",
             "expiration": "1d",
             "...": "..."
           }
           """;

MyRequestParameters requestParameters = new()
{
    Pretty = true
};

var pathAndQuery = requestParameters.CreatePathWithQueryStrings("/_security/api_key",
    client.ElasticsearchClientSettings);
var endpointPath = new EndpointPath(Elastic.Transport.HttpMethod.POST, pathAndQuery);

// Or, if the path does not contain query parameters:
// new EndpointPath(Elastic.Transport.HttpMethod.POST, "my_path")

var response = await client.Transport
    .RequestAsync<StringResponse>(
        endpointPath,
        PostData.String(body),
        null,
        null,
        cancellationToken: default)
    .ConfigureAwait(false);
```

# `OnBeforeRequest` example [on-before-request]

The `OnBeforeRequest` callback in `IElasticsearchClientSettings` can be used to dynamically modify requests.

```csharp
var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200))
    .OnBeforeRequest(OnBeforeRequest); <1>

RequestConfiguration? globalRequestConfiguration = null;
ConditionalWeakTable<RequestConfiguration, RequestConfiguration>? globalRequestConfigurations = null;

void OnBeforeRequest(ElasticsearchClient client, Request request, EndpointPath endpointPath, ref PostData? postData, ref IRequestConfiguration? requestConfiguration)
{
    // Each time a request is made, the transport creates a new `BoundConfiguration` for every `IRequestConfiguration`
    // that is not in the cache (based on reference equality).

    // To prevent frequent allocations of our mutated request configurations (and the secondary allocations for
    // `BoundConfiguration`), we have to maintain a custom cache that maps every original request configuration to the
    // mutated one.
    
    if (requestConfiguration is null)
    {
        globalRequestConfiguration = Interlocked.CompareExchange(
            ref globalRequestConfiguration,
            new RequestConfiguration
            {
                UserAgent = UserAgent.Create("my-custom-user-agent")
            },
            null) ?? globalRequestConfiguration;

        requestConfiguration = globalRequestConfiguration;
        return;
    }

    if (requestConfiguration is not RequestConfiguration rc)
    {
        // Only `RequestConfiguration` (not all implementations of `IRequestConfiguration`) gets cached in the
        // internal cache.
        requestConfiguration = MutateRequestConfiguration(requestConfiguration);
        return;
    }

    // ReSharper disable InconsistentlySynchronizedField

    var cache = (Interlocked.CompareExchange(
        ref globalRequestConfigurations,
        new ConditionalWeakTable<RequestConfiguration, RequestConfiguration>(),
        null
    ) ?? globalRequestConfigurations);

    if (cache.TryGetValue(rc, out var mutatedRequestConfiguration))
    {
        requestConfiguration = mutatedRequestConfiguration;
        return;
    }

    mutatedRequestConfiguration = MutateRequestConfiguration(rc);

#if NET8_0_OR_GREATER
    cache.TryAdd(rc, mutatedRequestConfiguration);
#else
    lock (cache)
    {
        cache.Add(rc, mutatedRequestConfiguration);
    }
#endif

    // ReSharper restore InconsistentlySynchronizedField

    return;

    RequestConfiguration MutateRequestConfiguration(IRequestConfiguration requestConfiguration)
    {
        return new RequestConfiguration(requestConfiguration)
        {
            UserAgent = UserAgent.Create("my-custom-user-agent")
        };
    }
}
```

1. Register the `OnBeforeRequest` callback.
