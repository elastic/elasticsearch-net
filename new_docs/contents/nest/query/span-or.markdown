---
template: layout.jade
title: Span Or Query
menusection: query
menuitem: span-or
---


# Span Or Query

Matches the union of its span clauses. The span or query maps to Lucene `SpanOrQuery`. 

	.SpanOr(sn => sn
		.Clauses(
			c => c.SpanTerm(f => f.Name, "elasticsearch.pm", 1.1),
			c => c.SpanFirst(sf => sf
				.MatchTerm(f => f.Name, "elasticsearch.pm", 1.1)
				.End(3)
			)
		)
	)

The clauses element is a list of one or more other span type queries.