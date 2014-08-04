---
template: layout.jade
title: Delete
menusection: core
menuitem: delete
---


# Deleting

The delete API allows to delete a typed JSON document from a specific index based on its id. See also [deleting by query]({{root}}/core/delete-by-query.html) for other ways to delete data.


## By Id

	client.Delete<ElasticSearchProject>(1);
	client.DeleteAsync<ElasticSearchProject>(1);

## Delete with custom parameters

### Fluent Syntax

	client.Delete(1, d => d
		.Type("users")
		.Index("myindex")
	);

### Object Initializer Syntax

	// Be explicit with type and index
	client.Delete(new DeleteRequest("myindex", "users", "1"));

	// Infer type and index from CLR type
	client.Delete(new DeleteRequest<ElasticsearchProject>("1"));

## By object (T)

Id property is inferred (can be any value type (int, string, float ...))

	client.Delete(searchProject);
	client.DeleteAsync(searchProject);

## By IEnumerable<T>

	client.DeleteMany(searchProjects);
	client.DeleteManyAsync(searchProjects);

## By Query

See [deleting by query]({{root}}/core/delete-by-query.html)

## Indices and Mappings

See [delete mapping]({{root}}/indices/delete-mapping.html) and [delete index]({{root}}/indices/delete-index.html)

## Bulk delete

See [bulk]({{root}}/code/bulk.html)

