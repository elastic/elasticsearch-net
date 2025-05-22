---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/index.html
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/introduction.html
---

# .NET [introduction]

**Rapidly develop applications with the .NET client for {{es}}.**

Designed for .NET application developers, the .NET language client library provides a strongly typed API and query DSL for interacting with {{es}}. The .NET client includes higher-level abstractions, such as helpers for coordinating bulk indexing and update operations. It also comes with built-in, configurable cluster failover retry mechanisms.

The {{es}} .NET client is available as a [NuGet](https://www.nuget.org/packages/Elastic.Clients.Elasticsearch) package for use with .NET Core, .NET 5+, and .NET Framework (4.6.1 and later) applications.

*NOTE: This documentation covers the v8 .NET client for {{es}}, for use with {{es}} 8.x versions. To develop applications targeting {{es}} v7, use the [v7 (NEST) client](https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17).*


## Features [features]

* One-to-one mapping with the REST API.
* Strongly typed requests and responses for {{es}} APIs.
* Fluent API for building requests.
* Query DSL to assist with constructing search queries.
* Helpers for common tasks such as bulk indexing of documents.
* Pluggable serialization of requests and responses based on `System.Text.Json`.
* Diagnostics, auditing, and .NET activity integration.

The .NET {{es}} client is built on the Elastic Transport library, which provides:

* Connection management and load balancing across all available nodes.
* Request retries and dead connections handling.


## {{es}} version compatibility [_es_version_compatibility]

Language clients are forward compatible: clients support communicating with current and later minor versions of {{es}}. {{es}} language clients are backward compatible with default distributions only and without guarantees.


## Questions, bugs, comments, feature requests [_questions_bugs_comments_feature_requests]

To submit a bug report or feature request, use [GitHub issues](https://github.com/elastic/elasticsearch-net/issues).

For more general questions and comments, try the community forum on [discuss.elastic.co](https://discuss.elastic.co/c/elasticsearch). Mention `.NET` in the title to indicate the discussion topic.

