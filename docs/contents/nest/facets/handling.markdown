---
template: layout.jade
title: Facets
menusection: facets
menuitem: handling
---


# Faceting
For a good overview of what facets are see the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets.html) on the subject.

**NOTE** Facets are deprecated and will be removed in future releases of Elasticsearch. You are encouraged to migrate to [Aggregations](/nest/aggregations/handling.html).

## Specifying Facets during Search

In its simplest form, you can add a facet to your query like this:

	var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
        .Size(10)
        .MatchAll()
        .FacetTerm(t => t
          .OnField(f => f.Country)
          .Size(20)
        )
	);

Adding more then one facet is also really easy:

	var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
        .Size(10)
        .MatchAll()
        .FacetTerm(t => t
          .OnField(f => f.Country)
          .Size(20)
        )
        .FacetTerm(t => t
          .OnField(f => f.Author)
          .Size(20)
        )
	);

NEST supports all the additional properties you can set on facets 
	
	var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
        .Size(10)
        .MatchAll()
        .FacetTerm(t => t
          .OnField(f => f.Country)
          .Size(20)
          .Order(TermsOrder.reverse_count)
          .Exclude("term1", "term2")
          .AllTerms()
          .Regex(@"\s+", RegexFlags.DOTALL)
          .Script("term + 'aaa'")
          .ScriptField("_source.my_field")
        )
        .FacetDateHistogram(h => h
          .OnField(f => f.StartedOn)
          .Interval(DateInterval.Day, DateRounding.Half_Floor)
          .TimeZones(Pre: "-3", Post: "-4")
      )
	);

Allowing you to take advantage of all the cool facets stuff built into Elasticsearch.

###  Getting to your facet

If you are interested in the facet meta data (such as missing, total) you can use the following methods:

	var facet = queryResults.Facet<TermFacet>(p=>p.Followers.Select(f=>f.LastName));

This will return a `TermFacet` object which has an `.Items` property holding all the facets.

`queryResult` also holds a `.Facets` dictionary one can use to iterate over the facets returned from the query.

## Shortcut to facet items

To get the facet items for `followers.lastName` the prettiest way to get them is.

	var facets = queryResults.FacetItems<FacetItem>(p=>p.Followers.Select(f=>f.LastName));

NEST will infer the right key from the specified lambda. You can also opt for specifying the name directly.

	var facets = queryResults.FacetItems<FacetItem>("followers.lastName");

***NOTE***  more types then just term facets are supported see the 'Corresponding Types' section

## Corresponding Types

The following lists the elasticsearch facet type and their corresponding NEST strongly typed class 

***terms_stats*** => [TermStatsFacet]({{root}}/facets/term-stats.html)

***statistical*** => [StatisticalFacet]({{root}}/facets/statistical.html)

***terms*** => [TermFacet]({{root}}/facets/terms.html)

***histogram***  => [HistogramFacet]({{root}}/facets/histogram.html)

***date_histogram*** => [DateHistogramFacet]({{root}}/facets/date-histogram.html)

***range*** => [DateRangeFacet]({{root}}/facets/range.html), [RangeFacet]({{root}}/facets/range.html)

***geo_distance*** => [GeoDistanceFacet]({{root}}/facets/geo-distance.html)

***query*** => [QueryFacet]({{root}}/facets/query.html)

***filter*** => [FilterFacet]({{root}}/facets/filter.html)

See also each individual facet's documentation.
					

