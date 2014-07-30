---
template: layout.jade
title: Building Requests
menusection: 
menuitem: esnet-building-requests
---

# Building Requests

`Elasticsearch.Net` maps **all** the Elasticsearch API endpoints to methods. The reason it can do this is because all these methods are generated from 
[the official client REST specification](https://github.com/elasticsearch/elasticsearch/tree/master/rest-api-spec/api). This specification documents all 
the URL's (paths and querystrings) but does not map any of the API request and response bodies.

    client.GetSource("myindex","mytype","1",qs=>qs
        .Routing("routingvalue")
    );

Which will do a `GET` request on `/myindex/mytype/1/_source?routing=routingvalue`. 
All the methods and arguments are fully documented based on the documentation of the specification. 

As you can see `Elasticsearch.Net` also strongly types the querystring parameters it knows exist on an endpoint with full Intellisense documentation. 
Unknown querystring parameters can still be added:

    client.GetSource("myindex","mytype","1",qs=>qs
        .Routing("routingvalue")
        .Add("key","value")
    );

The querystring parameter is always optional.

## Providing a Request Body

Some endpoints need a request body which can be passed in a couple of ways.

### String

    var myJson = @"{ ""hello"" : ""world"" }";
    client.Index("myindex","mytype","1", myJson);

This will call `POST` on `/myindex/mytype/1` with the provided string `myJson` passed verbatim as the request body

### (Anonymous) Object

    var myJson = new { hello = "world" };
    client.Index("myindex","mytype","1", myJson);

This will call `POST` on `/myindex/mytype/1` where `myJson` will be serialized by the registered `ISerializer`

**Side note:** if you need `PUT` semantics `IndexPut()` also exists. `Elasticsearch.Net` exposes all the endpoints with all the allowed
HTTP methods.

### IEnumerable&lt;object&gt; 

Some API endpoints in Elasticsearch follow a strict special json format. 

    line_of_json_with_no_enters \n
    json_payload_with_enters
    line_of_json_with_no_enters \n
    json_payload_with_enters
    line_of_json_with_no_enters \n
    json_payload_with_enters
    .....

Examples of such endpoints are the [bulk api](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html#docs-bulk).  In `Elasticsearch.Net` you can call these with

    var bulk = new object[]
    {
        new { index = new { _index = "test", _type="type", _id = "1"  }},
        new
        {
            name = "my object's name"
        }
    };
    client.Bulk(bulk);

`Elasticsearch.Net` will know not to serialize the passed object as `[]` but instead serialize each seperately and joining them up with `\n`. 
No request in Elasticsearch expects an array as root object for the request.



