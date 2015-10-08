using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using Nest;
using Elasticsearch.Net;
using Xunit;
using Tests.Framework.Integration;

namespace Tests.Indices.IndexManagement
{
	[Collection(IntegrationContext.Indexing)]
	public class CreateIndexApiTests : ApiTestBase<IIndicesOperationResponse, ICreateIndexRequest, CreateIndexDescriptor, CreateIndexRequest>
	{
		static string IndexName { get; } = RandomString();

		public CreateIndexApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CreateIndex(IndexName, f),
			fluentAsync: (client, f) => client.CreateIndexAsync(IndexName, f),
			request: (client, r) => client.CreateIndex(r),
			requestAsync: (client, r) => client.CreateIndexAsync(r)
		);
		protected override void OnBeforeCall(IElasticClient client)
		{
			if (client.IndexExists(IndexName).Exists) client.DeleteIndex(IndexName);
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/{IndexName}";

		protected override object ExpectJson { get; } = new
		{
			settings = new Dictionary<string, object>
			{
				{ "index.number_of_replicas", 1 },
				{ "index.number_of_shards", 1 },
			}
		};

		protected override CreateIndexDescriptor NewDescriptor() => new CreateIndexDescriptor(IndexName);

		protected override Func<CreateIndexDescriptor, ICreateIndexRequest> Fluent => d => d
			.Settings(s => s
				.NumberOfReplicas(1)
				.NumberOfShards(1)
			);

		protected override CreateIndexRequest Initializer => new CreateIndexRequest(IndexName)
		{
			Settings = new Nest.IndexSettings()
			{
				NumberOfReplicas = 1,
				NumberOfShards = 1,
			}
		};
	}

}
