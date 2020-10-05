// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexSettings.UpdateIndicesSettings
{
	public class UpdateIndexSettingsApiTests
		: ApiIntegrationTestBase<WritableCluster, UpdateIndexSettingsResponse, IUpdateIndexSettingsRequest, UpdateIndexSettingsDescriptor,
			UpdateIndexSettingsRequest>
	{
		public UpdateIndexSettingsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new Dictionary<string, object>
		{
			{ "index.blocks.write", false },
			{ "index.number_of_replicas", 2 },
			{ "index.priority", 2 }
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.IndexSettings(i => i
				.BlocksWrite(false)
				.NumberOfReplicas(2)
				.Priority(2)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override UpdateIndexSettingsRequest Initializer => new UpdateIndexSettingsRequest(CallIsolatedValue)
		{
			IndexSettings = new Nest.IndexSettings(new Dictionary<string, object>
			{
				{ "index.number_of_replicas", 3 },
				{ "index.priority", 2 }
			})
			{
				BlocksWrite = false,
				NumberOfReplicas = 2, //this should win from the value provided in the base dictionary
			}
		};

		protected override string UrlPath => $"{CallIsolatedValue}/_settings";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var value in values)
			{
				var index = value.Value;
				var createIndexResponse = client.Indices.Create(index);

				if (!createIndexResponse.IsValid)
					throw new Exception($"Invalid response when setting up index for integration test {GetType().Name}");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.UpdateSettings(CallIsolatedValue, f),
			(client, f) => client.Indices.UpdateSettingsAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.UpdateSettings(r),
			(client, r) => client.Indices.UpdateSettingsAsync(r)
		);
	}

	public class UpdateIndexSettingsRefreshIntervalNullApiTests
		: ApiIntegrationTestBase<WritableCluster, UpdateIndexSettingsResponse, IUpdateIndexSettingsRequest, UpdateIndexSettingsDescriptor,
			UpdateIndexSettingsRequest>
	{
		public UpdateIndexSettingsRefreshIntervalNullApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new Dictionary<string, object>
		{
			{ "index.refresh_interval", null },
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.IndexSettings(i => i
				.RefreshInterval(null)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override UpdateIndexSettingsRequest Initializer => new UpdateIndexSettingsRequest(CallIsolatedValue)
		{
			IndexSettings = new Nest.IndexSettings
			{
				RefreshInterval = null
			}
		};

		protected override string UrlPath => $"{CallIsolatedValue}/_settings";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var value in values)
			{
				var index = value.Value;
				var createIndexResponse = client.Indices.Create(index);

				if (!createIndexResponse.IsValid)
					throw new Exception($"Invalid response when setting up index for integration test {GetType().Name}");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.UpdateSettings(CallIsolatedValue, f),
			(client, f) => client.Indices.UpdateSettingsAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.UpdateSettings(r),
			(client, r) => client.Indices.UpdateSettingsAsync(r)
		);
	}
}
