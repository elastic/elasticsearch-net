---
template: layout.jade
title: Indices Query
menusection: query
menuitem: indices
---


# Indices Query

The indices query can be used when executed across multiple indices, allowing to have a query that executes only when executed on an index that matches a specific list of indices, and another query that executes when it is executed on an index that does not match the listed indices.

	.Query(q => q
		.Indices(fz => fz
			.Indices(new[] { "elasticsearchprojects", "people", 
			.Query<Person>(qq => qq.Term(f => f.FirstName, "joe"))
			.NoMatchQuery(qq => qq.MatchAll())
		)
	);



