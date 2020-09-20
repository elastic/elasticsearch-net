// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.StopDatafeed
{
	public class StopDatafeedApiTests
		: MachineLearningIntegrationTestBase<StopDatafeedResponse, IStopDatafeedRequest, StopDatafeedDescriptor, StopDatafeedRequest>
	{
		public StopDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<StopDatafeedDescriptor, IStopDatafeedRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override StopDatafeedRequest Initializer => new StopDatafeedRequest(CallIsolatedValue + "-datafeed");
		protected override string UrlPath => $"_ml/datafeeds/{CallIsolatedValue}-datafeed/_stop";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
				StartDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) CloseJob(client, callUniqueValue.Value);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.StopDatafeed(CallIsolatedValue + "-datafeed", f),
			(client, f) => client.MachineLearning.StopDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			(client, r) => client.MachineLearning.StopDatafeed(r),
			(client, r) => client.MachineLearning.StopDatafeedAsync(r)
		);

		protected override StopDatafeedDescriptor NewDescriptor() => new StopDatafeedDescriptor(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(StopDatafeedResponse response) => response.Stopped.Should().BeTrue();
	}
}
