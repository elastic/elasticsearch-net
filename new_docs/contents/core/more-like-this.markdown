---
template: layout.jade
title: Connecting
menusection: core
menuitem: more-like-this
---


# More Like This
More like this comes in two flavors, as the [_mlt query type](/query/mlt.html) and as the special `_mlt` API call endpoint. 

The special API can be called from NEST as follows:

	var result = this._client.MoreLikeThis<ElasticSearchProject>(mlt => mlt
		.Id(1)
		.Options(o => o
			.OnFields(p => p.Country, p => p.Content)
			.MinDocumentFrequency(1)
		)
		.Search(s=>s
			.From(0)
			.Take(20)
		)
	);