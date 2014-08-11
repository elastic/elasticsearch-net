---
template: layout.jade
title: Count
menusection: core
menuitem: count
---


# Count

The count API allows to easily execute a query and get the number of matches for that query. It can be executed across one or more indices and across one or more types. The query can either be provided using a simple query string as a parameter, or using the Query DSL defined within the request body.

## Examples

	var result = client.Count();

The above will do a count query across all indices. (The result type here is not limited)

If you want to limit the scope to a specific index:

	var result = client.Count<ElasticsearchProject>();

NEST will infer the index and type, but as usual, you can override the inferrence by specifying the index and type explicitly:

	var result = client.Count(c => c
		.Index("elasticsearchprojects")
		.Type("elasticsearchproject")
	);

You can also specify multiple indices and types:

	var result = client.Count(c => c
		.Indices("elasticsearchprojects", "foo", "bar")
		.Types("elasticsearchproject", "foo", "bar")
	);

`result` is an `ICountResponse` which contains the document count found in the `Count` property, along with the shards meta data (total, successful, failed) contained in the `Shards` property.

## Count by Query

Counting the number of documents that match a query is as simple as:

	var result = client.Count<ElasticsearchProject>(c => c
		.Query(q => q
			.Match(m => m
				.OnField(p => p.Name)
				.Query("NEST")
			)
		)
	);


