---
layout: default
title: Span Term Query
menu_section: query
menu_item: span-term
---


# Span Term Query

Matches spans containing a term. The span term query maps to Lucene `SpanTermQuery`.

	.Query(q => q
		.SpanTerm(f=>f.Name, "elasticsearch.pm", 1.1)
	);
