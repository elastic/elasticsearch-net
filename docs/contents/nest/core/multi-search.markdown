---
template: layout.jade
title: Multi Search
menusection: core
menuitem: multi-search
---


# Multi Search

The multi search API allows to execute several search requests within the same API.

### Fluent Syntax

	var result = client.MultiSearch(ms => ms
		.Search<ElasticsearchProject>("esproj", s => s.MatchAll())
		.Search<Person>("people", s => s.MatchAll())
	);


### Object Initializer Syntax

	var request = new MultiSearchRequest
	{
		Operations = new Dictionary<string, ISearchRequest>
		{
			{ "esproj", new SearchRequest 
				{ 
					Query = new QueryContainer(new MatchAllQuery()) 
				} 
			},
			{ "people", new SearchRequest 
				{ 
					Query = new QueryContainer(new MatchAllQuery()) 
				} 
			}
		}
	};

	var result = client.MultiSearch(request);

## Handling the Multi Search Response

`MultiSearch` returns an `IMultiSearchResponse` object.  Each `SearchResponse<T>` can be retrieved using the corresponding name that was specified in the request.

	// returns a SearchResponse<ElasticsearchProject>>
	var projects = result.GetResponse<ElasticsearchProject>("esproj");

	// returns a SearchResponse<Person>>
	var people = result.GetResponse<Person>("people");
