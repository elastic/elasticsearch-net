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

Here we create new a connection to our `node` and specify a `default index` to use when we don't explictly specify one. 
This can greatly reduce the places a magic string or constant has to be used.

**NOTE:** specifying defaultIndex is optional but NEST might throw an exception later on if no index is specified. In fact a simple `new ElasticClient()` is sufficient to chat with
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

That we would like to index in elasticsearch. Indexing is now as simple as calling.

    var person = new Person
    {
        Id = "1",
        Firstname = "Martijn",
        Lastname = "Laarman"
    };
    var index = client.Index(person);

This will index the object to `/my-application/person/1`. `NEST` is smart enough to infer the index and typename for the `Person` CLR type. It was also able to get the id of `1` through convention,  by looking for an `Id` property on the specified object. Which property it will use for the Id can also be specified using the `ElasticType` attribute.

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

`searchResults.Documents` now holds the first 10 people it knows who's first name is `Martijn`

Please see [the section on writing queries](http://localhost:8080/nest/writing-queries.html) for details on how NEST helps you write terse elasticsearch queries.

Again the same inferring rules apply as this will hit `/my-application/person/_search` and the same rule that inferring can be overridden also applies.

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

## Custom server side security while searching

Consider a scenario where you are using client side libraries like [elasticjs](https://github.com/fullscale/elastic.js) to construct to create User interfaces but want security to be provided by server side business logic, you can take this approach. You can route your queries through server side code.
```cs
    [RoutePrefix("api/Search")]
    public class SearchController : ApiController
    {
        [ActionName("_search")]
        public IHttpActionResult Post([FromBody]SearchDescriptor<dynamic> query)
        {
            var setting = new ConnectionSettings(new Uri(ConfigurationManager.AppSettings["SearchServerUri"])).SetDefaultIndex("informit");
            var client = new ElasticClient(setting);
    
            //Your server side security goes here
            var result = client.Search(q => query);
            return Ok(result);
        }
    }
```
The fragments `[RoutePrefix("api/Search")]` and `[ActionName("_search")]` will let you change your elastic search Url from http://localhost:9200/_search to http://yourwebsite/api/Search/_search and let things work as normal. The fragment `[FromBody]SearchDescriptor<dynamic> query` will convert the JSON query into NEST SearchDescriptor. The fragment `client.Search(q => query)` will execute the query. NOTE: `client.Search(query)` will compile but will NOT work.
