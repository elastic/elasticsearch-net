---
template: layout.jade
title: Filtered Query
menusection: query
menuitem: filtered
---


# Filtered Query
A query that applies a filter to the results of another query. This query maps to Lucene FilteredQuery.

	.Query(qd=>qd
		.Filtered(cs=>cs
			.Query(q=>q.MatchAll())
			.Filter(f => f.MatchAll())
		)
	)


