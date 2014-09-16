---
template: layout.jade
title: Index
menusection: core
menuitem: index
---


# Indexing

Indexing is as simple as:

	var post = new Post() { Id = 12, ... }
	var status = client.Index<Post>(post);

Of course C# is smart enough to infer `Post` so

	var status = client.Index(post);

is sufficient. This will index `post` to `/[default index]/posts/12`. The type name `posts` is automatically inferred from the type.

If you need more control, there are plenty of overloads, i.e:

	 client.Index(post, i => i
	 	.Index(index)
	 	.Type(type)
	 	.Id(post.Id)
	);

You can also construct the index request using the object initializer syntax instead:

	var request = new IndexRequest<Post>
	{
		Index = index,
		Type = type,
		Id = post.Id
	};

	client.Index<Post>(post);

## Asynchronous

Indexing asynchronously is as easy as:

	var task = client.IndexAsync(post); // IndexAsync returns a Task<ConnectionStatus>

## Bulk Indexing

See the section dedicated to using the [bulk api](bulk.html) for details on how to construct bulk indexing requests.
 

