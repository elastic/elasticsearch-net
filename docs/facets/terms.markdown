---
layout: default
title: Connecting
menu_section: facets
menu_item: terms
---

# Terms Facet

Allow to specify field facets that return the N most frequent terms. For example:

	this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
		.Size(10)
		.MatchAll()
		.FacetTerm(t => t.OnField(f => f.Country).Size(20))
	);

See [original docs](http://www.elasticsearch.org/guide/reference/api/search/facets/terms-facet.html) for more information


