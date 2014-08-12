---
template: layout.jade
title: Indices Exists
menusection: indices
menuitem: indices-exists
---


# Indices exists

Used to check if the index (indices) exists or not.

## Examples

### Fluent Syntax

	var result = client.IndexExists(i => i.Index("myindex"));

### Object Initializer Syntax

	var request = new IndexExistsRequest("myindex");
	var result = client.IndexExists(request);

## Handling the Index Exists response

`result` in the above examples is an `IExistsResponse` which contains a bool property `Exists`.

One thing to note is that if an index does not exist, Elasticsearch will return a `404`.  In this case, a `404` is a valid response and thus `result.IsValid` will be `true`.