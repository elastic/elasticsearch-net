---
template: layout.jade
title: More Like This Query
menusection: query
menuitem: mlt
---


# More Like This Query
More like this query find documents that are “like” provided text by running it against one or more fields.

	.Query(q => q
		.MoreLikeThis(fz => fz
			.OnFields(f => f.Name)
			.LikeText("elasticsearcc")
		)
	);

