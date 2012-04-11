---
layout: default
title: Connecting
menu_section: indices
menu_item: create-index
---


# Create index 
The create index API allows to instantiate an index. ElasticSearch provides support for multiple indices, including executing operations across several indices. Each index created can have specific settings associated with it.

## Note 
When adding settings strip the `index.` prefix when passing settings found for example here:http://www.elasticsearch.org/guide/reference/index-modules/

## Simple example

	var client = this.ConnectedClient;
	var settings = new IndexSettings();
	settings.NumberOfReplicas = 1;
	settings.NumberOfShards = 5;
	settings.Add("merge.policy.merge_factor","10");
	settings.Add("search.slowlog.threshold.fetch.warn", "1s");
	client.CreateIndex("myindexname", settings);


## Create index with type and name


	var typeMapping = new TypeMapping("mytype");
	var type = new TypeMappingProperty
	{
		Type = "string",
		Index = "not_analyzed",
		Boost = 2.0
		// Many more options available
	};
	typeMapping.Properties = new Dictionary<string, TypeMappingProperty>();
	typeMapping.Properties.Add("name", type);

	var settings = new IndexSettings();
	settings.Mappings.Add(typeMapping);
	settings.NumberOfReplicas = 1;
	settings.NumberOfShards = 5;
	settings.Analysis.Analyzer.Add("snowball", new SnowballAnalyzerSettings { Language = "English" });

	var indexName = Guid.NewGuid().ToString();
	var response = client.CreateIndex(indexName, settings);


## Create Index with type and field settings

Creates a multimap field "name" 

	var client = this.ConnectedClient;
	var typeMapping = new TypeMapping(Guid.NewGuid().ToString("n"));
	var property = new TypeMappingProperty
	{
		Type = "multi_field"
	};

	var primaryField = new TypeMappingProperty
	{
		Type = "string", 
		Index = "not_analyzed"
	};
	var analyzedField = new TypeMappingProperty
	{
		Type = "string", 
		Index = "analyzed"
	};

	property.Fields = new Dictionary<string, TypeMappingProperty>();
	property.Fields.Add("name", primaryField);
	property.Fields.Add("name_analyzed", analyzedField);

	typeMapping.Properties.Add("name", property);
	var settings = new IndexSettings();
	settings.Mappings.Add(typeMapping);
	settings.NumberOfReplicas = 1;
	settings.NumberOfShards = 5;
	settings.Analysis.Analyzer.Add("snowball", new SnowballAnalyzerSettings { Language = "English" });

	var indexName = Guid.NewGuid().ToString();
	var response = client.CreateIndex(indexName, settings);


