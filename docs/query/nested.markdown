---
layout: default
title: Connecting
menu_section: query
menu_item: nested
---


# Nested Query

Nested query allows to query nested objects / docs (see [nested mapping](http://www.elasticsearch.org/guide/reference/mapping/nested-type.html). The query is executed against the nested objects / docs as if they were indexed as separate docs (they are, internally) and resulting in the root parent doc (or parent nested mapping). 

	.Nested(n=>n
		.Path(f=>f.Followers[0])
		.Query(q=>q.Term(f=>f.Followers[0].FirstName,"elasticsearch.pm"))
	)