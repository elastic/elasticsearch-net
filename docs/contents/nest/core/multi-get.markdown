---
template: layout.jade
title: Multi Get
menusection: core
menuitem: multi-get
---


# Multi Get

You can use `GetMany<T>` to retrieve multiple documents of a single type by simply passing a collection containing their ids:

	var ids = new [] { 1, 2, 3 };
	var results = client.MultiGet(m => m.GetMany<ElasticsearchProject>(ids));

Index and type are inferred, but overloads still exists for full control:

	var results = client.MultiGet<ElasticsearchProject>("myalternateindex", "elasticprojs", ids);

If you need to retrieve multiple documents of different types, NEST also has you covered:

	var results = client.MultiGet(m => m
		.Get<ElasticsearchProject>(g => g.Id(1))
		.Get<Person>(g => g.Id(100))
		.Get<Person>(g => g.Id(105))
	);

This will get 1 `ElasticsearchProject` document and 2 `Person` documents in a single request.  The above could have also been written using a combination of `Get<T>` and `GetMany<T>`:

	var results = client.MultiGet(m => m
		.Get<ElasticsearchProject>(g => g.Id(1))
		.GetMany<Person>(new [] { 100, 105 })
	);

## Handling the Multi Get Response

`MultiGet` in NEST returns an `IMultiGetResonse` object which, similar to the request, also exposes a `Get<T>` and `GetMany<T>` that can be used for retrieving the documents.

You can pull the single `ElasticsearchProject` out of the response by using `Get<T>`:

	var hit = results.Get<ElasticsearchProject>(1);

And since we specified multiple `Person` documents in the above request, you can pull them all out of the response using `GetMany<T>`:

	var hits = results.GetMany<Person>(new[] { 100, 105 });

The result of `Get<T>` and `GetMany<T>` on the response object is an `IMultiGetHit<T>` and `IEnumerable<IMultiGetHit<T>>` respectively.

`IMultiGetHit<T>` contains the original document which can be found in the `Source` property, a `FieldSelection` collection containing specific fields if they were requested, and some additional meta data from Elasticsearch.

The `IMultiGetResponse` object also contains a `Documents` property of type `IEnumerable<IMultiGetHit<object>>` which holds *all* of the retrieved documents regardless of type.

## Field Selection

`MultiGet` also allows you to retrieve specific fields of a document:

	var results = client.MultiGet(m => m
		.Get<ElasticsearchProject>(g => g
			.Id(1)
			.Fields(p => p.Id, p => p.Followers.First().FirstName)
		)
		.Get<Person>(g => g.Id(100))
		.Get<Person>(g => g
			.Id(105)
			.Type("people")
			.Index("nest_test_data")
			.Fields(p => p.Id, p => p.FirstName)
		)
	);

Which can then be retrieved directly from the `IMultiGetResponse` object:

	var fields = results.GetFieldSelection<ElasticsearchProject>(1);
	var id = fields.FieldValues<int>(p => p.Id);
	var firstNames = fields.FieldValues<string[]>(p => p.Followers.First().FirstName);

Remember expressions like `p => p.Followers.First().FirstName` can be interchanged with `"followers.firstName"` if you prefer or need to reference a non-mapped field.
