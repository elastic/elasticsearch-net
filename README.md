<img alt="Elastic logo" align="right" width="auto" height="auto" src="https://www.elastic.co/static-res/images/elastic-logo-200.png">

Repository for **Elastic.Clients.Elasticsearch** the official .NET client for [Elasticsearch](https://github.com/elastic/elasticsearch). *Older branches include both previous clients, **NEST** and **Elasticsearch.Net**.*

The .NET client for Elasticsearch provides strongly typed requests and responses for Elasticsearch APIs. It delegates protocol handling to the [Elastic.Transport](https://github.com/elastic/elastic-transport-net) library, which takes care of all transport-level concerns (HTTP connection establishment and pooling, retries, etc.).

## Compatibility

Language clients are forward compatible; meaning that clients support communicating with greater or equal minor versions of Elasticsearch. Elasticsearch language clients are only backwards compatible with default distributions and without guarantees made.

## Versions

### Elasticsearch 8.x Clusters

We are actively working on the next generation of the .NET client for Elasticsearch, which aligns with v8 of Elasticsearch. We have renamed this library `Elastic.Clients.Elasticsearch`, and the packages are published on [NuGet](https://www.nuget.org/packages/Elastic.Clients.Elasticsearch/). The new client is in pre-release with beta versions available to install. We do not recommend using the pre-release versions in production.

Until the GA release of the new client, you may continue to use the latest `7.17.x` client to communicate with Elasticsearch v8 servers. Please review [our documentation](https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/connecting-to-elasticsearch-v8.html), which describes how to enable compatibility mode and secure communications with a v8 cluster.

### Elasticsearch 7.x Clusters

We recommend using the latest `7.17.x` client to communicate with Elasticsearch v7 servers.

## Documentation

Please refer to [the full documentation on elastic.co](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/index.html) for comprehensive information.

## Contributing

See [CONTRIBUTING.md](./CONTRIBUTING.md)

## Copyright and License

This software is Copyright (c) 2014-2022 by Elasticsearch BV.

This is free software, licensed under [The Apache License Version 2.0](https://github.com/elastic/elasticsearch-net/blob/main/LICENSE.txt).