---
template: layout.jade
title: Nodes Info
menusection: cluster
menuitem: nodes-info
---

# Nodes info

    var r = this._client.NodeInfo(NodesInfo.All);
    var node = r.Nodes.Values.First();
    

You can then traverse all the stats i.e:

    node.OS.CPU.Idle
