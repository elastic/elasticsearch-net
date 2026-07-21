---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/installation.html
---

# Installation [installation]

This page shows you how to install the .NET client for {{es}}.

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

Language clients are **forward compatible**:

Given a constant major version of the client, each related minor version is compatible with its equivalent- and all later {{es}} minor versions of the **same or next higher** major version.

For example:

| Client Version | Compatible with {{es}} `8.x` | Compatible with {{es}} `9.x` | Compatible with {{es}} `10.x` |
| ---: | :-- | :-- | :-- |
| 9.x | ❌ no | ✅ yes | ✅ yes |
| 8.x | ✅ yes | ✅ yes | ❌ no |

Language clients are also **backward compatible** across minor versions within the **same** major version (without strong guarantees), but **never** backward compatible with earlier {{es}} major versions.

:::{note}

Compatibility does not imply feature parity. For example, an `8.12` client is compatible with `8.13`, but does not support any of the new features introduced in {{es}} `8.13`.

:::

Refer to the [end-of-life policy](https://www.elastic.co/support/eol) for more information.
