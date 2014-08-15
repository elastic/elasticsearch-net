---
template: layout.jade
title: Bulk
menusection: core
menuitem: bulk
---


# Bulk

NEST long supported bulk index and deletes (through `IndexMany()` and `DeleteMany()`) but this shielded you from all that the Elasticsearch `_bulk` api enpoint has to offer. Now you can use `Bulk()` to create any bulk request you'd like. E.g if you want to do index/create/delete's in a certain order.

## Examples

	var result = client.Bulk(b => b
		.Index<ElasticSearchProject>(i => i
			.Document(new ElasticSearchProject {Id = 2})
		)
		.Create<ElasticSearchProject>(c => c
			.Document(new ElasticSearchProject { Id = 3 })
		)
		.Delete<ElasticSearchProject>(d => d
			.Document(new ElasticSearchProject { Id = 4 })
		)
	);

Each bulk operation can also be annotated with the right behaviours:

	.Index<ElasticSearchProject>(i => i
		.Routing(...)
		.Refresh(...)
		.Percolate(...)
		.Parent(...)
		.Consistency(...)
		.Version(...)
		.VersionType(...)
		.Document(new ElasticSearchProject { Id = 2 })
	)

Another approach to writing a complex bulk call:

	var descriptor = new BulkDescriptor();

	foreach (var i in Enumerable.Range(0, 1000))
	{
		descriptor.Index<ElasticSearchProject>(op => op
			.Document(new ElasticSearchProject {Id = i})
		);
	}

	var result = client.Bulk(descriptor);

### Object Initializer Syntax

Bulk calls can also be constructed using the object initializer syntax:

	var project = new ElasticsearchProject { Id = 4, Name = "new-project" };

	var request = new BulkRequest()
	{
		Refresh = true,
		Consistency = Consistency.One,
		Operations = new List<IBulkOperation>
		{
			{ new BulkIndexOperation<ElasticsearchProject>(project) { Id= "2"} },
			{ new BulkDeleteOperation<ElasticsearchProject>(6) },
			{ new BulkCreateOperation<ElasticsearchProject>(project) { Id = "6" } },
			{ new BulkUpdateOperation<ElasticsearchProject, object>(project, new { Name = "new-project2"}) { Id = "3" } },
		}
	};

	var response = client.Bulk(request);

