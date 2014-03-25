---
template: layout.jade
title: Connecting
menusection: core
menuitem: get
---


# Get a document

gets a single document from Elasticsearch

## By Id

	var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(hit.Id);

index and type are infered but overloads exists for full control

	var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>("myalternateindex", "elasticprojs", hit.Id);

## Full response

The `Get<T>()` call immediatly returns `T` which is handy in alot of cases but sometimes you'll want to get the complete metadata object back from Elasticsearch
using `GetFull()` you get a proper `IGetResponse<T>` back that holds the usual `IsValid` and `ConnectionStatus` properties amongst the `Index`, `Type`, `Id` and `Version` properties.

	var result = this._client.GetFull<ElasticSearchProject>(g => g
		.Index("nest_test_data")
		.Type("elasticsearchprojects")
		.Id(1)
	);

`result.Document` now holds the ElasticSearchProject with id 1.

`Index()` and `Type()` are optional

	var result = this._client.GetFull<ElasticSearchProject>(g => g
		.Id(1)
	);

Follows the same inferring rules as `.Get(id)` would.

Infact you could even just pass an object:

    var result = this._client.GetFull<SomeDto>(g => g
		.Object(new SomeDto { AlternateId = Guid.NewGuid() })
	);

provided SomeDto is mapped properly to use `AlternateId` as the alternate id field

	[ElasticType(IdProperty = "AlternateId")]
	internal class SomeDto
	{
		public Guid AlternateId { get; set; }
	}

You can also use GetFull to query just some fields of a single document

	var result = this._client.GetFull<ElasticSearchProject>(g => g
		.Index("nest_test_data")
		.Type("elasticsearchprojects")
		.Id(1)
		.Fields(p=>p.Content, p=>p.Name, p=>p.Id, p=>p.DoubleValue)
	);

These fields are exposed as followed:

	var name = result.Fields.FieldValue<string>(p => p.Name);
	var id = result.Fields.FieldValue<int>(p => p.Id);
	var doubleValue = result.Fields.FieldValue<double>(p => p.DoubleValue);


Remember `p => p.Name` can also be written as `"name"` and Nest does not force you to write expressions everywhere (although it is much encouraged!).


 

