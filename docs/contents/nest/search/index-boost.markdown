---
template: layout.jade
title: Index Boost
menusection: search
menuitem: index-boost
---


# Index boost

An index boost allows you to boost hits if they originate from a certain index

	var s = new SearchDescriptor<ElasticSearchProject>()
		.Skip(0)
		.Take(10)
		.Explain()
		.Version()
		.MinScore(0.4)
		.IndicesBoost(b => b.Add("index1", 1.4).Add("index2", 1.3));
	

