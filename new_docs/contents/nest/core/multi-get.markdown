---
template: layout.jade
title: Connecting
menusection: core
menuitem: multi-get
---


# Multi Get

Get multiple documents in a single request.

##Examples

	var ids = new [] { hit1.Id, hit2.Id };
	var foundDocuments = this.ConnectedClient.MultiGet<ElasticSearchProject>(ids);

Index and type are infered but overloads exists for full control.

	var foundDocuments = this.ConnectedClient.MultiGet<ElasticSearchProject>("myalternateindex", "elasticprojs", ids);

# Multi Get Full

The previous calls are handy if you need to get many objects of a single type and don't care about the response or the metadata of the documents. If you do, NEST has you covered as well.

	var result = this._client.MultiGetFull(a => a
		.Get<ElasticSearchProject>(g => g.Id(1))
		.Get<Person>(g => g.Id(100000))
		.Get<Person>(g => g.Id(105))
	);

This will get 1 ElasticSearchProject document and 2 Person documents in one request. 

These could then be pulled out of the result:

	var person = result.Get<Person>(100000);
	var personHit = result.GetWithMetaData<Person>(100000);

`Get` returns `T` and `GetWithMetaData` returns a `MultiGetHit<T>` which also exposes the document's metadata such as `_index` and `_version`. 

In case the document was not found then `Get` would return a `null` but `GetWithMetaData` still returns the a `MultiGetHit<T>` but with an `.Exists` of `false` this maps to the way elasticsearch returns not found objects in a `multi_get` call.

You can even get field selections for some of the documents:

	var result = this._client.MultiGetFull(a => a
		.Get<ElasticSearchProject>(g => g.
			Id(1)
			.Fields(p => p.Id, p => p.Followers.First().FirstName)
		)
		.Get<Person>(g => g.Id(100000))
		.Get<Person>(g => g
			.Id(100)
			.Type("people")
			.Index("nest_test_data")
			.Fields(p => p.Id, p => p.FirstName)
		)
	);

You can then get the returned fields like so:

	var fields = result.GetFieldSelection<ElasticSearchProject>(1);
	var id = fields.FieldValue<int>(p => p.Id);
	var firstNames = fields.FieldValue<string[]>(p => p.Followers.First().FirstName);

Remember `p => p.Followers.First().FirstName` can be interchanged with `"followers.firstName"` if you prefer or need to reference a non-mapped field.
