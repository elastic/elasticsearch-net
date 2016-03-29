---
template: layout.jade
title: Snapshot
menusection: indices
menuitem: snapshot
---

# Snapshot

The gateway snapshot API allows to explicitly perform a snapshot through the gateway of one or more indices (backup them). By default, each index gateway periodically snapshot changes, though it can be disabled and be controlled completely through this API.

Note, this API only applies when using shared storage gateway implementation, and does not apply when using the (default) local gateway.

## All

	var r = this.ConnectedClient.Snapshot();

## Snapshot Index 

	var r = this.ConnectedClient.Snapshot("index");


## Typed

	var r = this.ConnectedClient.Snapshot<ElasticSearchProject>();

