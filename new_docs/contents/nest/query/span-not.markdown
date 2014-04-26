---
template: layout.jade
title: Span Not Query
menusection: query
menuitem: span-not
---


# Span Not Query

Removes matches which overlap with another span query. The span not query maps to Lucene `SpanNotQuery`

	.SpanNot(sf=>sf
		.Include(e =>e.SpanTerm(f => f.Name, "elasticsearch.pm", 1.1))
		.Exclude(e=>e.SpanTerm(f => f.Name, "elasticsearch.pm", 1.1))
	)

The include and exclude clauses can be any span type query. The include clause is the span query whose matches are filtered, and the exclude clause is the span query whose matches must not overlap those returned.