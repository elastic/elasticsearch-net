using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteDatafeed
{
	public class DeleteDatafeedApiTests
		: MachineLearningIntegrationTestBase<IDeleteDatafeedResponse, IDeleteDatafeedRequest, DeleteDatafeedDescriptor, DeleteDatafeedRequest>
	{
		public DeleteDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteDatafeedRequest Initializer => new DeleteDatafeedRequest(CallIsolatedValue + "-datafeed");
		protected override string UrlPath => $"_ml/datafeeds/{CallIsolatedValue}-datafeed";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteDatafeed(CallIsolatedValue + "-datafeed", f),
			(client, f) => client.DeleteDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			(client, r) => client.DeleteDatafeed(r),
			(client, r) => client.DeleteDatafeedAsync(r)
		);

		protected override DeleteDatafeedDescriptor NewDescriptor() => new DeleteDatafeedDescriptor(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(IDeleteDatafeedResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
