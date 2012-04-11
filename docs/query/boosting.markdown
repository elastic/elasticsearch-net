---
layout: default
title: Connecting
menu_section: query
menu_item: boosting
---


# Boosting Query

The boosting query can be used to effectively demote results that match a given query. Unlike the “NOT” clause in bool query, this still selects documents that contain undesirable terms, but reduces their overall score.

	.Query(qd=>qd
		.Boosting(b=>b
			.Positive(q => q.MatchAll())
			.Negative(q => q.Term(p => p.Name, "elasticsearch.pm"))
			.NegativeBoost(0.4)
		)
	)

See [original docs](http://www.elasticsearch.org/guide/reference/query-dsl/boosting-query.html) for more information