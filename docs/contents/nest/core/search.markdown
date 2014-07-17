---
template: layout.jade
title: Connecting
menusection: core
menuitem: search
---


# Search

Search is **THE** call you'll probably use the most as it exposes Elasticsearch's key functionality: search!

**NOTE:** be sure to also read [how to use NEST's Query DSL](/concepts/writing-queries.html)


You can start a search like so, 

    var result = this._client.Search(s => s
        .Index("my-index")
        .Type("my-type")
 		.Query(q=> ....)
 		.Filter(f=> ....)	     
    );

This will get all the documents on the `my-index` index and `my-type` type. 

`result` is an `IQueryResponse<dynamic>` which has a `Documents` `IEnumerable<dynamic>` property that holds all the results. NOTE: remember in the absense of paging it will default to the first 10). 

## Typed results

Dynamic may or may not be what you want but in general it pays to type your search responses. In alot of cases you already need to have the POCO classes for indexing anyway. 

    var result = this._client.Search<ElasticSearchProject>(s => s
        .MatchAll()
    );

Now `result` is a `IQueryResponse<ElasticSearchProject>` and its Documents property will hold an `IEnumerable<ElasticSearchProject>`. 

If you are wondering why I am not specifying `Index()` and `Type()` here read the primer on [index and type name inference](/concepts/index-type-inference.html)








