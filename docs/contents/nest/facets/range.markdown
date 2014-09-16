---
template: layout.jade
title: Range Facet
menusection: facets
menuitem: range
---


# Range Facet

Range faceting allows you to specify a set of ranges and get both the number of docs (count) that fall within each range, and aggregated data either based on the field, or using another field. Here is a simple example:


	this.ConnectedClient.Search<ElasticSearchProject>(s=>s
		.From(0)
		.Size(10)
		.MatchAll()
		.FacetRange<int>(t => t
			.OnField(f => f.LOC)
			.Ranges(
				r=>r.To(50),
				r=>r.From(50).To(100),
				r=>r.From(100).To(150),
				r=>r.From(150).To(200),
				r=>r.From(200).To(250),
				r=>r.From(250)
			)
		);
	);

Ranges can also be passed as `double`:

	//SNIP
	.FacetRange<double>(t => t
	.OnField(f => f.LOC)
		.Ranges(
			r => r.To(50.0),
			r => r.From(50.0).To(100.0),
			r => r.From(100.0)
		)
	)
	//SNIP

or `DateTime`:

	//SNIP
	.FacetRange<DateTime>(t => t
		.OnField(f => f.StartedOn)
		.Ranges(
			r => r.To(new DateTime(1990,1,1).Date)
		)
	);
	//SNIP

You can also pass scripts to create complex range facets:

	//SNIP
	.FacetRange<DateTime>("needs_a_name", t => t
		.KeyScript("doc['date'].date.minuteOfHour")
		.ValueScript("doc['num1'].value")
		.Ranges(
			r => r.To(new DateTime(1990, 1, 1).Date)
		)
	)
	//SNIP

or alternative key/value fields

	//SNIP
	.FacetRange<DateTime>("needs_a_name", t => t
		.KeyField("field_name")
		.ValueField("another_field_name")
		.Ranges(
			r => r.To(new DateTime(1990, 1, 1).Date)
		)
	);
	//SNIP

See [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-range-facet.html) for more information.

