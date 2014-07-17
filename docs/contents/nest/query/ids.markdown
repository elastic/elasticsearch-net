---
template: layout.jade
title: Ids Query
menusection: query
menuitem: ids
---


# Ids Query

Filters documents that only have the provided ids. Note, this filter does not require the _id field to be indexed since it works using the _uid field.

	.Query(q=>q.Ids(new[] { "1", "4", "100" })

