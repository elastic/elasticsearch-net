---
template: layout.jade
title: Connecting
menusection: core
menuitem: delete-by-query
---


# Delete by Query

	this.ConnectedClient.DeleteByQuery<ElasticSearchProject>(q => q.Term(f => f.Name, "elasticsearch.pm"));

Elasticsearch allows you to delete over multiple types and indexes, so does NEST.

	this.ConnectedClient.DeleteByQuery<ElasticSearchProject>(q => q
		.Indices(new[] { "index1", "index2" })
		.Term(f => f.Name, "elasticsearch.pm")
	);

As always `*Async` variants are available too.

You can also delete by query over all the indexes and types:

	this.ConnectedClient.DeleteByQuery<ElasticSearchProject>(q => q
		.AllIndices()
		.Term(f => f.Name, "elasticsearch.pm")
	);

The DeleteByQuery can be further controlled by passing a `DeleteByQueryParameters` object

	this.ConnectedClient.DeleteByQuery<ElasticSearchProject>(
		q => q.Term(f => f.Name, "elasticsearch.pm")
		, new DeleteByQueryParameters { Consistency = Consistency.Quorum, Replication = Replication.Sync, Routing = "NEST" }	
	);
