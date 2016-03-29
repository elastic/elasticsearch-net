---
template: layout.jade
title: Quick Start
menusection: 
menuitem: quick-start
---

# Quick Start

`NEST` is a high level `elasticsearch` client that still maps very closely to the original `elasticsearch` API. 
Requests and Responses have been mapped to CLR objects and `NEST` also comes with a powerful strongly typed query dsl.

## Installing

From the package manager console inside visual studio 

    PM> Install-Package NEST

Or search in the Package Manager UI for `NEST` and go from there

## Connecting

Assumming Elasticsearch is already installed and running on your machine, go to http://localhost:9200 in your browser. You should see a similar response to this:

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

To connect to your local node using NEST, simply:

    var node = new Uri("http://localhost:9200");

    var settings = new ConnectionSettings(
        node, 
        defaultIndex: "my-application"
    );

    var client = new ElasticClient(settings);

Here we create new a connection to our `node` and specify a `default index` to use when we don't explictly specify one. 
This can greatly reduce the places a magic string or constant has to be used.

**NOTE:** specifying `defaultIndex` is optional but NEST might throw an exception later on if no index is specified. In fact a simple `new ElasticClient()` is sufficient to chat with
`http://localhost:9200` but explicitly specifying connection settings is recommended.

`node` here is a `Uri` but can also be an `IConnectionPool` see the 
[Elasticsearch.net section on connecting](/elasticsearch-net/connecting.html)

## Indexing

Now imagine we have a Person [POCO](http://en.wikipedia.org/wiki/Plain_Old_CLR_Object)
   
    public class Person
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

That we would like to index in elasticsearch. Indexing is now as simple as calling.

    var person = new Person
    {
        Id = "1",
        Firstname = "Martijn",
        Lastname = "Laarman"
    };

    var index = client.Index(person);

This will index the object to `/my-application/person/1`. `NEST` is smart enough to infer the index and typename for the `Person` CLR type. It was also able to get the id of `1` through convention,  by looking for an `Id` property on the specified object. Which property it will use for the Id can also be specified using the `ElasticType` attribute.

The default index and type names are configurable per type. See the [nest section on connecting](/nest/connecting.html).

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

`searchResults.Documents` now holds the first 10 people it knows whose first name is `Martijn`

Please see [the section on writing queries](/nest/writing-queries.html) for details on how NEST helps you write terse elasticsearch queries.

Again, the same inferring rules apply as this will hit `/my-application/person/_search` and the same rule that inferring can be overridden also applies.

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

## Object Initializer Syntax

As you can see from the previous examples, NEST provides a terse, fluent syntax for constructing API calls to Elasticsearch.  However, fear not if lambdas aren't your thing, you can now use the new object initializer syntax (OIS) introduced in 1.0.  

The OIS is an alternative to the familair fluent syntax of NEST and works on all API endpoints.  Anything that can be done with the fluent syntax can now also be done using the OIS.

For example, the earlier indexing example above can be re-written as:

    var indexRequest = new IndexRequest<Person>(person)
    {
        Index = "another-index",
        Type = "another-type",
        Id = "1-should-not-be-the-id",
        Refresh = true,
        Ttl = "1m"
    };

    var index = client.Index(indexRequest);

And searching...

    QueryContainer query = new TermQuery
    {
        Field = "firstName",
        Value = "martijn"
    };

    var searchRequest = new SearchRequest
    {
        From = 0,
        Size = 10,
        Query = query
    };

    var searchResults = Client.Search<Person>(searchRequest);


Many of the examples throughout this documentation will be written in both forms.


