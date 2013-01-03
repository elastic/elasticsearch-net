---
layout: default
title: Connecting
menu_section: facets
menu_item: statistical
---

# Statistical Facet

Statistical facet allows to compute statistical data on a numeric fields. The statistical data include count, total, sum of squares, mean (average), minimum, maximum, variance, and standard deviation. Here is an example:


	this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
		.Size(10)
		.MatchAll()
		.FacetStatistical(sf=>sf
			.OnField(f=>f.LOC)
		)
	);

See [original docs](http://www.elasticsearch.org/guide/reference/api/search/facets/statistical-facet.html) for more information

