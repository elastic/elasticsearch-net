---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: advanced
---

# Advanced usage

## Creating multiple aggregations from an IEnumerable

Premise:
You have an IEnumerable<TestObject> MyFilters, and you want to build a Filter aggregation with a Term filter for each of the TestObjects you have.

	class TestObject
	{
		public string Name { get; set; }
		public string Term { get; set; }
	}

Then you could do something like this:

	IEnumerable<TestObject> MyFilters;

	var descriptor = new SearchDescriptor<MySearchType>().Query(q => q.MatchAll());

	descriptor.Aggregations(aggr => 
	{
		foreach (var term in MyFilters)
		{
			aggr.Filter(term.Name, filter => filter
				.Filter(f => f
					.Term("TheTermField", term.Term)))
		}

		return aggr;
	});

	var result = client.Search<MySearchType>(descriptor);

And then you can access the aggregation results as normal.
