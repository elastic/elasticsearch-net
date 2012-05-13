---
layout: default
title: Top Children Query
menu_section: query
menu_item: top-children
---


# Top Children Query

The top_children query runs the child query with an estimated hits size, and out of the hit docs, aggregates it into parent docs. If there aren’t enough parent docs matching the requested from/size search request, then it is run again with a wider (more hits) search.

The top_children also provide scoring capabilities, with the ability to specify max, sum or avg as the score type.

Simple example:

	.TopChildren<Person>(fz => fz
		.Query(qq=>qq.Term(f=>f.FirstName, "john"))
		.Scope("my_scope")
	)

Custom scoring

	.TopChildren<Person>(fz => fz
		.Query(qq => qq.Term(f => f.FirstName, "john"))
		.Score(TopChildrenScore.avg)
		.Scope("my_scope")
		.Type("sillypeople")
	)