---
template: layout.jade
title: Connecting
menusection: concepts
menuitem: handling-responses
---


# Handling responses

All the return objects from API calls in NEST client implement:
	
	public interface IResponse
	{
		bool IsValid { get; }
		IElasticsearchResponse ConnectionStatus { get; }
		ElasticInferrer Infer { get; }
	}

`IsValid` will return whether a response is valid or not. A response is usually only valid when an HTTP return result in the 200 range was returned. Some calls allow for 404 to be a valid response too however.

If a response returns 200 in Elasticsearch sometimes it will contain more information on the validity of the call inside its response. It's highly recommended to read the documentation for a call and check for these properties on the responses as well. 

`ConnectionStatus` is the response as it was returned by `Elasticsearch.net`. It's section on 
[handling responses](/elasticsearch-net/handling-responses.html) applies here as well.

## Typed responses

`NEST` does not provide typed objects representing the responses, this is up to the developer to map. 

	var search = client.Search<T>();
	var results = search.Documents;

Here `T` is a type you provide to deserialize `NEST`s reponse to. The resuls are then available as an `IEnumerable<T>` at `Search<T>().Documents`.

## Dynamic reponses

If you do not provide an explicit type `<T>` as your return type, `NEST` will serialize the results into a `DynamicDictionary`. 

	var search = client.Search();
	int? my_id = (int?)search.Hits.Hits[0].Source["SomeValue"];

This won't throw a null binding exception. This is really great for exploratory programming but dynamic dispatch in C# is not the fastest part of the language. It's highly recommended you try and map responses to an explicit object instead.

## Raw response

If you want to isntead get the raw JSON string returned, you can do so by not providing an explicit type `<T>`. The raw string is then available in `ConnectionStatus.Result`.

	var search = client.Search();
	var json = search.ConnectionStatus.Result;

This is useful if you want to work with the json dynamically by passing it into a json serializer, such as `JSON.NET`. 