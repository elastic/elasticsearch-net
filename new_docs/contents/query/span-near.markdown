---
template: layout.jade
title: Span Near Query
menusection: query
menuitem: span-near
---


# Span Near Query

Matches spans which are near one another. One can specify slop, the maximum number of intervening unmatched positions, as well as whether matches are required to be in-order. The span near query maps to Lucene `SpanNearQuery`.

	.SpanNear(sn => sn
		.Clauses(
			c => c.SpanTerm(f => f.Name, "elasticsearch.pm", 1.1),
			c => c.SpanFirst(sf => sf
				.MatchTerm(f => f.Name, "elasticsearch.pm", 1.1)
				.End(3)
			)
		)
		.Slop(3)
		.CollectPayloads(false)
		.InOrder(false)
	)

The clauses element is a list of one or more other span type queries and the slop controls the maximum number of intervening unmatched positions permitted.