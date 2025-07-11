---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/_options_on_elasticsearchclientsettings.html
---

# Options on ElasticsearchClientSettings [_options_on_elasticsearchclientsettings]

The following is a list of available connection configuration options on `ElasticsearchClientSettings`:

`Authentication`
:   An implementation of `IAuthenticationHeader` describing what http header to use to authenticate with the product.

    ```
    `BasicAuthentication` for basic authentication
    ```
    ```
    `ApiKey` for simple secret token
    ```
    ```
    `Base64ApiKey` for Elastic Cloud style encoded api keys
    ```

`ClientCertificate`
:   Use the following certificates to authenticate all HTTP requests. You can also set them on individual request using `ClientCertificates`.

`ClientCertificates`
:   Use the following certificates to authenticate all HTTP requests. You can also set them on individual request using `ClientCertificates`.

`ConnectionLimit`
:   Limits the number of concurrent connections that can be opened to an endpoint. Defaults to 80 (see `DefaultConnectionLimit`).

    For Desktop CLR, this setting applies to the `DefaultConnectionLimit` property on the `ServicePointManager` object when creating `ServicePoint` objects, affecting the default `IConnection` implementation.

    For Core CLR, this setting applies to the `MaxConnectionsPerServer` property on the `HttpClientHandler` instances used by the `HttpClient` inside the default `IConnection` implementation.

`DeadTimeout`
:   The time to put dead nodes out of rotation (this will be multiplied by the number of times they’ve been dead).

`DefaultDisableIdInference`
:   Disables automatic Id inference for given CLR types.

    The client by default will use the value of a property named `Id` on a CLR type as the `_id` to send to {{es}}. Adding a type will disable this behaviour for that CLR type. If `Id` inference should be disabled for all CLR types, use `DefaultDisableIdInference`.

`DefaultFieldNameInferrer`
:   Specifies how field names are inferred from CLR property names.

    By default, the client camel cases property names. For example, CLR property `EmailAddress` will be inferred as "emailAddress" {{es}} document field name.

`DefaultIndex`
:   The default index to use for a request when no index has been explicitly specified and no default indices are specified for the given CLR type specified for the request.

`DefaultMappingFor`
:   Specify how the mapping is inferred for a given CLR type. The mapping can infer the index, id and relation name for a given CLR type, as well as control serialization behaviour for CLR properties.

`DisableAutomaticProxyDetection`
:   Disabled proxy detection on the webrequest, in some cases this may speed up the first connection your appdomain makes, in other cases it will actually increase the time for the first connection. No silver bullet! Use with care!

`DisableDirectStreaming`
:   When set to true will disable (de)serializing directly to the request and response stream and return a byte[] copy of the raw request and response. Defaults to false.

`DisablePing`
:   This signals that we do not want to send initial pings to unknown/previously dead nodes and just send the call straightaway.

`DnsRefreshTimeout`
:   DnsRefreshTimeout for the connections. Defaults to 5 minutes.

`EnableDebugMode`
:   Turns on settings that aid in debugging like `DisableDirectStreaming()` and `PrettyJson()` so that the original request and response JSON can be inspected. It also always asks the server for the full stack trace on errors.

`EnableHttpCompression`
:   Enable gzip compressed requests and responses.

`EnableHttpPipelining`
:   Whether HTTP pipelining is enabled. The default is `true`.

`EnableTcpKeepAlive`
:   Sets the keep-alive option on a TCP connection.

    For Desktop CLR, sets `ServicePointManager`.`SetTcpKeepAlive`.

`EnableTcpStats`
:   Enable statistics about TCP connections to be collected when making a request.

`GlobalHeaders`
:   Try to send these headers for every request.

`GlobalQueryStringParameters`
:   Append these query string parameters automatically to every request.

`MaxDeadTimeout`
:   The maximum amount of time a node is allowed to marked dead.

`MaximumRetries`
:   When a retryable exception occurs or status code is returned this controls the maximum amount of times we should retry the call to {{es}}.

`MaxRetryTimeout`
:   Limits the total runtime including retries separately from `RequestTimeout`. When not specified defaults to `RequestTimeout` which itself defaults to 60 seconds.

`MemoryStreamFactory`
:   Provides a memory stream factory.

`NodePredicate`
:   Register a predicate to select which nodes that you want to execute API calls on. Note that sniffing requests omit this predicate and always execute on all nodes. When using an `IConnectionPool` implementation that supports reseeding of nodes, this will default to omitting master only node from regular API calls. When using static or single node connection pooling it is assumed the list of node you instantiate the client with should be taken verbatim.

`OnRequestCompleted`
:   Allows you to register a callback every time a an API call is returned.

`OnBeforeRequest`
:   An action to run before a request is made.

`PingTimeout`
:   The timeout in milliseconds to use for ping requests, which are issued to determine whether a node is alive.

`PrettyJson`
:   Provide hints to serializer and products to produce pretty, non minified json.

    Note: this is not a guarantee you will always get prettified json.

`Proxy`
:   If your connection has to go through proxy, use this method to specify the proxy url.

`RequestTimeout`
:   The timeout in milliseconds for each request to {{es}}.

`ServerCertificateValidationCallback`
:   Register a `ServerCertificateValidationCallback` per request.

`SkipDeserializationForStatusCodes`
:   Configure the client to skip deserialization of certain status codes, for example, you run {{es}} behind a proxy that returns an unexpected json format.

`SniffLifeSpan`
:   Force a new sniff for the cluster when the cluster state information is older than the specified timespan.

`SniffOnConnectionFault`
:   Force a new sniff for the cluster state every time a connection dies.

`SniffOnStartup`
:   Sniff the cluster state immediately on startup.

`ThrowExceptions`
:   Instead of following a c/go like error checking on response. `IsValid` do throw an exception (except when `SuccessOrKnownError` is false) on the client when a call resulted in an exception on either the client or the {{es}} server.

    Reasons for such exceptions could be search parser errors, index missing exceptions, and so on.

`TransferEncodingChunked`
:   Whether the request should be sent with chunked Transfer-Encoding.

`UserAgent`
:   The user agent string to send with requests. Useful for debugging purposes to understand client and framework versions that initiate requests to {{es}}.

## ElasticsearchClientSettings with ElasticsearchClient [_elasticsearchclientsettings_with_elasticsearchclient]

Here’s an example to demonstrate setting configuration options using the client.

```csharp
var settings= new ElasticsearchClientSettings()
    .DefaultMappingFor<Project>(i => i
        .IndexName("my-projects")
        .IdProperty(p => p.Name)
    )
    .EnableDebugMode()
    .PrettyJson()
    .RequestTimeout(TimeSpan.FromMinutes(2));

var client = new ElasticsearchClient(settings);
```
