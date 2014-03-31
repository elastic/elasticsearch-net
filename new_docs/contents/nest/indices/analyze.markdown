---
template: layout.jade
title: Connecting
menusection: indices
menuitem: analyze
---

#Analyze 

Performs the analysis process on the specified text and returns the token breakdown for the text.

## Analyze using default index's default analyzer

	var text = "this is a string with some spaces and stuff";
	var r = this.ConnectedClient.Analyze(text);


## Analyze using a fields analyzer

    var text = "this is a string with some spaces and stuff";
    var r = this.ConnectedClient.Analyze<ElasticSearchProject>(p => p.Content, text);


## Analyze using parameters

	var analyzer = new AnalyzeParams { Analyzer = "whitespace", Index = Test.Default.DefaultIndex + "_clone" };
	var r = this.ConnectedClient.Analyze(analyzer, text);




