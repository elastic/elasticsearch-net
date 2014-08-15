# NEST [![Build Status](http://teamcity.codebetter.com/app/rest/builds/buildType:%28id:bt993%29/statusIcon)](http://teamcity.codebetter.com/viewType.html?buildTypeId=bt993&guest=1)

NEST is the official high-level .NET client of [elasticsearch](https://github.com/elasticsearch/elasticsearch).  It aims to be a solid, strongly typed client with a very concise API.

### Installing

NEST can be installed through NuGet:

	PM> Install-Package NEST

or by simply searching for `NEST` in the Package Manager UI.

### Connecting
```csharp
var node = new Uri("http://localhost:9200");
var settings = new ConnectionSettings(node);
var client = new ElasticClient(settings);
```
### Indexing
Indexing is as simple as:
```csharp
var person = new Person
{
    Id = "1",
    Firstname = "Martijn",
    Lastname = "Laarman"
};

var index = client.Index(person);
```
All the calls have async variants:
```csharp
var result = client.IndexAsync(person); // returns a Task<ConnectionStatus>
```
### Searching
NEST exposes a fluent interface and a [powerful query DSL](http://nest.azurewebsites.net/concepts/writing-queries.html)
```csharp
var results = client.Search<ElasticsearchProject>(s => s
	.From(0)
	.Size(10)
	.Fields(f => f.Id, f => f.Name)
	.SortAscending(f => f.LOC)
	.SortDescending(f => f.Name)
	.Query(q=>
		q.Term(f=>f.Name, "NEST", Boost: 2.0) 
		|| q.Match(mq=>mq.OnField(f=>f.Name).Query(userInput))
	)
);
```
As well as an object initializer syntax if lamdas arent your thing:
```csharp
var request = new SearchRequest
{
	From = 0,
	Size = 10,
	Query = new QueryContainer(new TermQuery 
		{ 
			Field = "name",
			Value = "NEST" 
		}
	)
};

var results = client.Search<ElasticsearchProject>(request);
```
NEST also includes and exposes the low-level [Elasticsearch.Net](https://github.com/elasticsearch/elasticsearch-net/tree/develop/src/Elasticsearch.Net) client that you can fall back to incase anything is missing:
```csharp
//.Raw is of type IRawElasticClient
var results = client.Raw.SearchPost("myindex","elasticsearchprojects", new
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
});
```
##[Read the documentation here](http://nest.azurewebsites.net/)

Additionally, [@joelabrahamsson](http://twitter.com/joelabrahamsson) wrote a great [intro to Elasticsearch on .NET](http://joelabrahamsson.com/entry/extending-aspnet-mvc-music-store-with-elasticsearch)
using NEST.

Also checkout the [searchbox.io guys](https://searchbox.io/) rocking NEST [on AppHarbor](http://blog.appharbor.com/2012/06/19/searchbox-elasticsearch-is-now-an-add-on) 
with their [demo project](https://github.com/searchbox-io/.net-sample).

#Who's using NEST?
* [stackoverflow.com](http://www.stackoverflow.com) (and the rest of the StackExchange family).
* [7digital.com](http://www.7digital.com) (run NEST on mono).
* [rijksmuseum.nl](https://www.rijksmuseum.nl/en) (Elasticsearch is the only datastorage hit for each page).
* [Kiln](http://www.fogcreek.com/kiln/) FogCreek's version control & code review tooling. 
  They are so pleased with Elasticsearch that [they made a video about how pleased they are!](http://blog.fogcreek.com/kiln-powered-by-elasticsearch/)

Using NEST as well? Let us know on Twitter at [@elasticsearch](https://twitter.com/elasticsearch)!

## Questions, bugs, comments, requests...

All of these are more than welcome on the [GitHub issues](https://github.com/elasticsearch/elasticsearch-net/issues) page!  We try to to at least reply within the same day.

####Many thanks to:
* [Q42](http://www.q42.nl) for supporting the development of NEST
* [redgate](http://www.red-gate.com) for supplying @Mpdreamz with an ANTS Memory Profiler 8 & ANTS Performance Profiler 8 licenses
* [jetBrains](http://www.jetbrains.com) for supplying @Mpdreamz with a dotTrace profiler and Resharper license
* [CodeBetter](http://codebetter.com) for hosting the continuous integration for NEST
* Everyone who has been awesome enough to contribute back to NEST (You're listed automatically on the [documentation page](http://nest.azurewebsites.net))

## Copyright and License

This software is Copyright (c) 2013-2014 by Elasticsearch BV.

This is free software, licensed under: [The Apache License Version 2.0](https://github.com/elasticsearch/elasticsearch-net/blob/develop/license.txt).
