---
template: layout.jade
title: Connecting
menusection: concepts
menuitem: connecting
---


# Connecting
This section describes how to instantiate a client and have it connect to the server.

## Choosing the right connection strategy

`NEST` follows pretty much the same design as `Elasticsearch.Net` when it comes to choosing 
the right [connection strategy](/elasticsearch-net/connecting.html).

    new ElasticClient();

will create a non failover client that talks to `http://localhost:9200`.

    var uri = new Uri("http://mynode.somewhere.com/");
    var settings = new ConnectionSettings(uri, defaultIndex: "my-application");

This will create a non-failover client that talks with `http://mynode.somewhere.com` and uses the default index name `my-application`
 for calls which do not explicitly state an index name. Specifying a default index is optional but [very handy](/nest/type-index-inference).

If you want a failover client, instead of passing a `Uri`, pass an `IConnectionPool`. See the [Elasticsearch.Net documentation on cluster failover](/elasticsearch-net/cluster-failover.html). All of its implementations can also be used with NEST.



## Changing the underlying connection 

By default NEST will use HTTP to chat with Elasticsearch, alternative implementation of the transport layer can be injected using the constructors optional second parameter:

	var client = new ElasticClient(settings, new ThriftConnection(settings));

NEST comes with an Http Connection `HttpConnection`, Thrift Connection `ThriftConnection` 
and an In-Memory Connection `InMemoryConnection`, that nevers hits Elasticsearch.

You can also roll your own connection if desired by implementing the `IConnection` interface.

## Subclassing existing connection implementations

In addition to implementing your own `IConnection`, the existing `HttpConnection` and `ThriftConnection` are extendible and can be subclassed in order to modify or extend their behavior.

For instance, a common use case is the ability to add client certificates to web requests.  You can subclass `HttpConnection` and override the `CreateHttpWebRequest` method that creates the web request, and add certificates to it like so:

    public class SignedHttpConnection : HttpConnection
    {
        private readonly X509CertificateCollection _certificates;

        public SignedHttpConnection(IConnectionConfigurationValues settings, X509CertificateCollection certificates)
            : base(settings)
        {
            _certificates = certificates;
        }

        protected override HttpWebRequest CreateHttpWebRequest(Uri uri, string method, byte[] data, IRequestConfiguration requestSpecificConfig)
        {
            var request = base.CreateHttpWebRequest(uri, method, data, requestSpecificConfig);
            request.ClientCertificates = _certificates;
            return request;
        }
    }

## Settings

The `NEST` client can be configured by passing in an `IConnectionSettingsValues` object, which is a sub-interface of 
`Elasticsearch.Net`'s `IConnectionConfigurationValues`.  Therefore all of the settings that can be used to 
[configure Elasticsearch.Net](/elasticsearch-net/connecting.html) also apply here, including the 
[cluster failover settings](/elasticsearch-net/cluster-failover.html)

The easiest way to pass `IConnectionSettingsValues` is to instantiate `ConnectionSettings` is:

    var settings = new ConnectionSettings(
            myConnectionPool, 
            defaultIndex: "my-application"
        )
        .PluralizeTypeNames();

`IConnectionSettingsValues` has the following options:

### AddContractJsonConverters
Allows you to add a custom JsonConverter to the built-in JSON serialization by passing
in a predicate for a type.  This way they will be part of the cached Json.NET contract for a type.

    settings.AddContractJsonConverters(t => 
        typeof (Enum).IsAssignableFrom(t) 
            ? new StringEnumConverter() 
            : null);

### MapDefaultTypeIndices
Maps a CLR type to Elasticsearch indices. Takes precedence over `SetDefaultIndex`.

### MapDefaultTypeNames
Maps Elasticsearch type names to a CLR type. Takes priority over the global `SetDefaultTypeNameInferrer`.

### PluralizeTypeNames
This calls `SetDefaultTypenameInferrer` with an implementation that will pluralize
type names. This used to be the default prior to Nest 1.0.

### SetDefaultIndex
Sets the index to default to when no index is specified.

### SetDefaultPropertyNameInferrer
By default NEST camelCases property names (EmailAddress => emailAddress)
that do not have an explicit property name either via an `ElasticProperty` attribute
or because they are part of a dictionary where the keys should be treated verbatim.
Here you can register a function that transforms property names (default
casing, pre- or suffixing).

### SetDefaultTypeNameInferrer
Allows you to override how type names should be represented. The default will
call `.ToLowerInvariant()` on the type's name.

### SetJsonSerializerSettingsModifier
Allows you to update the internal Json.NET serializer settings to your liking.
Do not use this to add custom JSON converters use `AddContractJsonConverters` instead.

