---
template: layout.jade
title: More Like This
menusection: core
menuitem: more-like-this
---


# More Like This

The more like this (mlt) API allows to get documents that are "like" a specified document.

## Examples

### Fluent Syntax

	var result = client.MoreLikeThis<ElasticsearchProject>(mlt => mlt
		.Id(1)
		.MltFields(p => p.Country, p => p.Content)
		.MinDocFreq(1)
		.Search(s => s
			.From(0)
			.Size(20)
		)
	);

### Object Initializer Syntax

	var request = new MoreLikeThisRequest<ElasticsearchProject>(1)
	{
		MltFields = new PropertyPathMarker[] { "country", "content"},
		MinDocFreq = 1,
		Search = new SearchRequest
		{
			From = 0,
			Size = 10
		}
	};

	var result = client.MoreLikeThis<ElasticsearchProject>(request);

## Handling the MLT response

`.MoreLikeThis<T>` behaves just like `.Search<T>` in that it also returns an `ISearchResponse<T>`.

See the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-more-like-this.html) for more information.