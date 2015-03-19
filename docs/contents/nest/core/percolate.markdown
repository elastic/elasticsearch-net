---
template: layout.jade
title: Percolate
menusection: core
menuitem: percolate
---


# Percolation
The percolator allows to register queries against an index, then send percolate requests which include a doc, and get back the queries that match on that doc out of the set of registered queries. 

Percolate is a complex but awesome Elasticsearch feature, so be sure to read the [official documentation](http://www.elastic.co/guide/en/elasticsearch/reference/current/search-percolate.html).

## Register a Percolator

	client.RegisterPercolator<ElasticsearchProject>("my-percolator", p => p
		.Query(q => q
			.Term(f => f.Name, "NEST")
		)
	);

## Percolate a Document

	var project = new ElasticsearchProject
	{
		Id = 1,
		Name = "NEST",
		Country = "Netherlands"
	};

	var result = client.Percolate<ElasticsearchProject>(p => p.Document(project));

`result.Matches` will contain any percolators that matched the given document `project`.

## Unregister a Percolator

	client.UnregisterPercolator<ElasticsearchProject>("my-percolator");

## Percolate from a Bulk index action

It's also possible to percolate while bulk indexing:

	client.Bulk(b => b
		.Index<ElasticsearchProject>(i => i
			.Document(new ElasticsearchProject { Id = 1, Name = "NEST" })
			.Percolate("*") // Match on any percolated docs
		)
	);

