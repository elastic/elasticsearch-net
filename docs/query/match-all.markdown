---
layout: default
title: Connecting
menu_section: query
menu_item: match-all
---


# Match All query

A query that matches all documents. Maps to Lucene `MatchAllDocsQuery`.

	.From(0)
	.Size(10)
	.Query(q => q.MatchAll());

A special shortcut exists for matchall queries

	.From(0)
	.Size(10)
	.MatchAll()

