---
template: layout.jade
title: Analyze
menusection: indices
menuitem: analyze
---

#Analyze 

Performs the analysis process on the specified text and returns the token breakdown for the text.

## Examples

### Fluent Syntax

	var result = client.Analyze(a => a
		.Index("myindex")
		.Analyzer("whitespace")
		.Text("text to analyze")
	);

### Object Initializer Syntax

	var request = new AnalyzeRequest("text to analyze")
	{
		Index = "myindex",
		Analyzer = "whitespace"
	};

	var result = client.Analyze(request);

## Handling the Analyze response

`result` above is an `IAnalyzeResponse` which contains a collection of tokens found in the `Tokens` property which is an `IEnumerable<AnalyzeToken>`.