using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteExpiredData
{
	public class DeleteExpiredDataApiTests
		: MachineLearningIntegrationTestBase<IDeleteExpiredDataResponse, IDeleteExpiredDataRequest, DeleteExpiredDataDescriptor,
			DeleteExpiredDataRequest>
	{
		public DeleteExpiredDataApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteExpiredDataRequest Initializer => new DeleteExpiredDataRequest();
		protected override string UrlPath => $"_ml/_delete_expired_data";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteExpiredData(f),
			(client, f) => client.DeleteExpiredDataAsync(f),
			(client, r) => client.DeleteExpiredData(r),
			(client, r) => client.DeleteExpiredDataAsync(r)
		);

		protected override DeleteExpiredDataDescriptor NewDescriptor() => new DeleteExpiredDataDescriptor();

		protected override void ExpectResponse(IDeleteExpiredDataResponse response) => response.Deleted.Should().BeTrue();
	}
}
