---
template: layout.jade
title: Connecting
menusection: cluster
menuitem: health
---


# Health
Get cluster health simple

    var r = this._client.Health(HealthLevel.Cluster);

Cluster health just for one (or more) index

    var r = this._client.Health(new[] { Test.Default.DefaultIndex }, HealthLevel.Cluster);

Advanced options are mapped as well

    var r = this._client.Health(new HealthParams
    {
        CheckLevel = HealthLevel.Shards,
        Timeout = "30s",
        WaitForMinNodes = 1,
        WaitForRelocatingShards = 0,
        WaitForStatus = HealthStatus.Green
    });

