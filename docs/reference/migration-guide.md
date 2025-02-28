---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/migration-guide.html
---

# Migration guide: From NEST v7 to .NET Client v8 [migration-guide]

The following migration guide explains the current state of the client, missing features, breaking changes and our rationale for some of the design choices we have introduced.


## Version 8 is a refresh [_version_8_is_a_refresh]

::::{important}
It is important to highlight that v8 of the Elasticsearch .NET Client represents a new start for the client design. It is important to review how this may affect your code and usage.

::::


Mature code becomes increasingly hard to maintain over time. Major releases allow us to simplify and better align our language clients with each other in terms of design. It is crucial to find the right balance between uniformity across programming languages and the idiomatic concerns of each language. For .NET, we typically compare and contrast with [Java](https://github.com/elastic/elasticsearch-java) and [Go](https://github.com/elastic/go-elasticsearch) to make sure that our approach is equivalent for each of these. We also take heavy inspiration from Microsoft framework design guidelines and the conventions of the wider .NET community.


### New Elastic.Clients.Elasticsearch NuGet package [_new_elastic_clients_elasticsearch_nuget_package]

We have shipped the new code-generated client as a [NuGet package](https://www.nuget.org/packages/Elastic.Clients.Elasticsearch/) with a new root namespace, `Elastic.Clients.Elasticsearch`. The v8 client is built upon the foundations of the v7 `NEST` client, but there are changes. By shipping as a new package, the expectation is that migration can be managed with a phased approach.

While this is a new package, we have aligned the major version (v8.x.x) with the supported {{es}} server version to clearly indicate the client/server compatibility. The v8 client is designed to work with version 8 of {{es}}.

The v7 `NEST` client continues to be supported but will not gain new features or support for new {{es}} endpoints. It should be considered deprecated in favour of the new client.


### Limited feature set [_limited_feature_set]

::::{warning}
The version 8 Elasticsearch .NET Client does not have feature parity with the previous v7 `NEST` high-level client.

::::


If a feature you depend on is missing (and not explicitly documented below as a feature that we do not plan to reintroduce), open [an issue](https://github.com/elastic/elasticsearch-net/issues/new/choose) or comment on a relevant existing issue to highlight your need to us. This will help us prioritise our roadmap.


## Code generation [_code_generation]

Given the size of the {{es}} API surface today, it is no longer practical to maintain thousands of types (requests, responses, queries, aggregations, etc.) by hand. To ensure consistent, accurate, and timely alignment between language clients and {{es}}, the 8.x clients, and many of the associated types are now automatically code-generated from a [shared specification](https://github.com/elastic/elasticsearch-specification). This is a common solution to maintaining alignment between client and server among SDKs and libraries, such as those for Azure, AWS and the Google Cloud Platform.

Code-generation from a specification has inevitably led to some differences between the existing v7 `NEST` types and those available in the new v7 Elasticsearch .NET Client. For version 8, we generate strictly from the specification, special casing a few areas to improve usability or to align with language idioms.

The base type hierarchy for concepts such as `Properties`, `Aggregations` and `Queries` is no longer present in generated code, as these arbitrary groupings do not align with concrete concepts of the public server API. These considerations do not preclude adding syntactic sugar and usability enhancements to types in future releases on a case-by-case basis.


## Elastic.Transport [_elastic_transport]

The .NET client includes a transport layer responsible for abstracting HTTP concepts and to provide functionality such as our request pipeline. This supports round-robin load-balancing of requests to nodes, pinging failed nodes and sniffing the cluster for node roles.

In v7, this layer shipped as `Elasticsearch.Net` and was considered our low-level client which could be used to send and receive raw JSON bytes between the client and server.

As part of the work for 8.0.0, we have moved the transport layer out into a [new dedicated package](https://www.nuget.org/packages/Elastic.Transport) and [repository](https://github.com/elastic/elastic-transport-net), named `Elastic.Transport`. This supports reuse across future clients and allows consumers with extremely high-performance requirements to build upon this foundation.


## System.Text.Json for serialization [_system_text_json_for_serialization]

The v7 `NEST` high-level client used an internalized and modified version of [Utf8Json](https://github.com/neuecc/Utf8Json) for request and response serialization. This was introduced for its performance improvements over [Json.NET](https://www.newtonsoft.com/json), the more common JSON framework at the time.

While Utf8Json provides good value, we have identified minor bugs and performance issues that have required maintenance over time. Some of these are hard to change without more significant effort. This library is no longer maintained, and any such changes cannot easily be contributed back to the original project.

With .NET Core 3.0, Microsoft shipped new [System.Text.Json APIs](https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-apis) that are included in-the-box with current versions of .NET. We have adopted `System.Text.Json` for all serialization. Consumers can still define and register their own `Serializer` implementation for their document types should they prefer to use a different serialization library.

By adopting `System.Text.Json`, we now depend on a well-maintained and supported library from Microsoft. `System.Text.Json` is designed from the ground up to support the latest performance optimizations in .NET and, as a result, provides both fast and low-allocation serialization.


## Mockability of ElasticsearchClient [_mockability_of_elasticsearchclient]

Testing code is an important part of software development. We recommend that consumers prefer introducing an abstraction for their use of the Elasticsearch .NET Client as the prefered way to decouple consuming code from client types and support unit testing.

To support user testing scenarios, we have unsealed the `ElasticsearchClient` type and made its methods virtual. This supports mocking the type directly for unit testing. This is an improvement over the original `IElasticClient` interface from `NEST` (v7) which only supported mocking of top-level client methods.

We have also introduced a `TestableResponseFactory` in `Elastic.Transport` to make it easier to create response instances with specific status codes and validity that can be used during unit testing.

These changes are in addition to our existing support for testing with an `InMemoryConnection`, virtualized clusters and with our [`Elastic.Elasticsearch.Managed`](https://github.com/elastic/elasticsearch-net-abstractions/blob/master/src/Elastic.Elasticsearch.Managed) library for integration testing against real {{es}} instances.


## Migrating to Elastic.Clients.Elasticsearch [_migrating_to_elastic_clients_elasticsearch]

::::{warning}
The version 8 client does not currently have full-feature parity with `NEST`. The client primary use case is for application developers communicating with {{es}}.

::::


The version 8 client focuses on core endpoints, more specifically for common CRUD scenarios. The intention is to reduce the feature gap in subsequent versions. Review this documentation carefully to learn about the missing features and reduced API surface details before migrating from the v7 `NEST` client!

The choice to code-generate a new evolution of the Elasticsearch .NET Client introduces some significant breaking changes.

The v8 client is shipped as a new [NuGet package](https://www.nuget.org/packages/Elastic.Clients.Elasticsearch/) which can be installed alongside v7 `NEST`. Some consumers may prefer a phased migration with both packages side-by-side for a short period of time to manage complex migrations. In addition, `NEST` 7.17.x can continue to be used in [compatibility mode](https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/connecting-to-elasticsearch-v8.html) with {{es}} 8.x servers until the v8 Elasticsearch .NET Client features align with application requirements.


## Breaking Changes [_breaking_changes]

::::{warning}
As a result of code-generating a majority of the client types, version 8 of the client includes multiple breaking changes.

::::


We have strived to keep the core foundation reasonably similar, but types emitted through code-generation are subject to change between `NEST` (v7) and the new `Elastic.Clients.Elasticsearch` (v8) package.


### Namespaces [_namespaces]

The package and top-level namespace for the v8 client have been renamed to `Elastic.Clients.Elasticsearch`. All types belong to this namespace. When necessary, to avoid potential conflicts, types are generated into suitable sub-namespaces based on the [{{es}} specification](https://github.com/elastic/elasticsearch-specification). Additional `using` directives may be required to access such types when using the Elasticsearch .NET Client.

Transport layer concepts have moved to the new `Elastic.Transport` NuGet package and related types are defined under its namespace. Some configuration and low-level transport functionality may require a `using` directive for the `Elastic.Transport` namespace.


### Type names [_type_names]

Type names may have changed from previous versions. These are not listed explicitly due to the potentially vast number of subtle differences. Type names will now more closely align to those used in the JSON and as documented in the {{es}} documentation.


### Class members [_class_members]

Types may include renamed properties based on the {{es}} specification, which differ from the original `NEST` property names. The types used for properties may also have changed due to code-generation. If you identify missing or incorrectly-typed properties, please open [an issue](https://github.com/elastic/elasticsearch-net/issues/new/choose) to alert us.


### Sealing classes [_sealing_classes]

Opinions on "sealing by default" within the .NET ecosystem tend to be quite polarized. Microsoft seal all internal types for potential performance gains and we see a benefit in starting with that approach for the Elasticsearch .NET Client, even for our public API surface.

While it prevents inheritance and, therefore, may inhibit a few consumer scenarios, sealing by default is intended to avoid the unexpected or invalid extension of types that could inadvertently be broken in the future.


### Removed features [_removed_features]

As part of the clean-slate redesign of the new client, certain features are removed from the v8.0 client. These are listed below:


#### Attribute mappings [_attribute_mappings]

In previous versions of the `NEST` client, attributes could be used to configure the mapping behaviour and inference for user types. It is recommended that mapping be completed via the fluent API when configuring client instances. `System.Text.Json` attributes may be used to rename and ignore properties during source serialization.


#### CAT APIs [_cat_apis]

The [CAT APIs](https://www.elastic.co/docs/api/doc/elasticsearch/group/endpoint-cat) of {{es}} are intended for human-readable usage and will no longer be supported via the v8 Elasticsearch .NET Client.


#### Interface removal [_interface_removal]

Several interfaces are removed to simplify the library and avoid interfaces where only a single implementation of that interface is expected to exist, such as `IElasticClient` in `NEST`. Abstract base classes are preferred over interfaces across the library, as this makes it easier to add enhancements without introducing breaking changes for derived types.


### Missing features [_missing_features]

The following are some of the main features which have not been re-implemented for the v8 client. These might be reviewed and prioritized for inclusion in future releases.

* Query DSL operators for combining queries.
* Scroll Helper.
* Fluent API for union types.
* `AutoMap` for field datatype inference.
* Visitor pattern support for types such as `Properties`.
* Support for `JoinField` which affects `ChildrenAggregation`.
* Conditionless queries.
* DiagnosticSources have been removed in `Elastic.Transport` to provide a clean-slate for an improved diagnostics story. The Elasticsearch .NET Client emits [OpenTelemetry](https://opentelemetry.io/) compatible `Activity` spans which can be consumed by APM agents such as the [Elastic APM Agent for .NET](apm-agent-dotnet://reference/index.md).
* Documentation is a work in progress, and we will expand on the documented scenarios in future releases.


## Reduced API surface [_reduced_api_surface]

In the current versions of the code-generated .NET client, supporting commonly used endpoints is critical. Some specific queries and aggregations need further work to generate code correctly, hence they are not included yet. Ensure that the features you are using are currently supported before migrating.

An up to date list of all supported and unsupported endpoints can be found on [GitHub](https://github.com/elastic/elasticsearch-net/issues/7890).


## Workarounds for missing features [_workarounds_for_missing_features]

If you encounter a missing feature with the v8 client, there are several ways to temporarily work around this issue until we officially reintroduce the feature.

`NEST` 7.17.x can continue to be used in [compatibility mode](https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/connecting-to-elasticsearch-v8.html) with {{es}} 8.x servers until the v8 Elasticsearch .NET Client features align with application requirements.

As a last resort, the low-level client `Elastic.Transport` can be used to create any desired request by hand:

```csharp
public class MyRequestParameters : RequestParameters
{
    public bool Pretty
    {
        get => Q<bool>("pretty");
        init => Q("pretty", value);
    }
}

// ...

var body = """
           {
             "name": "my-api-key",
             "expiration": "1d",
             "...": "..."
           }
           """;

MyRequestParameters requestParameters = new()
{
    Pretty = true
};

var pathAndQuery = requestParameters.CreatePathWithQueryStrings("/_security/api_key",
    client.ElasticsearchClientSettings);
var endpointPath = new EndpointPath(Elastic.Transport.HttpMethod.POST, pathAndQuery);

// Or, if the path does not contain query parameters:
// new EndpointPath(Elastic.Transport.HttpMethod.POST, "my_path")

var response = await client.Transport
    .RequestAsync<StringResponse>(
        endpointPath,
        PostData.String(body),
        null,
        null,
        cancellationToken: default)
    .ConfigureAwait(false);
```
