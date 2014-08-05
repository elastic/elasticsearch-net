---
template: layout.jade
title: Delete Indices
menusection: indices
menuitem: delete-indices
---


# Delete Index
The delete index API allows you to delete an existing index.

The delete index API can also be applied to more than one index, or on `_all` indices (be careful!). All indices will also be deleted when no specific index is provided. In order to disable allowing to delete all indices, set `action.disable_delete_all_indices` setting in the config to `true`.

## Examples
Using the default index

	this.ConnectedClient.DeleteIndex<ElasticSearchProject>()


or more explictly

	this.ConnectedClient.DeleteIndex("index")

