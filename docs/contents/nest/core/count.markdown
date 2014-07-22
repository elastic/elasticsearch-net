---
template: layout.jade
title: Connecting
menusection: core
menuitem: count
---


# Count

The count API allows to easily execute a query and get the number of matches for that query. It can be executed across one or more indices and across one or more types. The query can either be provided using a simple query string as a parameter, or using the Query DSL defined within the request body. Here is an example:

	var countResults = this._client.CountAll(q=>q.MatchAll());
	Assert.True(countResults.Count > 0);

The above will do a count query across all indices. (The result type here is not limited)

If you want to limit the scope to just the default index for the type:

	var countResults = this._client.Count<ElasticSearchProject>(q=>q.MatchAll());

This does a match all count query on the default index for `ElasticSearchProject`, if you need to specify custom indices use:


	var countResults = this._client.Count<ElasticSearchProject>(new [] { "index1", "index2"}, q=>q.MatchAll());
