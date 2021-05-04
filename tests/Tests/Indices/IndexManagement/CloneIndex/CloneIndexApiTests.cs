// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexManagement.CloneIndex
{
	[SkipVersion("<7.4.0", "clone index introduced in 7.4.0")]
	public class CloneIndexApiTests
		: ApiIntegrationTestBase<WritableCluster, CloneIndexResponse, ICloneIndexRequest, CloneIndexDescriptor, CloneIndexRequest>
	{
		private const string CloneSuffix = "_clone";

		public CloneIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var value in values)
			{
				var createIndexResponse = client.Indices.Create(value.Value, c => c
					.Settings(s => s
						.NumberOfShards(1)
						.NumberOfReplicas(0)
					)
				);

				if (!createIndexResponse.IsValid)
					throw new Exception($"exception whilst setting up integration test: {createIndexResponse.DebugInformation}");

				var updateSettings = client.Indices.UpdateSettings(value.Value, s => s
					.IndexSettings(i => i
						.BlocksWrite()
					)
				);

				if (!updateSettings.IsValid)
					throw new Exception($"exception whilst setting up integration test: {updateSettings.DebugInformation}");
			}
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			settings = new Dictionary<string, object>
			{
				{ "index.number_of_replicas", 0 },
				{ "index.number_of_shards", 1 },
				{ "index.queries.cache.enabled", true }
			},
			aliases = new Dictionary<string, object>
			{
				{ CallIsolatedValue + "-alias", new { is_write_index = true } }
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<CloneIndexDescriptor, ICloneIndexRequest> Fluent => d => d
			.Settings(s => s
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.Queries(q => q
					.Cache(c => c
						.Enabled()
					)
				)
			)
			.Aliases(a => a
				.Alias(CallIsolatedValue + "-alias", aa => aa
					.IsWriteIndex()
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override CloneIndexRequest Initializer => new CloneIndexRequest(CallIsolatedValue, CallIsolatedValue + CloneSuffix)
		{
			Settings = new Nest.IndexSettings
			{
				NumberOfReplicas = 0,
				NumberOfShards = 1,
				Queries = new QueriesSettings
				{
					Cache = new QueriesCacheSettings
					{
						Enabled = true
					}
				}
			},
			Aliases = new Aliases
			{
				{ CallIsolatedValue + "-alias", new Alias { IsWriteIndex = true } }
			}
		};

		protected override string UrlPath => $"/{CallIsolatedValue}/_clone/{CallIsolatedValue + CloneSuffix}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Clone(CallIsolatedValue, CallIsolatedValue + CloneSuffix, f),
			(client, f) => client.Indices.CloneAsync(CallIsolatedValue, CallIsolatedValue + CloneSuffix, f),
			(client, r) => client.Indices.Clone(r),
			(client, r) => client.Indices.CloneAsync(r)
		);

		protected override CloneIndexDescriptor NewDescriptor() => new CloneIndexDescriptor(CallIsolatedValue, CallIsolatedValue + CloneSuffix);

		protected override void ExpectResponse(CloneIndexResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
			response.ShardsAcknowledged.Should().BeTrue();
			response.Index.Should().Be(CallIsolatedValue + CloneSuffix);
		}
	}
}
