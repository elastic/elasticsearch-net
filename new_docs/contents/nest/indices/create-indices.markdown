---
template: layout.jade
title: Connecting
menusection: indices
menuitem: create-indices
---


# Create index 
The create index API allows to instantiate an index. Elasticsearch provides support for multiple indices, including executing operations across several indices. Each index created can have specific settings associated with it.

## Note 
When adding settings strip the `index.` prefix. This applies to settings found in the [Index Module](http://www.elasticsearch.org/guide/reference/index-modules/) documentation.

## Simple example

	var client = this.ConnectedClient;
	var settings = new IndexSettings();
	settings.NumberOfReplicas = 1;
	settings.NumberOfShards = 5;
	settings.Add("merge.policy.merge_factor","10");
	settings.Add("search.slowlog.threshold.fetch.warn", "1s");
	client.CreateIndex("myindexname", settings);


## Create index with settings and mappings in one go fluently

	client.CreateIndex("myindexname", c => c
		.NumberOfReplicas(0)
		.NumberOfShards(1)
		.Settings(s=>s
			.Add("merge.policy.merge_factor","10")
			.Add("search.slowlog.threshold.fetch.warn", "1s")
		)   
		.AddMapping<ElasticSearchProject>(m => m.MapFromAttributes())
		.AddMapping<Person>(m => m.MapFromAttributes())
	);


