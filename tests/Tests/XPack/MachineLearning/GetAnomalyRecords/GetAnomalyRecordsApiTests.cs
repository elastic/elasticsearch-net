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

namespace Tests.XPack.MachineLearning.GetAnomalyRecords
{
	public class GetAnomalyRecordsApiTests
		: MachineLearningIntegrationTestBase<GetAnomalyRecordsResponse, IGetAnomalyRecordsRequest, GetAnomalyRecordsDescriptor,
			GetAnomalyRecordsRequest>
	{
		private static readonly DateTimeOffset Timestamp = new DateTimeOffset(2016, 6, 2, 00, 00, 00, TimeSpan.Zero);

		public GetAnomalyRecordsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> Fluent => f => f
			.Start(Timestamp.AddHours(-1))
			.End(Timestamp.AddHours(1));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override GetAnomalyRecordsRequest Initializer => new GetAnomalyRecordsRequest(CallIsolatedValue)
		{
			Start = Timestamp.AddHours(-1), End = Timestamp.AddHours(1)
		};

		protected override string UrlPath => $"/_ml/anomaly_detectors/{CallIsolatedValue}/results/records";

		protected override GetAnomalyRecordsDescriptor NewDescriptor() => new GetAnomalyRecordsDescriptor(CallIsolatedValue);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexAnomalyRecord(client, callUniqueValue.Value, Timestamp);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetAnomalyRecords(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.GetAnomalyRecordsAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.GetAnomalyRecords(r),
			(client, r) => client.MachineLearning.GetAnomalyRecordsAsync(r)
		);

		protected override void ExpectResponse(GetAnomalyRecordsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().Be(1);
			response.Records.Should().HaveCount(1);
			response.Records.First().ResultType.Should().Be("record");
			response.Records.First().Probability.Should().Be(0);
			response.Records.First().RecordScore.Should().Be(80);
			response.Records.First().InitialRecordScore.Should().Be(0);
			response.Records.First().BucketSpan.Should().Be(1);
			response.Records.First().DetectorIndex.Should().Be(0);
			response.Records.First().IsInterim.Should().Be(true);
			response.Records.First().Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
		}
	}
}
