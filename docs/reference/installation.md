---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/installation.html
---

# Installation [installation]

This page shows you how to install the .NET client for {{es}}.

::::{important}
The v8 client for .NET does not have complete feature parity with the v7 `NEST` client. It may not be suitable for for all applications until additional endpoints and features are supported. We therefore recommend you thoroughly review our [release notes](/release-notes/index.md) before attempting to migrate existing applications to the `Elastic.Clients.Elasticsearch` library. Until the new client supports all endpoints and features your application requires, you may continue to use the 7.17.x [NEST](https://www.nuget.org/packages/NEST) client to communicate with v8 Elasticsearch servers using compatibility mode. Refer to the [Connecting to Elasticsearch v8.x using the v7.17.x client documentation](https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/connecting-to-elasticsearch-v8.html) for guidance on configuring the 7.17.x client.
::::



## Installing the .NET client [dot-net-client]

For SDK style projects, you can install the {{es}} client by running the following .NET CLI command in your terminal:

```text
dotnet add package Elastic.Clients.Elasticsearch
```

This command adds a package reference to your project (csproj) file for the latest stable version of the client.

If you prefer, you may also manually add a package reference inside your project file:

```shell
<PackageReference Include="Elastic.Clients.Elasticsearch" Version="{latest-version}" />
```

*NOTE: The version number should reflect the latest published version from [NuGet.org](https://www.nuget.org/packages/Elastic.Clients.Elasticsearch). To install a different version, modify the version as necessary.*

For Visual Studio users, the .NET client can also be installed from the Package Manager Console inside Visual Studio using the following command:

```shell
Install-Package Elastic.Clients.Elasticsearch
```

Alternatively, search for `Elastic.Clients.Elasticsearch` in the NuGet Package Manager UI.

To learn how to connect the {{es}} client, refer to the [Connecting](/reference/connecting.md) section.


## Compatibility [compatibility]

The {{es}} client is compatible with currently maintained .NET runtime versions. Compatibility with End of Life (EOL) .NET runtimes is not guaranteed or supported.

Language clients are forward compatible; meaning that the clients support communicating with greater or equal minor versions of {{es}} without breaking. It does not mean that the clients automatically support new features of newer {{es}} versions; it is only possible after a release of a new client version. For example, a 8.12 client version won’t automatically support the new features of the 8.13 version of {{es}}, the 8.13 client version is required for that. {{es}} language clients are only backwards compatible with default distributions and without guarantees made.

| Elasticsearch Version | Elasticsearch-NET Branch | Supported |
| --- | --- | --- |
| main | main |  |
| 8.x | 8.x | 8.x |
| 7.x | 7.x | 7.17 |

Refer to the [end-of-life policy](https://www.elastic.co/support/eol) for more information.


## CI feed [ci-feed]

We publish CI builds of our client packages, including the latest unreleased features. If you want to experiment with the latest bits, you can add the CI feed to your list of NuGet package sources.

Feed URL: [https://f.feedz.io/elastic/all/nuget/index.json](https://f.feedz.io/elastic/all/nuget/index.json)

We do not recommend using CI builds for production applications as they are not formally supported until they are released.

