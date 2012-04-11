---
layout: default
title: Connecting
menu_section: query
menu_item: bool
---


# BoolQuery

A query that matches documents matching boolean combinations of other queries. The bool query maps to Lucene BooleanQuery. It is built using one or more boolean clauses, each clause with a typed occurrence. 

	.Query(qd=>qd
		.Bool(b=>b
			.Must(q => q.MatchAll())
			.MustNot(q => q.Term(p => p.Name, "elasticsearch.pm"))
			.Should(q => q.Term(p => p.Name, "elasticflume"))
		)
	)

note each clause can take multiple queries e.g:

	.Should(
		q => q.Term(p => p.Name, "elasticflume"),
		q => q.Term(p => p.Name, "Nest")

	)

See [original docs](http://www.elasticsearch.org/guide/reference/query-dsl/bool-query.html) for more information