---
template: layout.jade
title: Highlighting
menusection: search
menuitem: highlighting
---


# Highlighting

Using highlighting you can return the relavent parts of a field and using highlighted markers indicating why a document matched.

	var result = this._client.Search<ElasticSearchProject>(s => s
		.From(0)
		.Size(10)
		.Query(q => q
			.QueryString(qs => qs
				.OnField(e => e.Content)
				.Query("null or null*")
			)
		)
		.Highlight(h => h
			.PreTags("<b>")
			.PostTags("</b>")
			.OnFields(f => f
				.OnField(e => e.Content)
				.PreTags("<em>")
				.PostTags("</em>")
			)
		)
	);

Please take note that this wont alter the contents of the results `.Documents` but the results will have a separate bucket 
that contains each highlight result(s) for each hit on `result.Highlights`. 