---
template: layout.jade
title: Connecting
menusection: concepts
menuitem: connecting
---


# Connecting
This section describes how to instantiate a client and have it connect to the server.

## Basic plumbing:

	var uri = new Uri("http://localhost:9200");
	var settings = new ConnectionSettings(uri)
		.SetDefaultIndex("mydefaultindex");
	var client = new ElasticClient(settings);

`ConnectionSettings`'s constructor has many overloads, including support for connecting through proxies.

## Connecting

Connecting can be done several ways:

	ConnectionStatus connectionStatus;
	if (client.TryConnect(out connectionStatus))

Or if you don't care about error reasons

	if (client.IsValid)

both will perform a one time lookup to see if ElasticSearch is available and ready by doing a request to `/` on the elasticsearch server. 

## Changing the underlying connection 

By default NEST will use HTTP to chat with elasticsearch, alternative implementation of the transport layer can be injected in the constructors optional second parameter

	var client = new ElasticClient(settings, new ThriftConnection(settings));

Nest comes with a Htpp connection `Connection`, Thrift Connection `ThriftConnection` and an in memory connection that nevers hits elasticsearch `InMemoryConnection`.

## Settings:
Settings can be set in a fluent fashion: `new ConnectionSettings().SetDefaultIndex().SetMaximumConnections()`

### DefaultIndex
Calling `SetDefaultIndex()` on `ConnectionSettings` will set the default index for the client. Whenever a method is called that doesn't explicitly passes an index this default will be used.

### MaximumAsyncConnections
Calling `SetMaximumAsyncConnections()` on `ConnectionSettings` will set the maximum async connections the client will send to ElasticSearch at the same time. If the maximum is hit the calls will be queued untill a slot becomes available.

### TypeNameInferrer
You can pass a `Func<string,string>` to `SetTypeNameInferrer()` on `ConnectionSettings` to overide NEST's default behavior of lowercasing and pluralizing typenames.

### UsePrettyResponses
Setting `UsePrettyResponses()` on `ConnectionSettings` will append `pretty=true` to all the requests to inform ElasticSearch we want nicely formatted responses, setting this does **not** prettify requests themselves because bulk requests in ElasticSearch follow a very exact line delimited format. 

### MapTypeIndices
Allows you to globally set the default type name for a type.

    .MapTypeIndices(s=>s
        .Add(typeof(MyType), "mytupo")
        .Add(typeof(YoutubeMovie), "mov")
    );
