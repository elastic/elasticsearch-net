---
template: layout.jade
title: Connecting
menusection: search
menuitem: basics
---


# From/Size

	s.From(120).Size(20);

this is also valid

	.Skip(120).Take(20)

# Indices / Types
You can explicitly tell NEST to use multiple indices:

	client.Search<MyObject>(s=>s
	    .Indices(new [] {"Index_A", "Index_B"})
	    ...
	)

If you want to search across all indices

	client.Search<MyObject>(s=>s
	    .AllIndices()
	    ...
	)

Or if you want to search one index (that is not the default index)

	client.Search<MyObject>(s=>s.
	    .Index("Index_A")
	    ...
	)

Remember that since Elasticsearch 19.8 you can also specify wildcards on index names

	client.Search<MyObject>(s=>s
	    .Index("Index_*")
	    ...
	)
	.Types(new [] { typeof(ElasticSearchProject)})

## Covariance
You can make C# covariance work for you by typing the search to a common baseclass (can be object)

i.e:

	.Search<object>(s=>s
    	.Types(typeof(Product),typeof(Category),typeof(Manufacturer))
    	.Query(...)
	);

This will search on `/yourdefaultindex/products,categories,manufacturers/_search` and setup a default `ConcreteTypeSelector` that understands what type each returned document is.

Using 

	.ConcreteTypeSelector(Func<dynamic, Hit<dynamic>, Type>)

you can manually specify the type of each hit based on some JSON value (on dynamic) or on the hit metadata.

#Routing
You can specify the routing for the search request using 

	.Routing("routevalue")

