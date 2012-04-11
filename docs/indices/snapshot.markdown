---
layout: default
title: Connecting
menu_section: indices
menu_item: snapshot
---

# Snapshot

The gateway snapshot API allows to explicitly perform a snapshot through the gateway of one or more indices (backup them). By default, each index gateway periodically snapshot changes, though it can be disabled and be controlled completely through this API.

Note, this API only applies when using shared storage gateway implementation, and does not apply when using the (default) local gateway.

## All
```C#
var r = this.ConnectedClient.Snapshot();
```

## Snapshot Index 
```C#
var r = this.ConnectedClient.Snapshot("index");
```

## Typed
```C#
var r = this.ConnectedClient.Snapshot<ElasticSearchProject>();
```
