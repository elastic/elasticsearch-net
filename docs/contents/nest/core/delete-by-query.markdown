---
template: layout.jade
title: Delete By Query
menusection: core
menuitem: delete-by-query
---


# Delete by Query

	client.DeleteByQuery<ElasticsearchProject>(q => q
		.Query(rq => rq
			.Term(f => f.Name, "elasticsearch.pm")
		)
	);

Elasticsearch allows you to delete over multiple types and indexes, so does NEST.

	client.DeleteByQuery<ElasticSearchProject>(q => q
		.Indices(new[] {"index1", "index2"})
		.Query(rq => rq
			.Term(f => f.Name, "elasticsearch.pm")
		)
	);

As always `*Async` variants are available too.

You can also delete by query over all the indices and types:

	client.DeleteByQuery<ElasticSearchProject>(q => q
		.AllIndices()
		.Query(rq => rq
			.Term(f => f.Name, "elasticsearch.pm")
		)
	);

The DeleteByQuery can be further controlled...

	client.DeleteByQuery<ElasticSearchProject>(q => q
		.Query(rq => rq
			.Term(f => f.Name, "elasticsearch.pm")
		)
		.Routing("nest")
		.Replication(Replication.Sync)
	);

### Object Initializer Syntax

The above can also be accomplished using the object initializer syntax:

	var request = new DeleteByQueryRequest<ElasticsearchProject>
	{
		Query = new QueryContainer(
				new TermQuery
				{
					Field = "name",
					Value = "elasticsearch.pm"
				}
			)
		,
		Routing = "nest",
		Replication = Replication.Sync
	};

	client.DeleteByQuery(request);


                    
