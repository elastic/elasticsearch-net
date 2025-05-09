---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/transport.html
---

# Transport example [transport]

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

// ...

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

