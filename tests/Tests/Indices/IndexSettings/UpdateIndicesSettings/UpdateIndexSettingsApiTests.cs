/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using Elasticsearch.Net;
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
