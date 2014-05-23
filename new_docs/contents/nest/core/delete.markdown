---
template: layout.jade
title: Connecting
menusection: core
menuitem: delete
---


# Deleting

The delete API allows to delete a typed JSON document from a specific index based on its id. See also [deleting by query]({{root}}/core/delete-by-query.html) for other ways to delete data.


## By Id

	this.ConnectedClient.Delete<ElasticSearchProject>(searchProject.Id);
	this.ConnectedClient.DeleteAsync<ElasticSearchProject>(searchProject.Id);

## Delete with custom parameters

	_client.Delete(request.Id, s => s
		.Type("users")
		.Index("myindex")
	);

## By object (T)

Id property is inferred (can be any value type (int, string, float ...))

	this.ConnectedClient.Delete(searchProject);
	this.ConnectedClient.DeleteAsync(searchProject);

## By IEnumerable<T>

	this.ConnectedClient.DeleteMany(searchProjects);
	this.ConnectedClient.DeleteManyAsync(searchProjects);

## By IEnumerable<T> using bulkparameters

Using bulk parameters you can control the deletion of each individual document further

			var bulkedProjects = searchProjects
				.Select(d=> new BulkParameters<ElasticSearchProject>(d) 
				{ 
					Version = d.Version, 
					VersionType = VersionType.External 
				}
			);
            this.ConnectedClient.DeleteMany(bulkedProjects, new SimpleBulkParameters() { Refresh = true });


## By Query

Please see [deleting by query]({{root}}/core/delete-by-query.html)

## Indices and Mappings

Please see [delete mapping]({{root}}/indices/delete-mapping.html)

and [delete index]({{root}}/indices/delete-index.html)
