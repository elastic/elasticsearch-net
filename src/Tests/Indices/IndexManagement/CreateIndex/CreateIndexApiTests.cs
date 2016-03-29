using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexManagement.CreateIndex
{
	[Collection(IntegrationContext.Indexing)]
	public class CreateIndexApiTests : ApiIntegrationTestBase<ICreateIndexResponse, ICreateIndexRequest, CreateIndexDescriptor, CreateIndexRequest>
	{
		public CreateIndexApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
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
			}
		};

		protected override CreateIndexDescriptor NewDescriptor() => new CreateIndexDescriptor(CallIsolatedValue);

		protected override Func<CreateIndexDescriptor, ICreateIndexRequest> Fluent => d => d
			.Settings(s => s
				.NumberOfReplicas(1)
				.NumberOfShards(1)
			);

		protected override CreateIndexRequest Initializer => new CreateIndexRequest(CallIsolatedValue)
		{
			Settings = new Nest.IndexSettings()
			{
				NumberOfReplicas = 1,
				NumberOfShards = 1,
			}
		};
	}
}
