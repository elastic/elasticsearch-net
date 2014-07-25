---
template: layout.jade
title: Wildcard Query
menusection: query
menuitem: wildcard
---

# Wildcard Query

Matches documents that have fields matching a wildcard expression (**not analyzed**). Supported wildcards are `*`, which matches any character sequence (including the empty one), and `?`, which matches any single character. Note this query can be slow, as it needs to iterate over many terms. In order to prevent extremely slow wildcard queries, a wildcard term should not start with one of the wildcards `*` or `?`. The wildcard query maps to Lucene `WildcardQuery`.

	.From(0)
	.Size(10)
	.Query(q => q
		.Wildcard(f => f.Name, "elasticsearch.*")
	);