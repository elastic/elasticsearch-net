---
template: layout.jade
title: Connecting
menusection: core
menuitem: percolate
---


# Percolation
The percolator allows to register queries against an index, and then send percolate requests which include a doc, and getting back the queries that match on that doc out of the set of registered queries. 

Percolate is a complex but awesome Elasticsearch feature, so be sure to read the [official documentation](http://www.elasticsearch.org/guide/reference/api/percolate/)

# Register a percolator

	var r = c.RegisterPercolator<ElasticSearchProject>(p => p
		.Name(name)
		.Query(q => q
			.Term(f => f.Name, "elasticsearch.pm")
		)
	);

# Percolate a document

	var r = c.Percolate<ElasticSearchProject>(p=>p.Object(new ElasticSearchProject()
	{
		Name = "elasticsearch.pm",
		Country = "netherlands",
		LOC = 100000,
	}));
	Assert.True(r.IsValid);
	Assert.True(r.OK);
	Assert.NotNull(r.Matches);
	Assert.True(r.Matches.Contains(name));

# Unregister a percolator

	var re = c.UnregisterPercolator<ElasticSearchProject>(name);

# Percolate from a bulk index action

	var descriptor = new BulkDescriptor();
	// match against any doc
	descriptor.Index<ElasticSearchProject>(i => i
		.Object(new ElasticSearchProject { Id = 2, Country = "netherlands" })
		.Percolate("*") // match on any percolated docs
	);

	// no percolate requested this time
	descriptor.Index<ElasticSearchProject>(i => i
		.Object(new ElasticSearchProject { Id = 3, Country = "netherlands" })
	);
	this._client.Bulk(descriptor);

