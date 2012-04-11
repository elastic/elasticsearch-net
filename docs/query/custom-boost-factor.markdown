---
layout: default
title: Connecting
menu_section: query
menu_item: custom-boost-factor
---


# Custom Boost Factor Query

custom_boost_factor query allows to wrap another query and multiply its score by the provided boost_factor. This can sometimes be desired since boost value set on specific queries gets normalized, while this query boost factor does not

	.Query(qd=>qd
		.CustomBoostFactor(cs=>cs
			.BoostFactor(5.2)
			.Query(qq=>qq.MatchAll())
		)
	)

See [original docs](http://www.elasticsearch.org/guide/reference/query-dsl/custom-boost-factor-query.html) for more information

