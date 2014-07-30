---
template: layout.jade
title: Term Query
menusection: query
menuitem: term
---


# Term Query

Matches documents that have fields that contain a term (not analyzed). The term query maps to Lucene `TermQuery`

	.Term(f => f.Name, "elasticsearch.pm")