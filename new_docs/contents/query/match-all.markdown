---
template: layout.jade
title: Match All Query
menusection: query
menuitem: match-all
---


# Match All Query

A query that matches all documents. Maps to Lucene `MatchAllDocsQuery`.

	.From(0)
	.Size(10)
	.Query(q => q.MatchAll());

A special shortcut exists for matchall queries

	.From(0)
	.Size(10)
	.MatchAll()

