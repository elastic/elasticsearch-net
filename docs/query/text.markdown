---
layout: default
title: Text Query
menu_section: query
menu_item: text
---


# Text Query

A family of text queries that accept text, analyzes it, and constructs a query out of it. 

## Boolean

	.Text(t => t
		.OnField(f => f.Name)
		.QueryString("this is a test")
		.Fuzziness(1.0)
		.Analyzer("my_analyzer")
		.PrefixLength(2)
	)

## Phrase

	.TextPhrase(t => t
		.OnField(f => f.Name)
		.QueryString("this is a test")
		.Fuzziness(1.0)
		.Analyzer("my_analyzer")
		.PrefixLength(2)
	)

## Phrase Prefix

	.TextPhrasePrefix(t => t
		.OnField(f => f.Name)
		.QueryString("this is a test")
		.Fuzziness(1.0)
		.Analyzer("my_analyzer")
		.PrefixLength(2)
		.Operator(Operator.and)
	)