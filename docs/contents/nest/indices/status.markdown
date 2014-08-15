---
template: layout.jade
title: Status
menusection: indices
menuitem: status
---


# Status

The status API allows to get comprehensive status information on one or more indices.

## Status for all indices

	var r = this.ConnectedClient.Status();

## Index / Indeces

	var r = this.ConnectedClient.Status("index");
	r = this.ConnectedClient.Status(new [] {"index4", "index2" });

## Typed (default index)

	var r = this.ConnectedClient.Status<ElasticSearchProject>();


In order to see the recovery status of shards, or the snapshot status, pass a StatusParams object. There is an overload for each Status method.

## Index with params

	var r = this.ConnectedClient.Status(new StatusParams { Recovery = true, Snapshot = true });


The StatusResponse object provides access to the status information of each index.

