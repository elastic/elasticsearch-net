---
layout: default
title: Connecting
menu_section: indices
menu_item: optimize
---


# Optimize 

The optimize API allows to optimize one or more indices through an API. The optimize process basically optimizes the index for faster search operations (and relates to the number of segments a lucene index within each shard). The optimize operation allows to optimize the number of segments to optimize to.

## Optimize all

	var r = this.ConnectedClient.Optimize();

## Optimize several indices with parameters

	var r = this.ConnectedClient.Optimize(new[] { "index", "index2" }, new OptimizeParams {MaximumSegments=2});

More overloads exists and all OptimizeParams are mapped. See [the original docs](http://www.elasticsearch.org/guide/reference/api/admin-indices-optimize.html) for parameters

