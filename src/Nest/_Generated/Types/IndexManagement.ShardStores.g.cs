// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
// Run the following in the root of the repository:
//
// ------------------------------------------------

using Elastic.Transport.Products.Elasticsearch.Failures;
using OneOf;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Nest.IndexManagement.ShardStores
{
	public partial class IndicesShardStores
	{
		[JsonPropertyName("shards")]
		public Dictionary<string, Nest.IndexManagement.ShardStores.ShardStoreWrapper> Shards
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ShardStore
	{
		[JsonPropertyName("allocation")]
		public Nest.IndexManagement.ShardStores.ShardStoreAllocation Allocation
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("allocation_id")]
		public Nest.Id AllocationId
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("attributes")]
		public Dictionary<string, object> Attributes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("id")]
		public Nest.Id Id
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("legacy_version")]
		public Nest.VersionNumber LegacyVersion
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("name")]
		public Nest.Name Name
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("store_exception")]
		public Nest.IndexManagement.ShardStores.ShardStoreException StoreException
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("transport_address")]
		public Nest.TransportAddress TransportAddress
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ShardStoreException
	{
		[JsonPropertyName("reason")]
		public string Reason
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("type")]
		public string Type
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ShardStoreWrapper
	{
		[JsonPropertyName("stores")]
		public IReadOnlyCollection<Nest.IndexManagement.ShardStores.ShardStore> Stores
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}
}