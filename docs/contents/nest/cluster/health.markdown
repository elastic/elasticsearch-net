---
template: layout.jade
title: Health
menusection: cluster
menuitem: health
---


# Health
Get cluster health simple

    var r = this._client.Health(HealthLevel.Cluster);

Cluster health for one (or more) indexes

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

