---
template: layout.jade
title: Connecting
menusection: search
menuitem: aggregation
---


# Aggregations

Aggregations is new and more powerfull way to build facets. Using them its possible to build any kind of facets and grouped in way as needed.

## Simple query

	var results = EClinet.Search<ElasticSearchObject>(s => s.QueryString("*")
		.Aggregations(a => a.Terms("GroupName", ta => ta.Field("Category")))
	);

then to read items 
    
	var termBucket = results.Aggs.Terms("GroupName").Items;

## Global

	var results = EClinet.Search<ElasticSearchObject>(s => s.QueryString("*")
		.Aggregations(a => a.Global("Title", g=>g.Aggregations(ga => ga.Terms("GroupName", ta => ta.Field("Category"))))
	));

##Sub aggregation

	var results = EClinet.Search<ElasticSearchObject>(s => s.QueryString("*")
		.Aggregations(a => a.Global("Title", g=>g.Aggregations(ga => ga.Terms("GroupName", ta => ta.Field("Category")
			.Aggregations(sa=>sa.Terms("Color", sat=>sat.Field("Color")))
		)))));