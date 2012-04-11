---
layout: default
title: Connecting
menu_section: query
menu_item: flt
---


# Fuzzy Like This Query
Fuzzy like this query find documents that are “like” provided text by running it against one or more fields.

	.Query(qd=>qd
		.FuzzyLikeThis(fz => fz
			.OnFields(f => f.Name)
			.LikeText("elasticsearcc")
			.PrefixLength(3)
			.MaxQueryTerms(25)
			.IgnoreTermFrequency(true)
			.Boost(1.1)
			.Analyzer("my_analyzer")
		)
	)

See [original docs](http://www.elasticsearch.org/guide/reference/query-dsl/flt-query.html) for more information

