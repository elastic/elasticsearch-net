---
template: layout.jade
title: Templates
menusection: indices
menuitem: templates
---


# Index Templates API

Allows you to define templates on the cluster that are applied everytime an index is created.

	var putResponse = this._client.PutTemplate(t=>t	
		.Name("put-template-with-settings")
		.Template("donotinfluencothertests-*")
		.Settings(s=>s
			.Add("index.number_of_shards", 3)
			.Add("index.number_of_replicas", 2)
		)
		.AddMapping<dynamic>(s=>s
			.TypeName("mytype")
			.DisableAllField()
		)
	);
