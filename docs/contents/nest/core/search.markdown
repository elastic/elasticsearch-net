---
template: layout.jade
title: Search
menusection: core
menuitem: search
---


Search is **THE** call you'll probably use the most, as it exposes Elasticsearch's key functionality: search!

### Fluent Syntax

    var result = client.Search<ElasticsearchProject>(s => s
        .From(0)
    	.Size(50)
        .Query(q => ....)
        .Filter(f => ....)	     
    );

### Object Initializer Syntax

	var searchRequest = new SearchRequest
	{
		From = 0,
		Size = 50,
		Query = ...
		Filter = ...
	};

	var result = client.Search<ElasticsearchProject>(searchRequest);

## Handling the Search response

`.Search<T>` returns an `ISearchResponse<T>` which has a `Hits` property.

`Hits` is an `IEnumerable<IHit<T>>`.  `IHit<T>` contains a `Source` property which holds the original document (`T`), along with other meta deta from Elasticsearch such as `Id`, `Score`, `Version`, `Index`, `Type`, `Sorts`, `Highlights` and `Explanation`.

See [the sections](/nest/search/basics.html) dedicated to Search for more information.





