---
template: layout.jade
title: Handling Responses
menusection: 
menuitem: esnet-handling-responses
---

# Handling Responses

Describes how to handle the the response objects from `Elasticsearch.Net`

#ElasticsearchResponse&lt;T&gt;

This is the container return type for all the API calls. It has the following properties

#### Success
The call succeeded and was succesfull (200 range). 
Note that even if you get a 200 back from Elasticsearch in many cases it's still recommended 
to check the actual response like did the call succeed on enough shards?

#### Error
When a call succeeds but does not return a http status code of 200 this property will have details on the error.
[Read more about error handling here](/elasticsearch-net/errors.html)
#### HttpStatusCode
#### RequestMethod
#### RequestUrl
#### Request 
The `byte[]` request that was sent to elasticsearch

#### ResponseRaw 
A `byte[]` representation of the response from elasticsearch, only set when `ExposeRawResponses()` is set 
[see the Connecting section](/elasticearch-net/connecting.html)

#### Response
The deserialized `T` object representing the response.

## Typed API Calls

`Elasticsearch.Net` does not provide typed objects representing the responses this is up to the developer to map. 

    var result = client.Search<MyType>()

In this example `MyType` is a type you provide to deserialize `Elasticsearch`'s response to. 

    var myTypeInstance = client.Response

If you specify `T` as `string` or `byte[]` the response will not go through the registered `ISerializer` but simply read and returned.

    var result = client.Search<string>();
    var stringResponse = result.Response;

This can be handy if you want to inspect the json dynamically by passing it into `JSON.NET`'s `JObject`. However `Elasticsearch.Net` also 
supports dynamic usecases out of the box.

## Dynamic API Calls

If you do not provide an explicit `<T>` for your return type `Elasticsearch.Net` will deserialize into a `DynamicDictionary`[\*]

    var result = client.Search();
    int? myInt = result.Response
        .hits.hits[2].nestedObject["someOtherValue"].myInt;

This will try and read `hits.hits[2].nestedObject.someOtherValue.myInt` from the search response and it won't throw null binding exceptions if i.e `nestedObject` does not exist in the second hit. 

This is really great for exploratory programming but dynamic dispatch in C# is not the fastest part of the language. It's highly recommended you try and map 
responses to an explicit object instead.

\* DynamicDictionary comes from the [Nancyfx](http://nancyfx.org/) project but is slightly modified to support arbitrary call depths without null checks in between.
