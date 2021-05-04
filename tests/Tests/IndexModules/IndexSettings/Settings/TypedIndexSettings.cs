// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest;

namespace Tests.IndexModules.IndexSettings.Settings
{
	public class TypedIndexSettings
	{
		/**
		 */

		public class Usage : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, Nest.IndexSettings>
		{
			protected override object ExpectJson => new Dictionary<string, object>
			{
				{ "any.setting", "can be set" },
				{ "doubles", 1.1 },
				{ "bools", false },
				{ "enums", "offsets" },
				{ "index.number_of_replicas", 2 },
				{ "index.auto_expand_replicas", "1-3" },
				{ "index.default_pipeline", "a-default-pipeline" },
				{ "index.final_pipeline", "a-final-pipeline" },
				{ "index.refresh_interval", -1 },
				{ "index.blocks.read_only", true },
				{ "index.blocks.read", true },
				{ "index.blocks.write", true },
				{ "index.blocks.read_only_allow_delete", true },
				{ "index.blocks.metadata", true },
				{ "index.priority", 11 },
				{ "index.recovery.initial_shards", "full-1" },
				{ "index.requests.cache.enable", true },
				{ "index.routing.allocation.total_shards_per_node", 10 },
				{ "index.unassigned.node_left.delayed_timeout", "1m" },
				{ "index.number_of_shards", 1 },
				{ "index.store.type", "mmapfs" },
				{ "index.queries.cache.enabled", true },
			};

			/**
			 *
			 */
			protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => s => s
				.Setting("any.setting", "can be set")
				.Setting("doubles", 1.1)
				.Setting("bools", false)
				.Setting("enums", IndexOptions.Offsets)
				.NumberOfShards(1)
				.NumberOfReplicas(2)
				.DefaultPipeline("a-default-pipeline")
				.FinalPipeline("a-final-pipeline")
				.AutoExpandReplicas("1-3")
				.BlocksMetadata()
				.BlocksRead()
				.BlocksReadOnly()
				.BlocksWrite()
				.BlocksReadOnlyAllowDelete()
				.Priority(11)
				.RecoveryInitialShards(RecoveryInitialShards.FullMinusOne)
				.RequestsCacheEnabled()
				.RoutingAllocationTotalShardsPerNode(10)
				.UnassignedNodeLeftDelayedTimeout(TimeSpan.FromMinutes(1))
				.RefreshInterval(-1)
				.FileSystemStorageImplementation(FileSystemStorageImplementation.MMap)
				.Queries(q => q.Cache(c => c.Enabled()));

			/**
			 */
			protected override Nest.IndexSettings Initializer =>
				new Nest.IndexSettings(new Dictionary<string, object>
				{
					{ "any.setting", "can be set" },
					{ "doubles", 1.1 },
					{ "bools", false },
					{ "enums", IndexOptions.Offsets },
				})
				{
					NumberOfShards = 1,
					NumberOfReplicas = 2,
					DefaultPipeline = "a-default-pipeline",
					FinalPipeline = "a-final-pipeline",
					AutoExpandReplicas = "1-3",
					BlocksMetadata = true,
					BlocksRead = true,
					BlocksReadOnly = true,
					BlocksWrite = true,
					BlocksReadOnlyAllowDelete = true,
					Priority = 11,
					RecoveryInitialShards = RecoveryInitialShards.FullMinusOne,
					RequestsCacheEnabled = true,
					RoutingAllocationTotalShardsPerNode = 10,
					UnassignedNodeLeftDelayedTimeout = "1m",
					RefreshInterval = -1,
					FileSystemStorageImplementation = FileSystemStorageImplementation.MMap,
					Queries = new QueriesSettings { Cache = new QueriesCacheSettings { Enabled = true } }
				};
		}
	}
}
