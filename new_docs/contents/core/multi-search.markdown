---
template: layout.jade
title: MultiSearch
menusection: core
menuitem: multi-search
---


# MultiSearch

The multi search API allows to execute several search requests within the same API. The endpoint for it is _msearch (available from 0.19 onwards).

# Simple example

	var result = this._client.MultiSearch(b => b
		.Search<ElasticSearchProject>(s => s.MatchAll())
		.Search<ElasticSearchProject>(s => s.MatchAll())
		.Search<Person>(s => s.MatchAll())
	);

	//elasticProjectResults = IEnumerable<QueryResponse<ElasticSearchProject>> with a Count() of 2
	var elasticProjectResults = result.GetResponses<ElasticSearchProject>();

	//personResults = IEnumerable<QueryResponse<Person>> with a Count() of 1
	var personResults = result.GetResponses<Person>();

# Named example

	var result = this._client.MultiSearch(b => b
		.Search<ElasticSearchProject>("esproj", s => s.MatchAll())
		.Search<ElasticSearchProject>(s => s.MatchAll())
		.Search<Person>("people", s => s.MatchAll())
	);

	//elasticProjectResult is a QueryResponse<ElasticSearchProject>>
	var elasticProjectResult = result.GetResponse<ElasticSearchProject>("esproj");

	//personResult is a QueryResponse<Person>>
	var personResults = result.GetResponses<Person>("people");

	//will be null 
	var invalidResult = result.GetResponses<ElasticSearchProject>("people");
