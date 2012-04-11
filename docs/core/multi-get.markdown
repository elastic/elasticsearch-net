---
layout: default
title: Connecting
menu_section: core
menu_item: multi-get
---


# Multi Get

Get multiple documents in a single request.

##Examles

	var ids = new [] { hit1.Id, hit2.Id };
	var foundDocuments = this.ConnectedClient.Get<ElasticSearchProject>(ids);

index and type are infered but overloads exists for full control

	var foundDocuments = this.ConnectedClient.Get<ElasticSearchProject>("myalternateindex", "elasticprojs", ids);
