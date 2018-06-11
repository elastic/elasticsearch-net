using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.DeleteDatafeed
{
	public class DeleteDatafeedApiTests : MachineLearningIntegrationTestBase<IDeleteDatafeedResponse, IDeleteDatafeedRequest, DeleteDatafeedDescriptor, DeleteDatafeedRequest>
	{
		public DeleteDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteDatafeed(CallIsolatedValue + "-datafeed", f),
			fluentAsync: (client, f) => client.DeleteDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			request: (client, r) => client.DeleteDatafeed(r),
			requestAsync: (client, r) => client.DeleteDatafeedAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}-datafeed";
		protected override bool SupportsDeserialization => true;
		protected override DeleteDatafeedDescriptor NewDescriptor() => new DeleteDatafeedDescriptor(CallIsolatedValue + "-datafeed");
		protected override object ExpectJson => null;
		protected override Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> Fluent => f => f;
		protected override DeleteDatafeedRequest Initializer => new DeleteDatafeedRequest(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(IDeleteDatafeedResponse response)
		{
			response.Acknowledged.Should().BeTrue();
		}
	}
}
