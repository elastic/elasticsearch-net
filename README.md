# Elasticsearch .NET Client

Repository for **Elastic.Clients.Elasticsearch** the official .NET client for
[Elasticsearch](https://github.com/elastic/elasticsearch).

**[Download the latest version of Elasticsearch](https://www.elastic.co/downloads/elasticsearch)**
or
**[sign-up](https://cloud.elastic.co/registration?elektra=en-ess-sign-up-page)**
**for a free trial of Elastic Cloud**.

The .NET client for Elasticsearch provides strongly typed requests and responses
 for Elasticsearch APIs. It delegates protocol handling to the
[Elastic.Transport](https://github.com/elastic/elastic-transport-net) library,
 which takes care of all transport-level concerns (HTTP connection establishment
 and pooling, retries, etc.).

## Versioning

The *major* and *minor* version parts of the Elasticsearch .NET client are dictated by the version of the Elasticsearch server.

> [!WARNING]
> This means that the Elasticsearch .NET client **does not** strictly follows semantic versioning!
>
> Although we try to avoid this as much as possible, it can happen that a *minor* or even *patch* version contains breaking changes (see also: [breaking changes policy](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/breaking-changes-policy.html)). Please always check the [release notes](https://github.com/elastic/elasticsearch-net/releases) before updating the client package.

## Compatibility

Language clients are **forward compatible**:

Given a constant major version of the client, each related minor version is compatible with its equivalent- and all later Elasticsearch minor versions of the **same or next higher** major version.

For example:

| Client Version | Compatible with Elasticsearch `8.x` | Compatible with Elasticsearch `9.x` | Compatible with Elasticsearch `10.x` |
| ---: | :-- | :-- | :-- |
| 9.x | ❌ no | ✅ yes | ✅ yes |
| 8.x | ✅ yes | ✅ yes | ❌ no |

Language clients are also **backward compatible** across minor versions within the **same** major version (without strong guarantees), but **never** backward compatible with earlier Elasticsearch major versions.

> [!NOTE]
> Compatibility does not imply feature parity. For example, an `8.12` client is compatible with `8.13`, but does not support any of the new features introduced in Elasticsearch `8.13`.

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

The API reference documentation is available [here](https://elastic.github.io/elasticsearch-net).

## Try Elasticsearch and Kibana locally

If you want to try Elasticsearch and Kibana locally, you can run the following command:

```bash
curl -fsSL https://elastic.co/start-local | sh
```

This will run Elasticsearch at [http://localhost:9200](http://localhost:9200) and Kibana at [http://localhost:5601](http://localhost:5601).

More information is available [here](https://www.elastic.co/guide/en/elasticsearch/reference/current/run-elasticsearch-locally.html).

## Contributing

See [CONTRIBUTING.md](./CONTRIBUTING.md)

## Copyright and License

This software is Copyright (c) 2014-2025 by Elasticsearch BV.

This is free software, licensed under
[The Apache License Version 2.0](https://github.com/elastic/elasticsearch-net/blob/main/LICENSE.txt).
