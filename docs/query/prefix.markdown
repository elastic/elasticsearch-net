---
layout: default
title: Connecting
menu_section: query
menu_item: prefix
---


# Prefix Query
Matches documents that have fields containing terms with a specified prefix (not analyzed). The prefix query maps to Lucene `PrefixQuery`.

	.Query(q => q
		.Prefix(f => f.Name, "el", Boost: 1.2)
	);
