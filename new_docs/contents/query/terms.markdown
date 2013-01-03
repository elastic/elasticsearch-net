---
layout: default
title: Terms Query
menu_section: query
menu_item: terms
---


# Terms Query

A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses. 

Several overloads exists including

	.Terms(f => f.Name, new[] { "elasticsearch.pm" })

or

	.Terms(f => f.Name, "elasticsearch.pm", "nest")

or

	.TermsDescriptor(tq => tq
		.OnField(f=>f.Name)
		.Terms("elasticsearch.pm", "nest")
		.MinimumMatch(2)
	)

