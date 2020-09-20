// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.DeleteDatafeed
{
	public class DeleteDatafeedApiTests
		: MachineLearningIntegrationTestBase<DeleteDatafeedResponse, IDeleteDatafeedRequest, DeleteDatafeedDescriptor, DeleteDatafeedRequest>
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
			(client, f) => client.MachineLearning.DeleteDatafeed(CallIsolatedValue + "-datafeed", f),
			(client, f) => client.MachineLearning.DeleteDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			(client, r) => client.MachineLearning.DeleteDatafeed(r),
			(client, r) => client.MachineLearning.DeleteDatafeedAsync(r)
		);

		protected override DeleteDatafeedDescriptor NewDescriptor() => new DeleteDatafeedDescriptor(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(DeleteDatafeedResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
