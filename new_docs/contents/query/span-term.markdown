---
template: layout.jade
title: Span Term Query
menusection: query
menuitem: span-term
---


# Span Term Query

Matches spans containing a term. The span term query maps to Lucene `SpanTermQuery`.

	.Query(q => q
		.SpanTerm(f=>f.Name, "elasticsearch.pm", 1.1)
	);
