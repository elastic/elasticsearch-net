---
template: layout.jade
title: Date Histogram Facet
menusection: facets
menuitem: date-histogram
---


# Date Histogram Facet

A specific histogram facet that can work with date field types enhancing it over the regular histogram facet. Here is a quick example:

    this.ConnectedClient.Search<ElasticSearchProject>(s=>s
      .From(0)
      .Size(10)
      .MatchAll()
      .FacetDateHistogram(h => h
        .OnField(f => f.StartedOn)
        .Interval(DateInterval.Day)
        .Factor(1000)
      )
    );

See [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-date-histogram-facet.html) for more information.