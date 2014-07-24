---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: top-hits
---


# Top Hits aggregation

A top_hits metric aggregator keeps track of the most relevant document being aggregated. This aggregator is intended to be used as a sub aggregator, so that the top matching documents can be aggregated per bucket.

The top_hits aggregator can effectively be used to group result sets by certain fields via a bucket aggregator. One or more bucket aggregators determines by which properties a result set get sliced into.

*Not implemented yet*

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-top-hits-aggregation.html) for more information.