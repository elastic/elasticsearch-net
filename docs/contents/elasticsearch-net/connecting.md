---
template: layout.jade
title: Connecting
menusection: 
menuitem: esnet-connecting
---

# Connecting

Connecting to Elasticsearch with `Elasticsearch.Net` is quite easy but has a few toggles and options worth knowing.

## Choosing the right connection strategy

If you simply new an `ElasticsearchClient`, it will be a non-failover connection to `http://localhost:9200`

    var client = new ElasticsearchClient();

If your Elasticsearch node does not live at `http://localhost:9200` but i.e `http://mynode.example.com:8082/apiKey`, then 
you will need to pass in some instance of `IConnectionConfigurationValues`.

The easiest way to do this is:

    var node = new Uri("http://mynode.example.com:8082/apiKey");
    var config = new ConnectionConfiguration(node);
    var client = new ElasticsearchClient(config);

This however is still a non-failover connection. Meaning if that `node` goes down the operation will not be retried on any other nodes in the cluster.

To get a failover connection we have to pass an `IConnectionPool` instance instead of a `Uri`.

    var node = new Uri("http://mynode.example.com:8082/apiKey");
    var connectionPool = new SniffingConnectionPool(new[] { node });
    var config = new ConnectionConfiguration(connectionPool);
    var client = new ElasticsearchClient(config);

Here instead of directly passing `node`, we pass a `SniffingConnectionPool` which will use our `node` to find out the rest of the available cluster nodes.
Be sure to read more about [Connection Pooling and Cluster Failover here](/elasticsearch-net/cluster-failover.html)

## Options

Besides either passing a `Uri` or `IConnectionPool` to `ConnectionConfiguration`, you can also fluently control many more options. For instance:

    var config = new ConnectionConfiguration(connectionPool)
        .EnableTrace()
        .ExposeRawResponse()
        .SetBasicAuthentication("user", "pass")
        .SetTimeout(5000)
        ...

The following is a list of available connection configuration options:

### DisableAutomaticProxyDetection
Disable automatic proxy detection.  Defaults to true.

### EnableCompressedResponses
Enable compressed responses from Elasticsearch (Note that nodes need to be configured to allow this.  See the [http module settings](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-http.html) for more info).

### EnableMetrics
Enable more meta data to be returned per API call about requests (ping, sniff, failover, and general stats).

### EnableTrace
Will cause `Elasticsearch.Net` to write connection debug information on the TRACE output of your application.

### ExposeRawResponse
By default responses are deserialized off stream to the object you tell it to. For debugging purposes it can be very useful to keep a copy of the raw response on the result object. 

    var result = client.Search<MySearchObject>(....);
    var obj = result.Response;
    //This will only have a value if the client configuration
    //has ExposeRawResponse set
    var raw = result.ResponseRaw;

Please note that this only make sense if you need a mapped response and the raw response at the same time. If you need a `string` or `byte[]` response simply call:

    var result = client.Search<string>(...);

### SetConnectionStatusHandler
Allows you to pass a `Action<IElasticsearchResponse>` that can eaves drop every time a response (good or bad) is created. If you have complex logging needs 
this is a good place to add that in.

### SetGlobalQueryStringParameters
Allows you to set querystring parameters that have to be added to every request. For instance, if you use a hosted elasticserch provider, and you need need to pass an `apiKey` parameter onto every request.

### SetMaximumAsyncConnections
The default `HttpConnection` is unbounded in how many async connection it will start. Do note that the HttpConnection does not use any threads. If you need to throttle this you can use this setting. For other `IConnection` implemenations, it can also be a hint to how many persistant connections it should hold, i.e the `ThriftConnection` listens to this.

### SetProxy
Sets proxy information on the connection.

### SetTimeout
Sets the global maximum time a connection may take. Please note that this is the request timeout, the builtin .NET `WebRequest` has no way to set connection timeouts (see http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.timeout(v=vs.110).aspx).

### ThrowOnElasticsearchServerExceptions
As an alternative to the C/go like error checking on `response.IsValid`, you can instead tell the client to always throw an `ElasticsearchServerException` when a call resulted in an exception on the Elasticsearch server. Reasons for such exceptions could be search parser errors and index missing exceptions.

### UsePrettyResponses
Appends `pretty=true` to all the requests. Handy if you are debugging or listening to the requests with i.e fiddler. This setting can be safely used in conjuction with `SetGlobalQueryStringParameters`.

### SetBasicAuthentication
Sets the HTTP basic authentication credentials to specify with all requests.

**Note:** This can alternatively be specified on the node URI directly:

    var uri = new Uri("http://username:password@localhost:9200");
    var config = new ConnectionConfiguration(uri);

...but may become tedious when using connection pooling with multiple nodes.

## Configuring SSL

SSL must be configured outside of the client using .NET's [ServicePointManager](http://msdn.microsoft.com/en-us/library/system.net.servicepointmanager%28v=vs.110%29.aspx
) class and setting the [ServerCertificateValidationCallback](http://msdn.microsoft.com/en-us/library/system.net.servicepointmanager.servercertificatevalidationcallback.aspx) property.

The bare minimum to make .NET accept self-signed SSL certs that are not in the Window's CA store would be to have the callback simply return `true`:

    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;

However, this will accept all requests from the AppDomain to untrusted SSL sites, therefore we recommend doing some minimal introspection on the passed in certificate.

			