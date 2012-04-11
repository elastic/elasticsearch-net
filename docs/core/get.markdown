---
layout: default
title: Connecting
menu_section: core
menu_item: get
---


# Get a document

gets a single document from Elasticsearch

## By Id

	var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(hit.Id);

index and type are infered but overloads exists for full control

	var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>("myalternateindex", "elasticprojs", hit.Id);




 

