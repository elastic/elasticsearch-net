// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.GetInfluencers
{
	public class GetInfluencersApiTests
		: MachineLearningIntegrationTestBase<GetInfluencersResponse, IGetInfluencersRequest, GetInfluencersDescriptor, GetInfluencersRequest>
	{
		public GetInfluencersApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetInfluencersDescriptor, IGetInfluencersRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override GetInfluencersRequest Initializer => new GetInfluencersRequest(CallIsolatedValue)
		{
			End = new DateTimeOffset(2016, 6, 2, 01, 00, 00, TimeSpan.Zero)
		};

		protected override string UrlPath => $"/_ml/anomaly_detectors/{CallIsolatedValue}/results/influencers";

		protected override GetInfluencersDescriptor NewDescriptor() => new GetInfluencersDescriptor(CallIsolatedValue)
			.End(new DateTimeOffset(2016, 6, 2, 01, 00, 00, TimeSpan.Zero));

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexInfluencer(client, callUniqueValue.Value, new DateTimeOffset(2016, 6, 2, 00, 00, 00, TimeSpan.Zero));
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetInfluencers(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.GetInfluencersAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.GetInfluencers(r),
			(client, r) => client.MachineLearning.GetInfluencersAsync(r)
		);

		protected override void ExpectResponse(GetInfluencersResponse response)
		{
			response.ShouldBeValid();
			response.Influencers.Should().HaveCount(1);
			response.Count.Should().Be(1);

			response.Influencers.First().JobId.Should().Be(CallIsolatedValue);
			response.Influencers.First().ResultType.Should().Be("influencer");
			response.Influencers.First().InfluencerFieldName.Should().Be("foo");
			response.Influencers.First().InfluencerFieldValue.Should().Be("bar");
			response.Influencers.First().InfluencerScore.Should().Be(50);
			response.Influencers.First().InitialInfluencerScore.Should().Be(0);
			response.Influencers.First().Probability.Should().Be(0);
			response.Influencers.First().BucketSpan.Should().Be(1);
			response.Influencers.First().IsInterim.Should().Be(false);
			response.Influencers.First().Timestamp.Should().Be(new DateTimeOffset(2016, 6, 2, 00, 00, 00, TimeSpan.Zero));
		}
	}
}
