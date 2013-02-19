---
template: layout.jade
title: Boosting Query
menusection: query
menuitem: boosting
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
