---
layout: default
title: More Like This Query
menu_section: query
menu_item: mlt
---


# More Like This Query
More like this query find documents that are “like” provided text by running it against one or more fields.

	.Query(q => q
		.MoreLikeThis(fz => fz
			.OnFields(f => f.Name)
			.LikeText("elasticsearcc")
		)
	);

