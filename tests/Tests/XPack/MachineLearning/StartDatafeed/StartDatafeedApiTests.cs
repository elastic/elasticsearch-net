// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.StartDatafeed
{
	public class StartDatafeedApiTests
		: MachineLearningIntegrationTestBase<StartDatafeedResponse, IStartDatafeedRequest, StartDatafeedDescriptor, StartDatafeedRequest>
	{
		public StartDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private DateTimeOffset Now => DateTimeOffset.Now;

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<StartDatafeedDescriptor, IStartDatafeedRequest> Fluent => f => f
			.Start(Now)
			.End(Now.AddSeconds(10));

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override StartDatafeedRequest Initializer => new StartDatafeedRequest(CallIsolatedValue + "-datafeed")
		{
			Start = Now,
			End = Now.AddSeconds(10)
		};

		protected override string UrlPath => $"_ml/datafeeds/{CallIsolatedValue}-datafeed/_start";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				StopDatafeed(client, callUniqueValue.Value);
				CloseJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.StartDatafeed(CallIsolatedValue + "-datafeed", f),
			(client, f) => client.MachineLearning.StartDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			(client, r) => client.MachineLearning.StartDatafeed(r),
			(client, r) => client.MachineLearning.StartDatafeedAsync(r)
		);

		protected override StartDatafeedDescriptor NewDescriptor() => new StartDatafeedDescriptor(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(StartDatafeedResponse response)
		{
			response.Started.Should().BeTrue();

			if (TestClient.Configuration.InRange(">=7.8.0"))
				response.Node.Should().NotBeNullOrEmpty();
		}
	}
}
