---
template: layout.jade
title: Terms Facet
menusection: facets
menuitem: terms
---

# Terms Facet

Allows you to specify field facets that return the N most frequent terms. For example:

	this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
		.Size(10)
		.MatchAll()
		.FacetTerm(t => t.OnField(f => f.Country).Size(20))
	);

See [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-terms-facet.html) for more information.


