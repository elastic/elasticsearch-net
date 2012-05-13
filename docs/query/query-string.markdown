---
layout: default
title: Connecting
menu_section: query
menu_item: query-string
---


# Query String Query

A query that uses a query parser in order to parse its content

Bare example:

	.QueryString(qs=>qs.Query("this AND that OR thus"))

Simple with field boosts:

	.QueryString(qs=>qs
		.OnFieldsWithBoost(d=>d
			.Add(f=>f.Name, 2.0)
			.Add(f=>f.Country, 5.0)
		)
		.Query("this AND that OR thus")
	)


All options are mapped:

	.QueryString(qs => qs
		.OnField(f=>f.Name)
		.Query("this that thus")
		.Operator(Operator.and)
		.Analyzer("my_analyzer")
		.AllowLeadingWildcard(true)
		.LowercaseExpendedTerms(true)
		.EnablePositionIncrements(true)
		.FuzzyPrefixLength(2)
		.FuzzyMinimumSimilarity(0.5)
		.PhraseSlop(1.0)
		.Boost(1.0)
		.AnalyzeWildcard(true)
		.AutoGeneratePhraseQueries(true)
		.MinimumShouldMatchPercentage(20)
		.UseDisMax(true)
		.TieBreaker(0.7)
	)