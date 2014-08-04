---
template: layout.jade
title: Geo Distance Facet
menusection: facets
menuitem: geo-distance
---


# Geo Distance Facet

The geo_distance facet is a facet providing information for ranges of distances from a provided geo_point including count of the number of hits that fall within each range, and aggregation information (like total).

	this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
		.Size(10)
		.MatchAll()
		.FacetGeoDistance("geo1", gd => gd
			.OnValueField(f=>f.Origin)
			.PinTo(Lat: 40, Lon: -70)
			.Ranges(
				r=>r.To(10),
				r=>r.From(10).To(20),
				r=>r.From(20).To(100),
				r=>r.From(100)
			)
		)
	);

See [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-geo-distance-facet.html) for more information.


