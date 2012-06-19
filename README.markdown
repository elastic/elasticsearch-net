# NEST

*Strongly typed Elasticsearch client*

NEST aims to be a .net client with a very concise API. Its main goal is to provide a solid strongly typed Elasticsearch client. It also has string/dynamic overloads for more dynamic usecases. 

Indexing is as simple as:

	var post = new Post() { Id = 12, ... }
	var result = client.Index(post);

Indexing asynchronously is as easy as:

	//t is a Task<ConnectionStatus>
	var t = client.IndexAsync(post);

Searching is fluid:

	var results = this.ConnectedClient.Search<ElasticSearchProject>(s => s
			.From(0)
			.Size(10)
			.Fields(f => f.Id, f => f.Name)
			.SortAscending(f => f.LOC)
			.SortDescending(f => f.Name)
			.Query(q=>q.Term(f=>f.Name, "NEST", Boost: 2.0))
	);

#[Read the documentation here](http://mpdreamz.github.com/NEST)

additionally @joelabrahamson wrote a great [intro into elasticsearch on .NET](http://joelabrahamsson.com/entry/extending-aspnet-mvc-music-store-with-elasticsearch)
using NEST. 

## Installing 

Nest can be installed through NuGet:

	PM> Install-Package NEST

Or searching for "elasticsearch"  will get you to nest as well. 

## Questions, bugs, comments, requests

All of these are more then welcome on the github issues pages! I try to to at least reply within the same day.

## Copyright

Copyright (c) 2010 Martijn Laarman and everyone wonderful enough to contribute to [NEST](https://github.com/Mpdreamz/NEST)

A special shoutout to [@stephenpope](http://github.com/stephenpope) for allowing his port 
of the java factory based dsl [Rubber](http://github.com/stephenpope/Rubber) to be merged into NEST. 
NEST now has **two types of query dsl's** (lambda and factory based)!

Some of the other wonderful features in NEST were pushed by these wonderful folks:

* [@nordbergm](https://github.com/nordbergm/NEST)
* [@kevingessner](https://github.com/kevingessner/NEST)
* [@EFJoseph](https://github.com/EFJoseph/NEST)
* [@pkrakowiak](https://github.com/pkrakowiak/NEST) 
* [@q42jaap] (https://github.com/q42jaap/NEST)

## License

NEST is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [license.txt](https://github.com/Mpdreamz/NEST/blob/master/src/license.txt) for more information.
