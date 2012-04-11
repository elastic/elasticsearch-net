---
layout: default
title: Connecting
menu_section: query
menu_item: fuzzy
---


# Fuzzy Query
A fuzzy based query that uses similarity based on Levenshtein (edit distance) algorithm.

Warning: this query is not very scalable with its default prefix length of 0 – in this case, every term will be enumerated and cause an edit score calculation or max_expansions is not set.

	.Query(qd=>qd
		.Fuzzy(fz => fz
			.OnField(f=>f.Name)
			.Value("elasticsearcc")
		)
	)

Numeric / Date Fuzzy

fuzzy query on a numeric field will result in a range query “around” the value using the min_similarity value. For example:

	.FuzzyNumeric(fz => fz
		.OnField(f=>f.LOC)
		.Value(200)
		.MinSimilarity(12)
	)

Same applies to dates, with support for time format for the min_similarity field:

	.FuzzyDate(fz => fz
		.OnField(f=>f.StartedOn)
		.Value(new DateTime(1999,12,31))
		.MinSimilarity("1d")
	)

See [original docs](http://www.elasticsearch.org/guide/reference/query-dsl/fuzzy-query.html) for more information

