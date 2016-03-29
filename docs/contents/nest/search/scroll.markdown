---
template: layout.jade
title: Scroll
menusection: search
menuitem: scroll
---

# Scroll

The scroll API allows you to efficiently page through a large dataset as it keeps the query alive in the cluster. 

	var scanResults = this._client.Search<ElasticSearchProject>(s => s
		.From(0)
		.Size(1)
		.MatchAll()
		.Fields(f => f.Name)
		.SearchType(Nest.SearchType.Scan)
		.Scroll("2s")
	);
	Assert.True(scanResults.IsValid);
	Assert.False(scanResults.Documents.Any());
	Assert.IsNotNullOrEmpty(scanResults.ScrollId);

	var scrolls = 0;
	var results = this._client.Scroll<ElasticSearchProject>("4s", scanResults.ScrollId);
	while (results.Documents.Any())
	{
		Assert.True(results.IsValid);
		Assert.True(results.Documents.Any());
		Assert.IsNotNullOrEmpty(results.ScrollId);
		results = this._client.Scroll<ElasticSearchProject>("4s", results.ScrollId);
		scrolls++;
	}
	Assert.AreEqual(18, scrolls);