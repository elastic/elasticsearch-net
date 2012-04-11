---
layout: default
title: Connecting
menu_section: query
menu_item: constant-score
---


# Constant Score Query

A query that wraps a filter or another query and simply returns a constant score equal to the query boost for every document in the filter. Maps to Lucene ConstantScoreQuery

	.Query(qd=>qd
		.ConstantScore(cs=>cs
			.Query(qq=>qq.MatchAll())
			.Boost(1.2)
		)
	)

See [original docs](http://www.elasticsearch.org/guide/reference/query-dsl/constant-score-query.html) for more information

