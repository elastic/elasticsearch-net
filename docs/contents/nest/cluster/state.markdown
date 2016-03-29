---
template: layout.jade
title: State
menusection: cluster
menuitem: state
---


# Cluster state

## Get state

To get the basic cluster state, call:

	var state = _client.ClusterState();

This returns a IClusterStateResponse that contains information about the master node, all nodes in the cluster and such.

## Get specific part of state

If you only want a specific part, i.e. only information about nodes, you can do the following:

	var state = _client.ClusterState(c => c
							.Metrics(ClusterStateMetric.Nodes));
