---
template: layout.jade
title: Custom Boost Factor Query
menusection: query
menuitem: custom-boost-factor
---


# Custom Boost Factor Query

custom_boost_factor query allows to wrap another query and multiply its score by the provided boost_factor. This can sometimes be desired since boost value set on specific queries gets normalized, while this query boost factor does not

	.Query(qd=>qd
		.CustomBoostFactor(cs=>cs
			.BoostFactor(5.2)
			.Query(qq=>qq.MatchAll())
		)
	)

