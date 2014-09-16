---
template: layout.jade
title: Sort
menusection: search
menuitem: sort
---


# Sort

Apply a sort over the search results. Please note that sorting and boosting are mutually exclusive.

	.SortAscending(p=> p.Name.Suffix("sort"))

or

	.SortDescending("name.sort")


