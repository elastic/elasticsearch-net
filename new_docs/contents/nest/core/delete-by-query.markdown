---
template: layout.jade
title: Connecting
menusection: core
menuitem: delete-by-query
---


# Delete by Query

	ConnectedClient.DeleteByQuery<object>(q => q.Query(rq => rq.Term(f => f.Name, "elasticsearch.pm")));

Elasticsearch allows you to delete over multiple types and indexes, so does NEST.

	ConnectedClient.DeleteByQuery<ElasticSearchProject>(q => q
                .Indices(new[] {"index1", "index2"})
                .Query(rq => rq.Term(f => f.Name, "elasticsearch.pm"))
                );

As always `*Async` variants are available too.

You can also delete by query over all the indices and types:

	ConnectedClient.DeleteByQuery<ElasticSearchProject>(q => q
                .AllIndices()
                .Query(rq => rq.Term(f => f.Name, "elasticsearch.pm"))
                );

The DeleteByQuery can be further controlled by passing a `DeleteByQueryParameters` object

	ConnectedClient.DeleteByQuery<object>(
                q => q.Query(rq => rq
                    .Term(f => f.Name, "elasticsearch.pm"))
                    .Routing("nest")
                    .Replication(ReplicationOptions.Sync)
                    
