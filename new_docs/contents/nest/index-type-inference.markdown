---
template: layout.jade
title: Connecting
menusection: concepts
menuitem: index-type-inference
---

## Index and Type inference. 
When setting up a connection to elasticsearch you can define a default index to be used see [the connecting](/concepts/connecting.html) section of the help. NEST will use this whenever an index is not explictly stated. This will helps tremendously to cut back on the verbose nature of having to supply the same string over and over.

Similarly for types NEST will lowercase and pluralize the typenames in the absence of an explicit type parameter. The behavior of lowercasing and pluralizing is overridable using the `TypeNameInferrer` setting on `ConnectionSettings` if you have a different generic scheme that works better for you. 

On the connection settings you can even set up the elasticsearch typename for the CLR typename explicitly globally. See MapTypeIndices section on [the connecting help page](/concepts/connecting.html) 

To summarize: NEST is set up in a way that you don't have to sprinkle index and typename strings through your entire codebase.

So whenever you find yourself writing:

    var result = this._client.Search<ElasticSearchProject>(s => s
        .Index("my-index")
        .Type("my-type")
        .MatchAll()
    );

alot, taking of adventage of `ConnectionSettings`

    var result = this._client.Search<ElasticSearchProject>(s => s
        .MatchAll()
    );

could suffice.

