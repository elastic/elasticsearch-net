---
template: layout.jade
title: Span First Query
menusection: query
menuitem: span-first
---


# Span First Query
Matches spans near the beginning of a field. The span first query maps to Lucene `SpanFirstQuery`. 

	.SpanFirst(sf=>sf
		.MatchTerm(f => f.Name, "elasticsearch.pm", 1.1)
		.End(3)
	)
