---
template: layout.jade
title: Connecting
menusection: core
menuitem: bulk
---


# Bulk

Nest long supported bulk index and deletes (through `IndexMany()` and `DeleteMany()`) but this shielded you from all that the elasticsearch `_bulk` api enpoint has to offer. Now you can use `Bulk()` to create any bulk request you'd like. E.g if you want to do index/create/delete's in a certain order.

# Examples

	var result = this._client.Bulk(b => b
		.Index<ElasticSearchProject>(i => i.Object(new ElasticSearchProject {Id = 2}))
		.Create<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 3 }))
		.Delete<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 4 }))
	);

Each bulk operation can also be anotated with the right behaviours:

	.Index<ElasticSearchProject>(i => i
		.Routing(...)
		.Refresh(...)
		.Percolate(...)
		.Parent(...)
		.Consistency(...)
		.Version(...)
		.VersionType(...)
		.Object(new ElasticSearchProject { Id = 2 })
	)

Another approach to writing a complex bulk call 

	var descriptor = new BulkDescriptor();
	foreach (var i in Enumerable.Range(0, 1000))
		descriptor.Index<ElasticSearchProject>(op => op.Object(new ElasticSearchProject {Id = i}));

	var result = this._client.Bulk(descriptor);