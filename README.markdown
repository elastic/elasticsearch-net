NEST
========
*Strongly typed Elasticsearch client*

NEST aims to be a .net client with a very concise API.

Connecting
------------------

Basic plumbing:

	var elasticSettings = new ConnectionSettings("127.0.0.1.", 9200)
							  .SetDefaultIndex("mpdreamz");
	var client = new ElasticClient(elasticSettings);


Connecting can be done several ways:

	ConnectionStatus connectionStatus;
	if (!client.TryConnect(out connectionStatus))

Or if you don't care about error reasons

	if (client.IsValid)

Indexing
------------------

Indexing is as simple as:

	var post = new Post() { Id = 12, ... }
	client.Index<Post>(post);

of course C# is smart enough to infer Post so

	client.Index(post);

is sufficient. this will index post too /[default index]/posts/12. 'posts is inferred from the type.

if you need more control there are plenty of overloads, i.e:

	client.Index("index","type","id",post);

Indexing asynchronously is as easy as:

	client.IndexAsync(post, (c) => /* called later */);

or just:

	client.IndexAsync(post);

**Bulk indexing**

Instead of passing `T` just pass `IEnumerable<T>` for both Index or IndexAsync. A zero copy approach that writes directly on the post stream is planned in a later version.

*Note*
For asynchronous commands there's a special connection setting which automatically semaphores threaded communication
to ES for you:

	var elasticSettings = new ConnectionSettings("127.0.0.1.", 9200)
							  .SetDefaultIndex("mpdreamz")
							  .SetMaximumAsyncConnections(20);

this ensures that at most there are 20 asynchronous connections to ES others are enqueued until a slot is 
available.


Query DSL
--------------
This part is still evolving but currently looks like this:


	QueryResponse<Blog> queryResults = client.Search<Blog>
	(
		new Search()
		{
			Query = new Query(new 	Fuzzy("author.firstName", "shay", 1.0))
		}.Skip(0).Take(10)
	);


`queryResults.Documents` now yields all the blogs found (between 0 and 10 of course).



