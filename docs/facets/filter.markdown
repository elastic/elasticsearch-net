---
layout: default
title: Connecting
menu_section: facets
menu_item: filter
---


# Filter Facet

A filter facet (not to be confused with a facet filter) allows you to return a count of the hits matching the filter. The filter itself can be expressed using the Query DSL. For example:

	this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
		.Size(10)
		.MatchAll()
		.FacetFilter("wow_facet", filter=>filter
			.Exists(f=>f.Name)
		)
	);

See [original docs](http://www.elasticsearch.org/guide/reference/api/search/facets/filter-facet.html) for more information
