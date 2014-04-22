---
template: layout.jade
title: Suggest
menusection: search
menuitem: suggest
---

# Suggest
The suggest feature suggests similar looking terms based on a provided text by using a suggester.

## Completion Suggester

	client.Suggest<ElasticSearchProject>(s => s
		.Completion("my-suggestion", c => c
			.Text("test")));

You can also use fuzzy parameters, either by specifying edit distance and such:

	client.Suggest<ElasticSearchProject>(s => s
		.Completion("my-suggestion", c => c
			.Text("test")
			.Fuzzy(f => f
				.EditDistance(2)
				.Transpositions(false)
				.MinLength(5)
				.PrefixLength(4))));

Or by using the Fuzziness variant:

	client.Suggest<ElasticSearchProject>(s => s
		.Completion("my-suggestion", c => c
			.Text("test")
			.Fuzziness();