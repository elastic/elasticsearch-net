# NEST

*Strongly typed Elasticsearch client*

NEST aims to be a .net client with a very concise API. Its main goal is to provide a solid strongly typed Elasticsearch client. 

Indexing is as simple as:

	var post = new Post() { Id = 12, ... }
	var result = client.Index(post);

All the calls have async variants:

	//t is a Task<ConnectionStatus>
	var t = client.IndexAsync(post);

Searching is fluent:

	var results = client.Search<ElasticSearchProject>(s => s
			.From(0)
			.Size(10)
			.Fields(f => f.Id, f => f.Name)
			.SortAscending(f => f.LOC)
			.SortDescending(f => f.Name)
			.Query(q=>q.Term(f=>f.Name, "NEST", Boost: 2.0))
	);

NEST also comes with a low level client that you can fall back to incase anything is missing:

	//.Raw is of type IRawElasticClient
	var results = this._client.Raw.SearchPost("myindex","elasticsearchprojects", new
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

#[Read the documentation here](http://nest.azurewebsites.net/)

additionally [@joelabrahamsson](http://twitter.com/joelabrahamsson) wrote a great [intro into elasticsearch on .NET](http://joelabrahamsson.com/entry/extending-aspnet-mvc-music-store-with-elasticsearch)
using NEST. 

Also checkout the [searchbox.io guys](https://searchbox.io/) rocking NEST [on AppHarbor](http://blog.appharbor.com/2012/06/19/searchbox-elasticsearch-is-now-an-add-on) 
with their [demo project](https://github.com/searchbox-io/.net-sample)

## Installing 

Nest can be installed through NuGet:

	PM> Install-Package NEST

Or searching for "elasticsearch"  will get you to nest as well. 

#Who's using NEST?
* [stackoverflow.com](http://www.stackoverflow.com) (and the rest of the stackexchange family).
* [7digital.com](http://www.7digital.com) (run NEST on mono).
* [rijksmuseum.nl](https://www.rijksmuseum.nl/en) (elasticsearch is the only datastorage hit for each page).
* [Kiln](http://www.fogcreek.com/kiln/) FogCreek's version control & code review tooling. 
  They are so pleased with elasticsearch that [they made a video about how pleased they are!](http://blog.fogcreek.com/kiln-powered-by-elasticsearch/)

Always keen to hear and list more uses ! hit me on [@Mpdreamz](https://twitter.com/Mpdreamz)

## Questions, bugs, comments, requests

All of these are more then welcome on the github issues pages! I try to to at least reply within the same day.

## Copyright

Copyright (c) 2010 Martijn Laarman and everyone wonderful enough to contribute to [NEST](http://nest.azurewebsites.net/)

Many thanks to: 

* my ex-employer [Q42](http://www.q42.nl) for supporting the development of NEST by donating 20% of my contract hours for NEST's development. 
* redgate for supplying me with an ANTS Memory Profiler 8 & ANTS Performance Profiler 8 licenses 
* jetBrains for supplying me with a dotTrace profiler license and a Resharper license
* CodeBetter for hosting the continuous integration for NEST
* Everyone who's been awesome enough to contribute back to NEST (You're listed automatically on the [documentation page](http://nest.azurewebsites.net/))

## License

NEST is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [license.txt](https://github.com/Mpdreamz/NEST/blob/master/src/license.txt) for more information.
