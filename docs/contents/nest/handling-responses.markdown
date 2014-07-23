---
template: layout.jade
title: Handling Responses
menusection: concepts
menuitem: handling-responses
---


# Handling Responses


All the return objects from API calls in NEST implement:
	
	public interface IResponse
	{
		bool IsValid { get; }
		IElasticsearchResponse ConnectionStatus { get; }
		ElasticInferrer Infer { get; }
	}

`IsValid` will return whether a response is valid or not. A response is usually only valid when an HTTP return result in the 200 range was returned. Some calls allow for 404 to be a valid response too, however.

If a response returns 200 in Elasticsearch, sometimes it will contain more information on the validity of the call inside its response. It's highly recommended to read the Elasticsearch documentation for a call and check for these properties on the response as well. 

`ConnectionStatus` is the response as it was returned by `Elasticsearch.Net`. Its section on 
[handling responses](/elasticsearch-net/handling-responses.html) applies here as well.
