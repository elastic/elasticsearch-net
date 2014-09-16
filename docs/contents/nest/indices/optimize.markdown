---
template: layout.jade
title: Optimize
menusection: indices
menuitem: optimize
---


# Optimize 

The optimize API allows you to optimize one or more indices through an API. The optimize process basically optimizes the index for faster search operations (and relates to the number of segments a Lucene index maintains within each shard). The optimize operation allows you to specify the maximum number of segments to use during the optimization.

## Optimize all

	var r = this.ConnectedClient.Optimize();

## Optimize several indices with parameters

	var r = this.ConnectedClient.Optimize(new[] { "index", "index2" }, new OptimizeParams {MaximumSegments=2});

More overloads exists and all OptimizeParams are mapped. See [the original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-optimize.html) for parameters

