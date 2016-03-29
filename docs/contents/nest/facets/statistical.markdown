---
template: layout.jade
title: Statistical Facet
menusection: facets
menuitem: statistical
---

# Statistical Facet

Statistical faceting allows you to compute statistical data on numeric fields. The statistical data includes count, total, sum of squares, mean (average), minimum, maximum, variance, and standard deviation. Here is an example:


	this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
		.Size(10)
		.MatchAll()
		.FacetStatistical(sf=>sf
			.OnField(f=>f.LOC)
		)
	);

See [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-statistical-facet.html) for more information.

