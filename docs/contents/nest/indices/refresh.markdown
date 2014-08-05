---
template: layout.jade
title: Refresh
menusection: indices
menuitem: refresh
---


# Refresh

The refresh API allows to explicitly refresh one or more index, making all operations performed since the last refresh available for search. The (near) real-time capabilities depends on the index engine used. For example, the robin one requires refresh to be called, but by default a refresh is scheduled periodically.

## Refresh all

	var r = this.ConnectedClient.Refresh();

## Index / Indeces

	var r = this.ConnectedClient.Refresh("index");
	r = this.ConnectedClient.Refresh(new [] {"index4", "index2" });

## Typed (default index)

	var r = this.ConnectedClient.Refresh<ElasticSearchProject>();


