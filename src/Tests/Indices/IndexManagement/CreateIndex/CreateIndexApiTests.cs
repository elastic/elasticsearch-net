using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexManagement.CreateIndex
{
	public class CreateIndexApiTests : ApiIntegrationTestBase<WritableCluster, ICreateIndexResponse, ICreateIndexRequest, CreateIndexDescriptor, CreateIndexRequest>
	{
		public CreateIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CreateIndex(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.CreateIndexAsync(CallIsolatedValue, f),
			request: (client, r) => client.CreateIndex(r),
			requestAsync: (client, r) => client.CreateIndexAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/{CallIsolatedValue}";

		protected override object ExpectJson { get; } = new
		{
			settings = new Dictionary<string, object>
			{
				{ "index.number_of_replicas", 1 },
				{ "index.number_of_shards", 1 },
				{ "index.queries.cache.enabled", true },
			}
		};

		protected override CreateIndexDescriptor NewDescriptor() => new CreateIndexDescriptor(CallIsolatedValue);

		protected override Func<CreateIndexDescriptor, ICreateIndexRequest> Fluent => d => d
			.Settings(s => s
				.NumberOfReplicas(1)
				.NumberOfShards(1)
				.Queries(q => q
					.Cache(c => c
						.Enabled()
					)
				)
			);

		protected override CreateIndexRequest Initializer => new CreateIndexRequest(CallIsolatedValue)
		{
			Settings = new Nest_5_2_0.IndexSettings
			{
				NumberOfReplicas = 1,
				NumberOfShards = 1,
				Queries = new QueriesSettings
				{
					Cache = new QueriesCacheSettings
					{
						Enabled = true
					}
				}
			}
		};

		protected override void ExpectResponse(ICreateIndexResponse response)
		{
			response.IsValid.Should().BeTrue();
			response.Acknowledged.Should().BeTrue();
			response.ShardsAcknowledged.Should().BeTrue();

			var indexSettings = this.Client.GetIndexSettings(g => g.Index(CallIsolatedValue));

			indexSettings.IsValid.Should().BeTrue();
			indexSettings.Indices.Should().NotBeEmpty().And.ContainKey(CallIsolatedValue);

			var settings = indexSettings.Indices[CallIsolatedValue];

			settings.Settings.NumberOfShards.Should().Be(1);
			settings.Settings.NumberOfReplicas.Should().Be(1);
			settings.Settings.Queries.Cache.Enabled.Should().Be(true);
		}
	}
}
