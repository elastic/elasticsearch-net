Repository for both **NEST** and **Elasticsearch.Net**, the two official [Elasticsearch](https://github.com/elastic/elasticsearch) .NET clients.

## Compatibility Matrix
<table>
    <tr>
        <th><b>Elasticsearch<b></th>
        <th><b>Clients<b></th>
        <th><b>Supported<b></th>
        <th><b>Windows/Linux CI</b></th>
        <th><b>Tests<b></th>
    </tr>
    <tr>
    	<td><code>0.x</code></td>
    	<td><code>0.x</code></td>
    	<td>:x:</td>
    	<td>:heavy_minus_sign:</td>
    	<td>:heavy_minus_sign:</td>
    </tr>
    <tr>
    	<td><code>1.x</code></td>
    	<td><code>1.x</code></td>
    	<td>:x:</td>
    	<td>:heavy_minus_sign:</td>
    	<td>:heavy_minus_sign:</td>
    </tr>
    <tr>
    	<td><code>2.x</code></td>
    	<td><code>2.x</code></td>
    	<td>:x:</td>
    	<td>:heavy_minus_sign:</td>
    	<td>:heavy_minus_sign:</td>
    	</td>  
    </tr>
    <tr>
    	<td><code>5.x</code></td>
    	<td><code>5.x</code></td>
    	<td>:white_check_mark:</td>
      <td><a href="https://ci.appveyor.com/project/elastic/elasticsearch-net/branch/5.x"><img src="https://ci.appveyor.com/api/projects/status/github/elastic/elasticsearch-net?branch=5.x&svg=true"></a></td>
    	<td><a href="https://ci.appveyor.com/project/elastic/elasticsearch-net/branch/5.x/tests"><img alt="5.x unit tests" src="https://img.shields.io/appveyor/tests/elastic/elasticsearch-net/5.x.svg?style=flat-square"></a></td>
    </tr>
    <tr>
    	<td><code>6.x</code></td>
    	<td><code>6.x</code></td>
    	<td>:white_check_mark:</td>
      <td><a href="https://ci.appveyor.com/project/elastic/elasticsearch-net/branch/6.x"><img src="https://ci.appveyor.com/api/projects/status/github/elastic/elasticsearch-net?branch=6.x&svg=true"></a></td>
    	<td><a href="https://ci.appveyor.com/project/elastic/elasticsearch-net/branch/6.x/tests"><img alt="6.x unit tests" src="https://img.shields.io/appveyor/tests/elastic/elasticsearch-net/6.x.svg?style=flat-square"></a></td>
    </tr>
    <tr>
    	<td><code>7.x</code></td>
    	<td><code>7.x</code></td>
      <td>:white_check_mark:</td>
      <td><a href="https://ci.appveyor.com/project/elastic/elasticsearch-net/branch/7.x"><img src="https://ci.appveyor.com/api/projects/status/github/elastic/elasticsearch-net?branch=7.x&svg=true"></a></td>
    	<td><a href="https://ci.appveyor.com/project/elastic/elasticsearch-net/branch/7.x/tests"><img alt="7.x unit tests" src="https://img.shields.io/appveyor/tests/elastic/elasticsearch-net/7.x.svg?style=flat-square"></a></td>
    </tr>
    <tr>
    	<td><code>master</code></td>
    	<td><code>master</code></td>
    	<td>:x:</td>
      <td><a href="https://ci.appveyor.com/project/elastic/elasticsearch-net/branch/master"><img src="https://ci.appveyor.com/api/projects/status/github/elastic/elasticsearch-net?branch=master&svg=true"></a></td>
    	<td><a href="https://ci.appveyor.com/project/elastic/elasticsearch-net/branch/master/tests"><img alt="master unit tests" src="https://img.shields.io/appveyor/tests/elastic/elasticsearch-net/master.svg?style=flat-square"></a></td>
    </tr>
</table>

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

# [NEST](https://github.com/elasticsearch/elasticsearch-net/tree/master/src/Nest)

NEST is the official high-level .NET client of [Elasticsearch](https://github.com/elasticsearch/elasticsearch).  It aims to be a solid, strongly typed client with a very concise API.

* High-level client that internally uses the low-level **Elasticsearch.Net** client
* Maps requests and responses to strongly typed objects with both fluent interface and object initializer syntaxes to build them
* Comes with a very powerful query DSL that maps one-to-one with Elasticsearch
* Takes advantage of .NET features where they make sense (e.g. type and index inference, inferred mapping from POCO properties)
* All calls have async variants with support for cancellation

## Getting Started

For a comprehensive, walkthrough-styled tutorial, check out the [NuSearch example repository](https://github.com/elastic/elasticsearch-net-example).

### Installing

From the package manager console:

	PM> Install-Package NEST

or by simply searching for `NEST` in the package manager UI.

### Connecting

You can connect to your Elasticsearch cluster via a single node, or by specifying multiple nodes using a connection pool.  Using a connection pool has a few advantages over a single node connection, such as load balancing and cluster fail over support.

**Connecting to a single node**

```csharp
var node = new Uri("http://myserver:9200");
var settings = new ConnectionSettings(node);
var client = new ElasticClient(settings);
```

**Using a connection pool**

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

Indexing a document is as simple as (with 6.x):

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
    From = 0,
    Size = 10,
    Query = new TermQuery { Field = "user", Value = "kimchy" } || 
            new MatchQuery { Field = "description", Query = "nest" }
};

var response = client.Search<Tweet>(request);
```

### Falling back to Elasticsearch.Net

NEST also includes and exposes the low-level [Elasticsearch.Net](https://github.com/elasticsearch/elasticsearch-net/tree/master/src/Elasticsearch.Net) client that you can fall back to incase anything is missing:

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

A low-level, dependency free, client that has no opinions how you build and represent your requests and responses.

* Low-level client that provides a one-to-one mapping with the Elasticsearch REST API
* No dependencies
* Almost completely generated from the official REST API spec which makes it easy to keep up to date
* Comes with an integration test suite that can be generated from the YAML test definitions that the Elasticsearch core team uses to test their REST API
* Has no opinions on how you create or consume requests and responses
* Load balancing and cluster failover support
* All calls have async variants

### Installing

From the package manager console:

	PM> Install-Package Elasticsearch.Net

or by searching for `Elasticsearch.Net` in the package manager UI.

### Connecting

Connecting using the low-level client is very similar to how you would connect using NEST.  In fact, the connection constructs that NEST use are actually Elasticsearch.Net constructs.  Thus, single node connections and connection pooling still apply when using Elasticsearch.Net.

```csharp
var node = new Uri("http://myserver:9200");
var config = new ConnectionConfiguration(node);
var client = new ElasticLowLevelClient(config);
```

Note the main difference here is that we are instantiating an `ElasticLowLevelClient` rather than `ElasticClient`, and `ConnectionConfiguration` instead of `ConnectionSettings`.

### Calling an API endpoint

Elasticsearch.Net is generated from the the [official REST specification](https://github.com/elastic/elasticsearch/tree/master/rest-api-spec), and thus maps to all Elasticsearch API endpoints.

```csharp
client.GetSource("myindex","mytype","1",qs=>qs
    .Routing("routingvalue")
);
```

will execute a `GET` to `/myindex/mytype/1/_source?routing=routingvalue`. All the methods and arguments are fully documented based on the documentation of the specification.

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

Text within multi-line comments conforms to [asciidoc](http://asciidoc.org/), a lightweight markdown style text format well suited to technical documentation. To generate the asciidoc files from the test files, you need to run the [DocGenerator](src/CodeGeneration/DocGenerator) console application which will output the documentation files in the docs output directory. To verify that the generated asciidoc files can generate the documentation for the website, [clone the elastic docs repo](https://github.com/elastic/docs) and follow the instructions there for building documentation locally. as an example, suppose I have cloned the elastic docs to `c:\source\elastic-docs`, then to verify the generated asciidoc files for NEST are valid would be as following (using Cygwin on Windows)

```sh
cd /cygdrive/c/source/elastic-docs

./build_docs.pl --doc /cygdrive/c/source/elasticsearch-net-master/docs/index.asciidoc --chunk=1 --open
```

the result of running this for a successful build will be

```sh
Building HTML from /cygdrive/c/source/elasticsearch-net-master/docs/index.asciidoc
Done
See: /cygdrive/c/source/elasticsearch-docs/html_docs/index.html
```

and a small HTTP server will be spun up locally on port 8000 through which you can view the documentation.

[Pull Requests](https://github.com/elastic/elasticsearch-net/pulls) are most welcome for areas of documentation that need improving.

#### Many thanks to:
* [Q42](https://q42.nl/) for supporting the development of NEST
* [redgate](http://www.red-gate.com) for supplying @Mpdreamz with an ANTS Memory Profiler 8 & ANTS Performance Profiler 8 licenses
* [jetBrains](http://www.jetbrains.com) for supplying @Mpdreamz with a dotTrace profiler and Resharper license
* [CodeBetter](http://codebetter.com) for hosting the continuous integration for NEST
* Everyone who has been awesome enough to contribute back to NEST (You're listed automatically on the [documentation page](https://github.com/elastic/elasticsearch-net/graphs/contributors))

## Copyright and License

This software is Copyright (c) 2014-2019 by Elasticsearch BV.

This is free software, licensed under: [The Apache License Version 2.0](https://github.com/elasticsearch/elasticsearch-net/blob/develop/license.txt).
