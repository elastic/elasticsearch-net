---
template: index.jade
title: Introduction
menusection: concepts
menuitem: introduction
---

# Introduction

NEST aims to be a .net client with a very concise API. Its main goal is to provide a solid strongly typed Elasticsearch client. It also has string/dynamic overloads for more dynamic usecases. 

Indexing is as simple as:

	var post = new Post() { Id = 12, ... }
	client.Index(post);

Indexing asynchronously is as easy as:

	//IndexAsync returns a Task<ConnectionStatus>
	var task = client.IndexAsync(post);

Searching is fluent:

	var results = this.ConnectedClient.Search<ElasticSearchProject>(s => s
			.From(0)
			.Size(10)
			.Fields(f => f.Id, f => f.Name)
			.SortAscending(f => f.LOC)
			.SortDescending(f => f.Name)
			.Query(q=>q.Term(f=>f.Name, "NEST", Boost: 2.0))
	);

## Installing 

Nest can be installed through NuGet:

	PM> Install-Package NEST

Or searching for "elasticsearch" will get you to nest as well. 

## Who's using Nest

Some notable examples are the stackexchange sites ([stackoverflow.com](http://stackoverflow.com/search?q=elasticsearch), [serverfault.com](http://serverfault.com/search?q=elasticsearch), ..) and [rijksmuseum.nl](https://www.rijksmuseum.nl/en/search?q=elastiek). If you are using Nest let me know! I love to boast about it right here :)

## Read more elsewhere

[@joelabrahamsson](http://twitter.com/joelabrahamsson) wrote a great [intro into elasticsearch on .NET](http://joelabrahamsson.com/entry/extending-aspnet-mvc-music-store-with-elasticsearch)
using NEST. 

Also checkout the [searchbox.io guys](https://searchbox.io/) rocking NEST [on AppHarbor](http://blog.appharbor.com/2012/06/19/searchbox-elasticsearch-is-now-an-add-on) 
with their [demo project](https://github.com/searchbox-io/.net-sample)

## Questions, bugs, comments, requests

All of these are more then welcome on the github issues pages! I try to to at least reply within the same day.

I also monitor question tagged with ['nest' on stackoverflow](http://stackoverflow.com/questions/tagged/nest)

## Copyright

Copyright (c) 2010 Martijn Laarman and everyone wonderful enough to contribute to [NEST](https://github.com/Mpdreamz/NEST)

## License

NEST is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [license.txt](https://github.com/Mpdreamz/NEST/blob/master/src/license.txt) for more information.

## Contributors

A special shoutout to [@stephenpope](http://github.com/stephenpope) for allowing his port 
of the java factory based dsl [Rubber](http://github.com/stephenpope/Rubber) to be merged into NEST. 
NEST now has **two types of query dsl's** (lambda and factory based)!

Some of the other wonderful features in NEST were pushed by these wonderful folks:

