---
template: layout.jade
title: Connecting
menusection: 
menuitem: esnet-connecting
---

# Connecting

Connecting to `Elasticsearch` with `Elasticsearch.net` is quite easy but has a few toggles and options worth knowing

## Choosing the right connection strategy

If you simply new an `ElasticsearchClient` it will be a non failover connection to `http://localhost:9200`

    var client = new ElasticsearchClient();

If your elasticsearch node does not live at `http://localhost:9200` but i.e `http://mynode.example.com:8082/apiKey` 
you will need to pass in some `IConnectionConfigurationValues` the easiest way to do this is:

    var node = new Uri("http://mynode.example.com:8082/apiKey");
    var config = new ConnectionConfiguration(node);
    var client = new ElasticsearchClient(config);

This however is still a non failover connection. Meaning if that `node` goes down the operation will not be retried on any other nodes in the cluster.
To get a failover connection we have to pass an `IConnectionPool` instead of a `Uri`.

    var node = new Uri("http://mynode.example.com:8082/apiKey");
    var connectionPool = new SniffingConnectionPool(new[] { node });
    var config = new ConnectionConfiguration(connectionPool);
    var client = new ElasticsearchClient(config);

Here instead of directly passing `node` we pass a `SniffingConnectionPool` which will use our `node` to find out the rest of the available cluster nodes.
Be sure to read more about [Connection Pooling and Cluster Failover here](/elasticsearch-net/cluster-failover.html)


## Options

Besides either passing a `Uri` or `IConnectionPool` on the constructor of `ConnectionConfiguration` you can also fluently control many more options.

    var config = new ConnectionConfiguration(connectionPool)
        .EnableTrace()
        .ExposeRawResponse(shouldExposeRawResponse);

#### EnableTrace()
Will cause `Elasticsearch.Net` to write connection debug information on the TRACE output of your application

#### ExposeRawResponse()
By default responses are deserialized off stream to the object you tell it too. For debugging purposes it can be very usefull to keep a copy off the raw 
response on the result object. 


    var result = client.Search<MySearchObject>(....);
    var obj = result.Response;
    //This will only have a value if the client configuration
    //has ExposeRawResponse set
    var raw = result.ResponseRaw;

Please not that this only make sense if you need a mapped response and the raw response at the same time. If you need a `string` or `byte[]` response simply call:

    var result = client.Search<string>(...);

#### SetConnectionStatusHandler
Allows you to pass a `Action<IElasticsearchResponse>` that can eaves drop every time a response (good or bad) is created. If you have complex logging needs 
this is a good place to add that in.

#### SetGlobalQueryStringParameters
Allows you to set querystring parameters that have to be added to every request. i.e you use a hosted elasticserch provider and you need need to pass an `apiKey` parameter onto everyrequest

#### SetMaximumAsyncConnections
The default `HttpConnection` is unbounded in how many async connection it will start. Due note that the HttpConnection does not use any threads. If you need to throttle this you can set this setting. For other `IConnection` implemenations it can also be a hint to how many persistant connections it should hold, i.e the `ThriftConnection` listens to this.

#### SetProxy()
Sets proxy information on the connection

#### SetTimeout()
Sets the global maximum time a connection may take. Please not that this is the request timeout, the builtin .net webrequest has no way to set connect timeouts. See http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.timeout(v=vs.110).aspx

#### UsePrettyResponses()
Appends `pretty=true` to all the requests handy if you are debugging or listening to the requests with i.e fiddler. Note that this settings can be safely used in conjuction with `SetGlobalQueryStringParameters()`
