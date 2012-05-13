---
layout: default
title: Term Query
menu_section: query
menu_item: term
---


# Term Query

Matches documents that have fields that contain a term (not analyzed). The term query maps to Lucene `TermQuery`

	.Term(f => f.Name, "elasticsearch.pm")