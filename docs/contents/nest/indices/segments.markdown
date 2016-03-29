---
template: layout.jade
title: Segments
menusection: indices
menuitem: segments
---


# Segments
Provide low level segments information that a Lucene index (shard level) is built with. Allows to be used to provide more information on the state of a shard and an index, possibly optimization information, data “wasted” on deletes, and so on.

## All indices segments information

	var r = this.ConnectedClient.Segments();
	r.Indices[this.Settings.DefaultIndex].Shards["0"].Segments["_l"].SizeInBytes;


