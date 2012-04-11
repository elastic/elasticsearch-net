---
layout: default
title: Connecting
menu_section: indices
menu_item: analyze
---

#Analyze 

Performs the analysis process on a text and return the tokens breakdown of the text.

## Analyze using default index's default analyzer

	var text = "this is a string with some spaces and stuff";
	var r = this.ConnectedClient.Analyze(text);


## Analyze using a fields analyzer

    var text = "this is a string with some spaces and stuff";
    var r = this.ConnectedClient.Analyze<ElasticSearchProject>(p => p.Content, text);


## Analyze using parameters

	var analyzer = new AnalyzeParams { Analyzer = "whitespace", Index = Test.Default.DefaultIndex + "_clone" };
	var r = this.ConnectedClient.Analyze(analyzer, text);




