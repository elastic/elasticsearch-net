---
layout: default
title: Connecting
menu_section: facets
menu_item: histogram
---


# Histogram Facet

The histogram facet works with numeric data by building a histogram across intervals of the field values. Each value is “rounded” into an interval (or placed in a bucket), and statistics are provided per interval/bucket (count and total). Here is a simple example:

	this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
		.Size(10)
		.MatchAll()
		.FacetHistogram(h => h.OnField(f=>f.LOC).Interval(100))
	);

See [original docs](http://www.elasticsearch.org/guide/reference/api/search/facets/histogram-facet.html) for more information


