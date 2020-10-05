// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.PreviewDataFeed
{
	//TODO what does an invalid request return here? this API returns a json array for the happy path
	public class PreviewDatafeedApiTests
		: MachineLearningIntegrationTestBase<PreviewDatafeedResponse<Metric>, IPreviewDatafeedRequest, PreviewDatafeedDescriptor,
			PreviewDatafeedRequest>
	{
		public PreviewDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override PreviewDatafeedRequest Initializer => new PreviewDatafeedRequest(CallIsolatedValue + "-datafeed");
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_ml/datafeeds/{CallIsolatedValue}-datafeed/_preview";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.PreviewDatafeed<Metric>(CallIsolatedValue + "-datafeed", f),
			(client, f) => client.MachineLearning.PreviewDatafeedAsync<Metric>(CallIsolatedValue + "-datafeed", f),
			(client, r) => client.MachineLearning.PreviewDatafeed<Metric>(r),
			(client, r) => client.MachineLearning.PreviewDatafeedAsync<Metric>(r)
		);

		protected override PreviewDatafeedDescriptor NewDescriptor() => new PreviewDatafeedDescriptor(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(PreviewDatafeedResponse<Metric> response)
		{
			response.IsValid.Should().BeTrue();
			response.Data.Count.Should().BeGreaterThan(0);
		}
	}
}
