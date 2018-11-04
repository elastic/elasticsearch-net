using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.AliasManagement.DeleteAlias
{
	public class DeleteAliasApiTests
		: ApiIntegrationTestBase<WritableCluster, IDeleteAliasResponse, IDeleteAliasRequest, DeleteAliasDescriptor, DeleteAliasRequest>
	{
		public DeleteAliasApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteAliasDescriptor, IDeleteAliasRequest> Fluent => null;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteAliasRequest Initializer => new DeleteAliasRequest(Infer.AllIndices, Names);
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_all/_alias/{CallIsolatedValue + "-alias"}";
		private Names Names => Infer.Names(CallIsolatedValue + "-alias");

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
				client.CreateIndex(index, c => c
					.Aliases(aa => aa.Alias(index + "-alias"))
				);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteAlias(Infer.AllIndices, Names),
			(client, f) => client.DeleteAliasAsync(Infer.AllIndices, Names),
			(client, r) => client.DeleteAlias(r),
			(client, r) => client.DeleteAliasAsync(r)
		);
	}
}
