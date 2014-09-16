---
template: layout.jade
title: Search Basics
menusection: search
menuitem: basics
---

# Search



## From / Size

Pagination can be done using `From()` and `Size()`:

	.From(0)
	.Size(50)

Alternatively,

	.Skip(0)
	.Take(50)

can also be used.

## Indices / Types

NEST will automatically infer the index to search based on the type specified:

	client.Search<MyObject>(s => s
	    ...
	)

If `MyObject` has not been mapped to an index, then the default index will be used.

You can override type inferrence by explicitly specifying the index to search on:

	client.Search<MyObject>(s => s.
	    .Index("Index_A")
	    ...
	)

Remember that since Elasticsearch 19.8 you can also specify wildcards on index names:

	client.Search<MyObject>(s => s
	    .Index("Index_*")
	    ...
	)
	.Types(new [] { typeof(ElasticSearchProject)})

You can also tell NEST to search multiple indices:

	client.Search<MyObject>(s => s
	    .Indices(new [] {"Index_A", "Index_B"})
	    ...
	)

Or you can search across all indices

	client.Search<MyObject>(s => s
	    .AllIndices()
	    ...
	)

## Covariance
You can make C# covariance work for you by typing the search to a common baseclass (can be object)

i.e:

	.Search<object>(s => s
    	.Types(typeof(Product), typeof(Category), typeof(Manufacturer))
    	.Query(q => ...)
	);

This will search on `/yourdefaultindex/products,categories,manufacturers/_search` and setup a default `ConcreteTypeSelector` that understands what type each returned document is.

Using 

	.ConcreteTypeSelector(Func<dynamic, Hit<dynamic>, Type>)

you can manually specify the type of each hit based on some JSON value (on dynamic) or on the hit metadata.

##Routing
You can specify the routing for the search request using 

	.Routing("routevalue")

