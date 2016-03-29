---
template: layout.jade
title: Term Stats Facet
menusection: facets
menuitem: term-stats
---

# Term Stats Facet

The terms_stats facet combines both the terms and statistical allowing you to obtain stats computed on a field, per term value driven by another field. For example:


	this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
		.Size(10)
		.MatchAll()
		.FacetTerm(t => t.OnField(f => f.Country).Size(20))
	);

See [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-terms-stats-facet.html) for more information.


