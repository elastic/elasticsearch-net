---
template: layout.jade
title: Connecting
menusection: indices
menuitem: delete-mapping
---


# Delete Mapping
Allow to delete a mapping (type) along with its data. The REST endpoint is `/{index}/{type}` with `DELETE` method.

Note, most times, it make more sense to reindex the data into a fresh index compared to delete large chunks of it.

## Examples
Using the default index and the inferred type name

	this.ConnectedClient.DeleteMapping<ElasticSearchProject>()


or more explictly:

	this.ConnectedClient.DeleteMapping<ElasticSearchProject>("alternateindex","alternatetypename")


