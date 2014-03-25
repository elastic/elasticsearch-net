---
template: layout.jade
title: Quick Start
menusection: 
menuitem: quick-start
---

# Quick Start

`NEST` is a high level `elasticsearch` client that still maps very close to the original `elasticsearch` API. 
Requests and Responses have been mapped to CLR objects and `NEST` also comes with a powerful strongly typed query dsl.

## Installing

From the package manager console inside visual studio 

    PM > Install Package NEST -PreRelease

Or search in the Package Manager UI for `NEST` and go from there

## Connecting

hit http://localhost:9200 in the browser you should see a similar response to this:

    {
      "status" : 200,
      "name" : "Sin-Eater",
      "version" : {
        "number" : "1.0.0",
        "build_hash" : "a46900e9c72c0a623d71b54016357d5f94c8ea32",
        "build_timestamp" : "2014-02-12T16:18:34Z",
        "build_snapshot" : false,
        "lucene_version" : "4.6"
      },
      "tagline" : "You Know, for Search"
    }

To connect to your local node from C# simply:

    var node = new Uri("http://localhost:9200");
    var settings = new ConnectionSettings(
        node, 
        defaultIndex: "my-application"
    );
    var client = new ElasticClient(settings);

Here we create new connection to our `node` and specify a `default index` to use when we don't explictly specify one. 
This can greatly reduce the places a magic string or constant has to be used.

**NOTE:** specifying defaultIndex is optional but NEST might throw later on if no index is specified. In fact a simple `new ElasticClient()` is sufficient to chat with
`http://localhost:9200` but explicitly specifying connection settings is recommended.

`node` here is a `Uri` but can also be an `IConnectionPool` see the 
[Elasticsearch.net section on connecting](http://localhost:8080/elasticsearch-net/connecting.html)

## Indexing

Now imagine we have a Person [POCO](http://en.wikipedia.org/wiki/Plain_Old_CLR_Object)
   
    public class Person
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

That we would like to index in elasticsearch indexing is now as simple as calling.

    var person = new Person
    {
        Id = "1",
        Firstname = "Martijn",
        Lastname = "Laarman"
    };
    var index = client.Index(person);

This will index the object to `/my-application/person/1`. `NEST` is smart enough to infer the index and typename for the `Person` CLR type. It was also able to get the id of `1` by convention of looking for `Id` property on the specified object. Where it will look for the Id can be specified using the `ElasticType` attribute.

The default index and type names are configurable per type see the [nest section on connecting](/nest/connecting.html)

Image you want to override all the defaults for this one call, you should be able to do this with `NEST` and yes you can. `NEST` inferring is very powerful but if you want to pass explicit values you can **always** do so.

    var index = client.Index(person, i=>i
        .Index("another-index")
        .Type("another-type")
        .Id("1-should-not-be-the-id")
        .Refresh()
        .Ttl("1m")
    );

This will index the document using `/another-index/another-type/1-should-not-be-the-id?refresh=true&&ttl=1m` as the url. 

## Searching

Now that we have indexed some documents we can begin to search for them. 

    var searchResults = client.Search<Person>(s=>s
        .From(0)
        .Size(10)
        .Query(q=>q
             .Term(p=>p.Firstname, "martijn")
        )
    );

`searchResults.Documents`now holds the first 10 people it knows who's first name is `Martijn`

Please see [the section on writing queries](http://localhost:8080/nest/writing-queries.html) how NEST helps you write terse elasticsearch queries.

Again the same inferring rules apply as this will hit `/my-application/person/_search` and the same rule that inferring can be overruled also applies.

    // uses /other-index/other-type/_search
    var searchResults = client.Search<Person>(s=>s
        .Index("other-index")
        .OtherType("other-type")
    );
    
    // uses /_all/person/_search
    var searchResults = client.Search<Person>(s=>s
       .AllIndices()
    );
    
    // uses /_search
    var searchResults = client.Search<Person>(s=>s
        .AllIndices()
        .AllTypes() 
    );

