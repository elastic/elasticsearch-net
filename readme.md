Repository for both **NEST** and **Elasticsearch.Net**, the two official [Elasticsearch](https://github.com/elastic/elasticsearch) .NET clients.

## Table of Contents

* [Compatibility Matrix](#compatibility-matrix)
* [Further Compatibility Clarifications](#further-compatibility-clarifications)
  * [Low Level Client Compatibility](#low-level-client-compatibility)
* [Preview builds](#preview-builds)
* [Upgrading](#upgrading)
* [NEST](#nest)
  * [Getting Started](#getting-started)
  * [Installing](#installing)
  * [Connecting](#connecting)
  * [Indexing](#indexing)
  * [Getting a Document](#getting-a-document)
  * [Searching For Documents](#searching-for-documents)
  * [Falling back to Elasticsearch.Net](#falling-back-to-elasticsearchnet)
* [Elasticsearch.Net](#elasticsearchnet)
  * [Installing](#installing-1)
  * [Connecting](#connecting-1)
  * [Calling an API endpoint](#calling-an-api-endpoint)
  * [Providing a request body](#providing-a-request-body)
* [Contributing](#contributing)
  * [Generating documentation from tests](#generating-documentation-from-tests)
  * [Many thanks to](#many-thanks-to)
* [Copyright and License](#copyright-and-license)

## Compatibility Matrix

| .NET Clients      | Elasticsearch | Supported          | Windows/Linux CI   | Tests              |
| ----------------- | ------------- | ------------------ | ------------------ | ------------------ |
| 0.x               | 0.x           | :x:                | :heavy_minus_sign: | :heavy_minus_sign: |
| 1.x               | 1.x           | :x:                | :heavy_minus_sign: | :heavy_minus_sign: |
| 2.x               | 2.x           | :x:                | :heavy_minus_sign: | :heavy_minus_sign: |
| 5.x               | 5.x           | :x:                | :heavy_minus_sign: | :heavy_minus_sign: |
| 6.x               | 6.x           | :white_check_mark: | [![Build status](https://ci.appveyor.com/api/projects/status/9hiqkga2jjn05ftu/branch/6.x?svg=true)](https://ci.appveyor.com/project/elastic/elasticsearch-net/branch/6.x) | :heavy_minus_sign: |
| 7.x               | 7.x           | :white_check_mark: | [![Integration](https://github.com/elastic/elasticsearch-net/actions/workflows/integration-jobs.yml/badge.svg?branch=7.x)](https://github.com/elastic/elasticsearch-net/actions/workflows/integration-jobs.yml) | [![Tests](https://github.com/elastic/elasticsearch-net/actions/workflows/test-jobs.yml/badge.svg?branch=7.x)](https://github.com/elastic/elasticsearch-net/actions/workflows/test-jobs.yml) |
| master            | master        | :x:                | [![Integration](https://github.com/elastic/elasticsearch-net/actions/workflows/integration-jobs.yml/badge.svg)](https://github.com/elastic/elasticsearch-net/actions/workflows/integration-jobs.yml) | [![Tests](https://github.com/elastic/elasticsearch-net/actions/workflows/test-jobs.yml/badge.svg)](https://github.com/elastic/elasticsearch-net/actions/workflows/test-jobs.yml) |

Please refer to the [end-of-life policy](https://www.elastic.co/support/eol) for complete information.

## Further Compatibility Clarifications

#### Can I use a `7.0.0` client against a `5.6` server (new against old), or a `6.0` client against a `7.2` server (old against new)?

No, this is not recommended.

No compatibility assurances are given between different major versions of the client and server. Major differences likely exist between major versions of the server, particularly around request and response object formats.

#### Can I use a `7.0.0` client against a `7.2.0` server?

Both the server and the client are developed according to [SemVer](https://semver.org/) principles, so there should be no breaking changes in a minor version. A `7.0.0` client will work against a `7.2.0` server.

This can be illustrated with some examples;

*Using a `7.0.0` client against a `7.0.0` server*. There is client support for all features in the server. In some instances, we may decide to delay implementing new APIs or some server features until later minor versions of the client, but this is the exception and not the rule.

*Using a `7.0.0` client against a `7.0.1` - `7.2.0` server*. There is client support for all comparable features that are available in the `7.0.0` server. If you want to use new features introduced in `7.2.0` with a lower version client, then you will have to use the low level client `DoRequest` method and perform the request/response (de)serialisation yourself.

When we release a client we will run the unit and integration tests against the latest minor patch version of Elasticsearch that matches the major.minor of the client release, and then any other latest minors within that major version.

Any incompatibilities between minor versions are documented against the release.

#### I have a `6.0` server, what client should I use?

Always use the latest minor version of the client within that major version, so in this instance, at time of writing, this is version `6.8.x`. The reason being is that `6.8.x` will contain many bug fixes not present in the `6.0.0` version of the client.

### Low Level Client Compatibility

The Elasticsearch.Net low level client will expose functionality in REST APIs that are marked as either `experimental` or `beta`. This functionality is marked as such using the `<remarks/>` XML documentation comments, which contains the stability and a description of the compatibility guarantees. Examples below:

> /// <remarks>Note: Experimental within the Elasticsearch server, this functionality is experimental and may be changed or removed completely in a future release. Elastic will take a best effort approach to fix any issues, but experimental features are not subject to the support SLA of official GA features. This functionality is subject to potential breaking changes within a minor version, meaning that your referencing code may break when this library is upgraded.</remarks>

> /// <remarks>Note: Beta within the Elasticsearch server, this functionality is in beta and is subject to change. The design and code is less mature than official GA features and is being provided as-is with no warranties. Beta features are not subject to the support SLA of official GA features. This functionality is subject to potential breaking changes within a minor version, meaning that your referencing code may break when this library is upgraded.</remarks>

If you use this `experimental` or `beta` functionality, by taking a dependency on it within your code, you are exposing yourself to the potential for binary breaking changes within that major version.

#### Branch Compatibility

- `master` reflects the latest server version, this is typically the `current latest major + 1`
- `N.x` where N represents the major version component of the Elasticsearch server release its integrating with; e.g. `7.x`
- `N.Y` where `N` is the major version and `Y` is the minor component, typically opened as integration branch for a specific minor leaving `N.x` free to do bug fixes.

## Preview builds

All branches push new nuget packages on successful CI builds to https://ci.appveyor.com/nuget/elasticsearch-net
          
### [Full documentation at https://www.elastic.co/guide/en/elasticsearch/client/net-api/current](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/index.html) 

## Upgrading

Please consult the [current upgrading Elasticsearch guidelines](https://www.elastic.co/guide/en/elasticsearch/reference/current/setup-upgrade.html) to understand what you should consider when upgrading from an older version of Elasticsearch to a newer one.

### Upgrading from 1.x to 2.x

Take a look at the [blog post for details around the evolution of NEST 2.x](https://www.elastic.co/blog/ga-release-of-nest-2-0-our-dot-net-client-for-elasticsearch), in addition to the list of breaking changes for [NEST](https://github.com/elastic/elasticsearch-net/blob/master/docs/2.0-breaking-changes/nest-breaking-changes.md) and [Elasticsearch.Net](https://github.com/elastic/elasticsearch-net/blob/master/docs/2.0-breaking-changes/elasticsearch-net-breaking-changes.md).

### Upgrading from 2.x to 5.x

Take a look at the [blog post for the release of NEST 5.x](https://www.elastic.co/blog/nest-5-0-released), in addition to the list of breaking changes for [NEST](https://github.com/elastic/elasticsearch-net/blob/master/docs/5.0-breaking-changes/nest-breaking-changes.md) and [Elasticsearch.Net](https://github.com/elastic/elasticsearch-net/blob/master/docs/5.0-breaking-changes/elasticsearch-net-breaking-changes.md).

### Upgrading from 5.x to 6.x

Take a look at the [blog post for the GA release of Elasticsearch.Net and NEST 6.0](https://www.elastic.co/blog/nest-elasticsearch-net-6-0-ga), in addition to the list of breaking changes for [NEST](https://www.elastic.co/guide/en/elasticsearch/client/net-api/6.x/nest-breaking-changes.html) and [Elasticsearch.Net](https://www.elastic.co/guide/en/elasticsearch/client/net-api/6.x/elasticsearch-net-breaking-changes.html).

### Upgrading from 6.x to 7.x

Take a look at the [blog post for the GA release of Elasticsearch.Net and NEST 7.0](https://www.elastic.co/blog/nest-and-elasticsearch-net-7-0-now-ga). Please also see the [7.0.0 release notes](https://github.com/elastic/elasticsearch-net/releases/tag/7.0.0).

# [NEST](https://github.com/elastic/elasticsearch-net/tree/master/src/Nest)

NEST is the official high-level .NET client of [Elasticsearch](https://github.com/elastic/elasticsearch).

It aims to be a solid, strongly typed client with a very concise API. The client internally uses the low-level **Elasticsearch.Net** client. It maps requests and responses to strongly-typed objects with both fluent interface and object initializer syntax. It also provides a very powerful query DSL that maps 1-to-1 with the Elasticsearch API. This client takes advantage of .NET features where they make sense (e.g. type and index inference and inferred mapping from POCO properties). All client method calls have asynchronous variants with support for cancellation.

## Getting Started

For a comprehensive, walkthrough-styled tutorial, check out the [NuSearch example repository](https://github.com/elastic/elasticsearch-net-example).

### Installing

You can install NEST from the package manager console:

	PM> Install-Package NEST

Alternatively, simply search for `NEST` in the package manager UI.

### Connecting

You can connect to your Elasticsearch cluster via a single node, or by specifying multiple nodes using a connection pool.  Using a connection pool has a few advantages over a single node connection, such as load balancing and cluster failover support.

**Connecting to a single node**

```csharp
var node = new Uri("http://myserver:9200");
var settings = new ConnectionSettings(node);
var client = new ElasticClient(settings);
```

**Connecting to multiple nodes using a connection pool**

```csharp
var nodes = new Uri[]
{
	new Uri("http://myserver1:9200"),
	new Uri("http://myserver2:9200"),
	new Uri("http://myserver3:9200")
};

var pool = new StaticConnectionPool(nodes);
var settings = new ConnectionSettings(pool);
var client = new ElasticClient(settings);
```

### Indexing

Indexing a document is as simple as:

```csharp
var tweet = new Tweet
{
    Id = 1,
    User = "kimchy",
    PostDate = new DateTime(2009, 11, 15),
    Message = "Trying out NEST, so far so good?"
};

var response = client.Index(tweet, idx => idx.Index("mytweetindex")); //or specify index via settings.DefaultIndex("mytweetindex");
```

All the calls have async variants:

```csharp
var response = client.IndexAsync(tweet, idx => idx.Index("mytweetindex")); // returns a Task<IndexResponse>

// Or, in an async-context
var response = await client.IndexAsync(tweet, idx => idx.Index("mytweetindex")); // awaits a Task<IndexResponse>
```

### Getting a document

```csharp
var response = client.Get<Tweet>(1, idx => idx.Index("mytweetindex")); // returns an IGetResponse mapped 1-to-1 with the Elasticsearch JSON response
var tweet = response.Source; // the original document
```

### Searching for documents

NEST exposes a fluent interface and a [powerful query DSL](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/query-dsl.html)

```csharp
var response = client.Search<Tweet>(s => s
    .Index("mytweetindex") //or specify index via settings.DefaultIndex("mytweetindex");
    .From(0)
    .Size(10)
    .Query(q => q
        .Term(t => t.User, "kimchy") || q
        .Match(mq => mq.Field(f => f.User).Query("nest"))
    )
);
```

As well as an object initializer syntax if lambdas aren't your thing:

```csharp
var request = new SearchRequest
{
    Index = "mytweetindex", //or specify index via settings.DefaultIndex("mytweetindex"),
    From = 0,
    Size = 10,
    Query = new TermQuery { Field = "user", Value = "kimchy" } || 
            new MatchQuery { Field = "description", Query = "nest" }
};

var response = client.Search<Tweet>(request);
```

### Falling back to Elasticsearch.Net

NEST also includes and exposes the low-level [Elasticsearch.Net](https://github.com/elastic/elasticsearch-net/tree/master/src/Elasticsearch.Net) client that you can fall back to in case anything is missing:

```csharp
//.LowLevel is of type IElasticLowLevelClient
// Generic parameter of Search<> is the type of .Body on response
var response = client.LowLevel.Search<SearchResponse<Tweet>>("myindex", PostData.Serializable(new
{
	from = 0,
	size = 10,
	fields = new [] {"id", "name"},
	query = new {
		term = new {
			name = new {
				value= "NEST",
				boost = 2.0
			}
		}
	}
}));
```

#### [Full documentation at https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/nest.html](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/nest.html) 

# [Elasticsearch.Net](src/Elasticsearch.Net)

A low-level, dependency free client that has no opinions how you build and represent your requests and responses.

It provides a one-to-one mapping with the Elasticsearch REST API. The client is almost completely generated from the official REST API specification, which makes it easy to keep up-to-date. The client also has support for load balancing and cluster failover and all client method calls have both synchronous and asynchronous variants

### Installing

You can install Elasticsearch.Net from the package manager console:

	PM> Install-Package Elasticsearch.Net

Alternatively,  search for `Elasticsearch.Net` in the package manager UI.

### Connecting

Connecting using the low-level client is very similar to how you would connect using NEST.  In fact, the connection constructs that NEST use are actually Elasticsearch.Net constructs.  Thus, single node connections and connection pooling still apply when using Elasticsearch.Net.

```csharp
var node = new Uri("http://myserver:9200");
var config = new ConnectionConfiguration(node);
var client = new ElasticLowLevelClient(config);
```

Note the main difference here is that we are instantiating an `ElasticLowLevelClient` rather than `ElasticClient`, and `ConnectionConfiguration` instead of `ConnectionSettings`.

### Calling an API endpoint

Elasticsearch.Net is generated from the [official REST specification](https://github.com/elastic/elasticsearch/tree/master/rest-api-spec), and thus maps to all Elasticsearch API endpoints.

```csharp
client.GetSource("myindex","mytype","1",qs=>qs
    .Routing("routingvalue")
);
```

This will execute a `GET` to `/myindex/mytype/1/_source?routing=routingvalue`. All the methods and arguments are fully documented based on the documentation of the specification.

As you can see, Elasticsearch.Net also strongly types the query string parameters that it knows exist on an endpoint with full Intellisense documentation. However, unknown query string parameters can still be added:

```csharp
client.Source<StringResponse>("myindex", "1", new SourceRequestParameters
{
    Routing = "routingvalue",
    QueryString =
    {
        { "key", "value" }
    }
});
```

The query string parameter is always optional.

### Providing a request body

You can specify a request body directly with a string:

```csharp
var myJson = @"{ ""hello"" : ""world"" }";
client.Index<StringResponse>("myindex", "1", myJson);
```

This will execute a `POST` to `/myindex/mytype/1` with the provided string `myJson` passed verbatim as the request body.

Alternatively, you can specify an anonymous object:

```csharp
var myJson = new { hello = "world" };
client.Index<BytesResponse>("myindex", "1", PostData.Serializable(myJson));
```
This will execute the same request, but this time `myJson` will be serialized by the registered `IElasticsearchSerializer`.

#### [Full documentation at https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/elasticsearch-net.html](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/elasticsearch-net.html) 

## Contributing

[Pull requests](https://github.com/elastic/elasticsearch-net/pulls) and [issues](https://github.com/elastic/elasticsearch-net/issues) are very much welcomed and appreciated.  If you'd like to report a bug or submit a feature/bug fix then please read our [contributing guide](contributing.md) first!

### Generating documentation from tests

[All Elasticsearch.Net and NEST documentation on elastic.co](https://www.elastic.co/guide/en/elasticsearch/client/net-api/index.html) is generated from code within the [Tests project](src/Tests) using [Roslyn](https://github.com/dotnet/roslyn); multi-line comments serve as the main bodies of text, intermixed with code samples that test the documented components. The intention is to reduce the likelihood of documentation becoming outdated as the source changes. 

Text within multi-line comments conforms to [asciidoc](http://asciidoc.org/), a lightweight markdown style text format well suited to technical documentation. To generate the asciidoc files from the test files, you need to run the [DocGenerator](src/DocGenerator) console application which will output the documentation files in the docs output directory. To verify that the generated asciidoc files can generate the documentation for the website, [clone the elastic docs repo](https://github.com/elastic/docs) and follow the instructions there for building documentation locally. As an example, suppose I have cloned the elastic docs to `c:\source\elastic-docs`, then to verify the generated asciidoc files for NEST are valid would be as follows (using Cygwin on Windows):

```sh
cd /cygdrive/c/source/elastic-docs

./build_docs.pl --doc /cygdrive/c/source/elasticsearch-net-master/docs/index.asciidoc --chunk=1 --open
```

The result of running this for a successful build will be:

```sh
Building HTML from /cygdrive/c/source/elasticsearch-net-master/docs/index.asciidoc
Done
See: /cygdrive/c/source/elasticsearch-docs/html_docs/index.html
```

A small HTTP server will be spun up locally on port 8000 through which you can view the documentation.

[Pull Requests](https://github.com/elastic/elasticsearch-net/pulls) are most welcome for areas of documentation that need improving.

#### Many thanks to:
* [Q42](https://q42.nl/) for supporting the development of NEST
* [redgate](http://www.red-gate.com) for supplying @Mpdreamz with an ANTS Memory Profiler 8 & ANTS Performance Profiler 8 licenses
* [jetBrains](http://www.jetbrains.com) for supplying @Mpdreamz with a dotTrace profiler and Resharper license
* [CodeBetter](http://codebetter.com) for hosting the continuous integration for NEST
* Everyone who has been awesome enough to contribute back to NEST (You're listed automatically on the [documentation page](https://github.com/elastic/elasticsearch-net/graphs/contributors))

## Copyright and License

This software is Copyright (c) 2014-2019 by Elasticsearch BV.

This is free software, licensed under: [The Apache License Version 2.0](https://github.com/elastic/elasticsearch-net/blob/master/license.txt).
