---
template: layout.jade
title: Range Query
menusection: query
menuitem: range
---


# Range Query
Matches documents with fields that have terms within a certain range. The type of the Lucene query depends on the field type, for string fields, the `TermRangeQuery`, while for number/date fields, the query is a `NumericRangeQuery`. 


	.Query(ff => ff
		.Range(n => n
			.OnField(f=>f.LOC)
			.From("10")
			.To("20")
			.FromExclusive()
		)
	);

alternatively `.From(10)` and `.From(10.0)` work as expected.