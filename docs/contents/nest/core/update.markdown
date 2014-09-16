---
template: layout.jade
title: Update
menusection: core
menuitem: update
---


# Update

The update API allows to update a document based on a script provided. The operation gets the document (collocated with the shard) from the index, runs the script (with optional script language and parameters), and index back the result (also allows to delete, or ignore the operation). It uses versioning to make sure no updates have happened during the "get" and "reindex".

Note, this operation still means full reindex of the document, it just removes some network roundtrips and reduces chances of version conflicts between the get and the index. The _source field need to be enabled for this feature to work.

## By Script

	client.Update<ElasticsearchProject>(u => u
		.Id(1)
		.Script("ctx._source.country = country")
		.Params(p => p
			.Add("country", "United States")
		)
		.RetryOnConflict(3)
		.Refresh()
	);

## By Partial Document

The update API also has a `.Update<T, K>` variant, where `T` is the document type to update, and `K` is the partial document to merge.

	public class PartialElasticsearchProject
	{
		public string Country { get; set }
	}

	client.Update<ElasticsearchProject, PartialElasticsearchProject>(u => u
		.Id(1)
		.Doc(new PartialElasticsearchProject { Country = "United States"})
		.RetryOnConflict(3)
		.Refresh()
	);

### Anonymous objects as partial documents

Notice in the example above we created a custom partial object `PartialElasticsearchProject`, which only contains a `Country` property, to apply the partial update.  The reason for this is that if we used the same types for both our document (`T`) and partial document (`K`) (i.e., `typeof(T) == typeof(K)`) then `K` would have to be fully populated with all of its values, otherwise the existing document in the index will get overriden by C# defaults for each property that wasn't populated.

Due to this, a common use case is to just use an anonymous object as your partial document:

	client.Update<ElasticsearchProject, object>(u => u
		.Id(1)
		.Doc(new { Country = "United States"})
		.RetryOnConflict(3)
		.Refresh()
	);

## Upserting

You can insert the partial object passed to `Doc` into your index if it doesn't already exist by using the `DocAsUpsert` method:

	client.Update<ElasticsearchProject, object>(u => u
		.Id(1)
		.Doc(new { Country = "United States"})
		.DocAsUpsert()
	);

Or you can pass an entirely new document to be upserted by using `Upsert`:

	client.Update<ElasticsearchProject, object>(u => u
		.Id(1)
		.Doc(new { Country = "United States"})
		.Upsert(new ElasticsearchProject { Id = 1, Country = "United States" })
	);


## Id inferrence

In all of the above examples, we explicitly specified the id of the document in which we wanted to update.  Alternatively, you can specify `IdFrom`, which will allow NEST to infer the id from an object instance:

	client.Update<ElasticsearchProject, object>(u => u
		.IdFrom(elasticsearchProject)
		.Doc(new { Country = "United States"})
		.DocAsUpsert()
	);