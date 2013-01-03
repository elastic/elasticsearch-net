---
layout: default
title: Connecting
menu_section: facets
menu_item: term-stats
---

# Term Stats Facet

The terms_stats facet combines both the terms and statistical allowing to compute stats computed on a field, per term value driven by another field. For example:


	this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
		.Size(10)
		.MatchAll()
		.FacetTerm(t => t.OnField(f => f.Country).Size(20))
	);

See [original docs](http://www.elasticsearch.org/guide/reference/api/search/facets/terms-stats-facet.html) for more information


