---
template: layout.jade
title: Connecting
menusection: facets
menuitem: query
---


# Query Facet

A facet query allows to return a count of the hits matching the facet query. The query itself can be expressed using the Query DSL. For example:


	this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
		.Size(10)
		.MatchAll()
		.FacetQuery("wow_facet", q=>q
			.Term(f=>f.Name, "elasticsearch.pm")
		);
	);

See [original docs](http://www.elasticsearch.org/guide/reference/api/search/facets/query-facet.html) for more information

