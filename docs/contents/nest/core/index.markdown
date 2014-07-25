---
template: layout.jade
title: Connecting
menusection: core
menuitem: index
---


# Indexing

Indexing is as simple as:

	var post = new Post() { Id = 12, ... }
	var status = client.Index<Post>(post);

Of course C# is smart enough to infer Post so

	var status = client.Index(post);

is sufficient. this will index post too `/[default index]/posts/12`. The typename`posts` is automatically inferred from the type.

If you need more control there are plenty of overloads, i.e:

	 _client.Index(post, i => i
				.Index(index)
				.Type(type)
				.Id(post.Id)
	);

## Asynchronous

Indexing asynchronously is as easy as:

	//IndexAsync returns a Task<ConnectionStatus>
	var task = client.IndexAsync(post);


## Aditional parameters

You can pass aditional data using `IndexParameters`

	client.Index(post, new IndexParameters() { VersionType = VersionType.External, Version = "212" });

Similarly to force a wait for a refresh 

	client.Index(post, new IndexParameters() { Refresh = true });

## Bulk Indexing

Instead of passing `T` just pass an `IEnumerable<T>` to `IndexMany()` or `IndexManyAsync()`.

**Note**
For asynchronous commands there's a special connection setting which automatically semaphores threaded communication
to Elasticsearch for you:

	var elasticSettings = new ConnectionSettings("127.0.0.1.", 9200)
							  .SetDefaultIndex("mpdreamz")
							  .SetMaximumAsyncConnections(20);

This ensures that at most there are 20 asynchronous connections to Elasticsearch. All others are enqueued until a slot is 
available.

## Aditional parameters
Like the overloads just taking a `T` the `IEnumerable<T>` has alot of overloads taking in extra parameters. 

	client.IndexMany(posts, new SimpleBulkParameters() { Refresh = true });

The reason the `IEnumerable<T>` overloads take a `SimpleBulkParameters` is because to pass item specific parameters you'll have to wrap `posts` in a `BulkParameters<T>` i.e:

	client.IndexMany(posts.Select(p=>new BulkParameters<T>(p) { Version = p.Version }));

This will do a bulk index on posts but use each individual posts version. Again there's plenty of overloads to mix and match:

	var bulkParams = posts.Select(p=>new BulkParameters<T>(p) { Version = p.Version });
	client.IndexMany(bulkParams , new SimpleBulkParameters() { Refresh = true });


 

