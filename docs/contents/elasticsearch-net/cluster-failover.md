---
template: layout.jade
title: Cluster Failover
menusection: 
menuitem: esnet-cluster-failover
---

# Connection pooling & Cluster failover

One of the major benefits of Elasticsearch is that it can handle dying and respawning  nodes. 
As long as enough nodes agree that the cluster is healthy, the cluster will continue to operate.
`Elasticsearch.Net` comes with builtin support for handle falling over to a different node when the requested node failed.

## Configuring 

Configuring how the registered `IConnectionPool` should behave happens on the `IConnectionConfigurationValues` passed to client 
[see the section on connecting ](/elasticsearch-net/connecting.html)

    var settings = new ConnectionConfiguration(connectionPool)
        .SniffOnConnectionFault(false)
        .SniffOnStartup(false)
        .SniffLifeSpan(TimeSpan.FromMinutes(1));

### SniffOnConnectionFault
Should the connection pool resniff the cluster state everytime an operation on a node throws an exception or a faulty http status code.
Defaults to true.

### SniffOnStartup
Should the connection pool sniff the cluster state the first time its instantiated. Defaults to true.

### SniffLifeSpan
When set will cause the connectionpool to resniff whenever it notices the last sniff information happened too long ago. Defaults to null.

### SetDeadTimeout
Sets the timeout before a node is retried. The default `DateTimeProvider` will increment this timeout exponentially based on the number of attempts.

### SetMaxDeadTimeout
Sets the maximum time a node may be marked dead.

### DisablePing
By default before a previously dead node is retried a short ping will be sent to the node to make sure the node will respond. 
The reason for a separate call is that a ping will call an elasticsearch endpoint that won't stress the JVM. If a node is having issues retrying a possible heavy search operation on it might cause the request to fail later rather then asap. This setting allows you to disable these pings before retries.

### MaximumRetries
By default an `IConnectionPool` itself will decide how many times to retry (usually all the registered nodes) if you wish to 
limit this you can explicitly tell the connection pool to never retry more than `retries`.

## SniffingConnectionPool

This `IConnectionPool` implementation will `sniff` the cluster state on the passed seed nodes to find all the alive nodes in the cluster. It will round robin requests over all the alive nodes it knows about. 

    var pool = new SniffingConnectionPool(seedUris);

## StaticConnectionPool

This `IConnectionPool` implementation will round robin over the provided nodes. When nodes are dead it will mark it dead with an incremental timeout before 
retrying requests on that node. When all the known nodes are marked dead an operation will be tried once on a random node from the list. 

The difference between this implementation and `SniffingConnectionPool` is that this implementation effectively treats the `Sniff()` call as a 
[NOOP](http://en.wikipedia.org/wiki/Noop)

    var pool = new StaticConnectionPool(seedUris);

