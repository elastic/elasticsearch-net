---
layout: default
title: Connecting
menu_section: indices
menu_item: flush
---


# Flush

## Flush all

	var r = this.ConnectedClient.Flush();


## Flush index

	var r = this.ConnectedClient.Flush("index");


## Flush several indices

	var r = this.ConnectedClient.Flush(new[] { "index", "index2" });


## Flush by type and wait for the refresh

	var r = this.ConnectedClient.Flush<ElasticSearchProject>(true);


