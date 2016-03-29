---
template: layout.jade
title: Stats
menusection: indices
menuitem: stats
---


#Indices stats api
Indices level stats provide statistics on different operations happening on an index. The API provides statistics on the index level scope (though most stats can also be retrieved using node level scope).

## Global Stats

	var r = this.ConnectedClient.Stats();
	var deletedOnPrimaries = r.Stats.Primaries.Documents.Deleted;
	var deletedOnIndexPrimaries = r.Stats.Indices["nest_test_data"].Primaries.Documents.Count;


## Index Stats

	var r = this.ConnectedClient.Stats("nest_test_data");
	var deletedOnIndexPrimaries = r.Stats.Primaries.Documents.Deleted;


## Stats params

	var r = this.ConnectedClient.Stats(new StatsParams()
	{
		InfoOn = StatsInfo.All,
		Refresh = true,
		Types = new List<string>{ "elasticsearchprojects" }

	});
	var x = r.Stats.Primaries.Indexing.Types["elasticsearchprojects"].Current;

