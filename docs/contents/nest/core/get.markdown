---
template: layout.jade
title: Get
menusection: core
menuitem: get
---


# Get a document

Gets a single document from Elasticsearch

## By Id

	var response = client.Get<ElasticSearchProject>(1);

Index and type are inferred but overloads still exists for full control:

	var response = client.Get<ElasticSearchProject>("myindex", "mytype", 1);

## Handling the Get response

The `Get<T>()` call returns an `IGetResponse<T>` that holds the requested document as well as other meta data returned from elasticsearch.

`response.Source` holds the ElasticSearchProject with id `1`.

You can also use `Get<T>()` to query just some fields of a single document:

### Fluent Syntax

	var response = client.Get<ElasticsearchProject>(g => g
		.Index("myindex")
		.Type("mytype")
		.Id(1)
		.Fields(p=>p.Content, p=>p.Name, p=>p.Id, p=>p.DoubleValue)
	);

### Object Initializer Syntax

	var request = new GetRequest("myindex", "mytype", "1")
	{
		Fields = new PropertyPathMarker[] { "content", "name", "id" }
	};

	var response = client.Get<ElasticsearchProject>(request);
	

You can then access the fields like so:

	var name = response.Fields.FieldValue<string>(p => p.Name);
	var id = response.Fields.FieldValue<int>(p => p.Id);
	var doubleValue = response.Fields.FieldValue<double>(p => p.DoubleValue);

Remember `p => p.Name` can also be written as `"name"` and NEST does not force you to write expressions everywhere (although it is much encouraged!).


 

