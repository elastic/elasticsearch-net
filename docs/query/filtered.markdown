---
layout: default
title: Connecting
menu_section: query
menu_item: filtered
---


# Filtered Query
A query that applies a filter to the results of another query. This query maps to Lucene FilteredQuery.

	.Query(qd=>qd
		.Filtered(cs=>cs
			.Query(q=>q.MatchAll())
			.Filter(f => f.MatchAll())
		)
	)

See [original docs](http://www.elasticsearch.org/guide/reference/query-dsl/filtered-query.html) for more information

