# Elasticsearch .NET Client

Repository for **Elastic.Clients.Elasticsearch** the official .NET client for
[Elasticsearch](https://github.com/elastic/elasticsearch).
*Older branches include both previous clients, **NEST** and **Elasticsearch.Net**.*

**[Download the latest version of Elasticsearch](https://www.elastic.co/downloads/elasticsearch)**
or
**[sign-up](https://cloud.elastic.co/registration?elektra=en-ess-sign-up-page)**
**for a free trial of Elastic Cloud**.

The .NET client for Elasticsearch provides strongly typed requests and responses
 for Elasticsearch APIs. It delegates protocol handling to the 
 [Elastic.Transport](https://github.com/elastic/elastic-transport-net) library,
 which takes care of all transport-level concerns (HTTP connection establishment
 and pooling, retries, etc.).

## Compatibility

Language clients are forward compatible; meaning that the clients support
communicating with greater or equal minor versions of Elasticsearch without
breaking. It does not mean that the clients automatically support new features
of newer Elasticsearch versions; it is only possible after a release of a new
client version. For example, a 8.12 client version won't automatically support
the new features of the 8.13 version of Elasticsearch, the 8.13 client version
is required for that. Elasticsearch language clients are only backwards
compatible with default distributions and without guarantees made.

| Elasticsearch Version | Elasticsearch-NET Branch  | Supported |
| --------------------- | ------------------------- | --------- |
| main                  | main                      |           |
| 8.x                   | 8.x                       | 8.x       |
| 7.x                   | 7.x                       | 7.17      |

## Installation

Refer to the [Installation section](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/getting-started-net.html#_installation)
of the getting started documentation.

## Connecting

Refer to the [Connecting section](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/getting-started-net.html#_connecting)
of the getting started documentation.

## Usage

- [Creating an index](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/getting-started-net.html#_creating_an_index)
- [Indexing a document](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/getting-started-net.html#_indexing_documents)
- [Getting documents](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/getting-started-net.html#_getting_documents)
- [Searching documents](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/getting-started-net.html#_searching_documents)
- [Updating documents](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/getting-started-net.html#_updating_documents)
- [Deleting documents](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/getting-started-net.html#_deleting_documents)
- [Deleting an index](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/getting-started-net.html#_deleting_an_index)

## Documentation

Please refer to
[the full documentation on elastic.co](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/index.html)
for comprehensive information on installation, configuration and usage.

## Versions

### Elasticsearch 8.x Clusters

We have released the next generation of the .NET client for Elasticsearch, which
aligns with v8 of Elasticsearch. We have renamed this library
`Elastic.Clients.Elasticsearch`, and the packages are published on
[NuGet](https://www.nuget.org/packages/Elastic.Clients.Elasticsearch/). The
8.0.x versions do not offer complete feature parity with the existing `NEST`
client. We therefore recommend you thoroughly review our
[release notes and migration guidance](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/release-notes-8.0.0.html)
before attempting to migrate existing applications to the
`Elastic.Clients.Elasticsearch` library.

Until the new client supports all endpoints and features your application
requires, you may continue to use the latest `7.17.x` client to communicate with
Elasticsearch v8 servers. Please review
[our documentation](https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/connecting-to-elasticsearch-v8.html),
which describes how to enable compatibility mode and secure communications with
a v8 cluster.

### Elasticsearch 7.x Clusters

We recommend using the latest `7.17.x`
[NEST client](https://www.nuget.org/packages/Nest) to communicate with
Elasticsearch v7 servers.

## Contributing

See [CONTRIBUTING.md](./CONTRIBUTING.md)

## Copyright and License

This software is Copyright (c) 2014-2022 by Elasticsearch BV.

This is free software, licensed under
[The Apache License Version 2.0](https://github.com/elastic/elasticsearch-net/blob/main/LICENSE.txt).