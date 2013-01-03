---
layout: default
title: Ids Query
menu_section: query
menu_item: ids
---


# Ids Query

Filters documents that only have the provided ids. Note, this filter does not require the _id field to be indexed since it works using the _uid field.

	.Query(q=>q.Ids(new[] { "1", "4", "100" })

