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
	public class CreateIndex : ApiCallIntegration<IIndicesOperationResponse, ICreateIndexRequest, CreateIndexDescriptor, CreateIndexRequest>
	{
		public CreateIndex(IndexingCluster cluster, ApiUsage usage) : base(cluster, usage) { }

		public string IndexName { get; } = RandomString();

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CreateIndex(IndexName, f),
			fluentAsync: (client, f) => client.CreateIndexAsync(this.IndexName, f),
			request: (client, r) => client.CreateIndex(r),
			requestAsync: (client, r) => client.CreateIndexAsync(r)
		);
		protected override void OnBeforeCall(IElasticClient client)
		{
			if (client.IndexExists(IndexName).Exists) client.DeleteIndex(IndexName);
		}

		public override bool ExpectIsValid => true;
		public override int ExpectStatusCode => 200;
		public override HttpMethod HttpMethod => HttpMethod.POST;
		public override string UrlPath => "/x/x/x";

		protected override object ExpectJson { get; } = new object
		{

		};

		protected override Func<CreateIndexDescriptor, ICreateIndexRequest> Fluent => d => d
			.Analysis(a => a
				.Analyzers(an => an
				)
			);

		protected override CreateIndexRequest Initializer => new CreateIndexRequest(this.IndexName)
		{
		};

	}
}
