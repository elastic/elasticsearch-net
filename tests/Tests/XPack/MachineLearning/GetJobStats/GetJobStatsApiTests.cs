// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Configuration;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.GetJobStats
{
	public class GetJobStatsApiTests
		: MachineLearningIntegrationTestBase<GetJobStatsResponse, IGetJobStatsRequest, GetJobStatsDescriptor, GetJobStatsRequest>
	{
		public GetJobStatsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetJobStatsDescriptor, IGetJobStatsRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override GetJobStatsRequest Initializer => new GetJobStatsRequest();
		protected override string UrlPath => $"/_ml/anomaly_detectors/_stats";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) PutJob(client, callUniqueValue.Value);
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) DeleteJob(client, callUniqueValue.Value);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetJobStats(f),
			(client, f) => client.MachineLearning.GetJobStatsAsync(f),
			(client, r) => client.MachineLearning.GetJobStats(r),
			(client, r) => client.MachineLearning.GetJobStatsAsync(r)
		);

		protected override void ExpectResponse(GetJobStatsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);
			response.Jobs.Count.Should().BeGreaterOrEqualTo(1);

			var job = response.Jobs.Single(j => j.JobId == CallIsolatedValue);
			job.State.Should().Be(JobState.Closed);
			job.AssignmentExplanation.Should().BeNull();

			job.DataCounts.Should().NotBeNull();
			job.DataCounts.BucketCount.Should().Be(0);
			job.DataCounts.EmptyBucketCount.Should().Be(0);
			job.DataCounts.InputBytes.Should().Be(0);
			job.DataCounts.InputFieldCount.Should().Be(0);
			job.DataCounts.InputRecordCount.Should().Be(0);
			job.DataCounts.InvalidDateCount.Should().Be(0);
			job.DataCounts.MissingFieldCount.Should().Be(0);
			job.DataCounts.OutOfOrderTimestampCount.Should().Be(0);
			job.DataCounts.ProcessedFieldCount.Should().Be(0);
			job.DataCounts.ProcessedRecordCount.Should().Be(0);
			job.DataCounts.SparseBucketCount.Should().Be(0);

			job.ModelSizeStats.Should().NotBeNull();
			job.ModelSizeStats.BucketAllocationFailuresCount.Should().Be(0);
			job.ModelSizeStats.LogTime.Should().BeAfter(new DateTime(2017, 9, 1));
			job.ModelSizeStats.MemoryStatus.Should().Be(MemoryStatus.Ok);
			job.ModelSizeStats.ModelBytes.Should().BeGreaterOrEqualTo(0);
			job.ModelSizeStats.ResultType.Should().Be("model_size_stats");
			job.ModelSizeStats.TotalByFieldCount.Should().Be(0);
			job.ModelSizeStats.TotalOverFieldCount.Should().Be(0);
			job.ModelSizeStats.TotalPartitionFieldCount.Should().Be(0);

			if (TestConfiguration.Instance.InRange(">=7.3.0"))
			{
				job.TimingStats.Should().NotBeNull();
				job.TimingStats.JobId.Should().Be(job.JobId);
				job.TimingStats.BucketCount.Should().Be(0);
				job.TimingStats.MinimumBucketProcessingTimeMilliseconds.Should().Be(0);
				job.TimingStats.MaximumBucketProcessingTimeMilliseconds.Should().Be(0);
				job.TimingStats.AverageBucketProcessingTimeMilliseconds.Should().Be(0);
				job.TimingStats.ExponentialAverageBucketProcessingTimeMilliseconds.Should().Be(0);
			}
		}
	}

	public class GetJobStatsWithJobIdApiTests
		: MachineLearningIntegrationTestBase<GetJobStatsResponse, IGetJobStatsRequest, GetJobStatsDescriptor, GetJobStatsRequest>
	{
		public GetJobStatsWithJobIdApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetJobStatsDescriptor, IGetJobStatsRequest> Fluent => f => f.JobId(CallIsolatedValue);
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override GetJobStatsRequest Initializer => new GetJobStatsRequest(CallIsolatedValue);
		protected override string UrlPath => $"/_ml/anomaly_detectors/{CallIsolatedValue}/_stats";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) PutJob(client, callUniqueValue.Value);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetJobStats(f),
			(client, f) => client.MachineLearning.GetJobStatsAsync(f),
			(client, r) => client.MachineLearning.GetJobStats(r),
			(client, r) => client.MachineLearning.GetJobStatsAsync(r)
		);

		protected override void ExpectResponse(GetJobStatsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().Be(1);
			response.Jobs.Count.Should().Be(1);

			var firstJob = response.Jobs.First();
			firstJob.State.Should().Be(JobState.Closed);
			firstJob.AssignmentExplanation.Should().BeNull();

			firstJob.DataCounts.Should().NotBeNull();
			firstJob.DataCounts.JobId.Should().Be(CallIsolatedValue);
			firstJob.DataCounts.BucketCount.Should().Be(0);
			firstJob.DataCounts.EmptyBucketCount.Should().Be(0);
			firstJob.DataCounts.InputBytes.Should().Be(0);
			firstJob.DataCounts.InputFieldCount.Should().Be(0);
			firstJob.DataCounts.InputRecordCount.Should().Be(0);
			firstJob.DataCounts.InvalidDateCount.Should().Be(0);
			firstJob.DataCounts.MissingFieldCount.Should().Be(0);
			firstJob.DataCounts.OutOfOrderTimestampCount.Should().Be(0);
			firstJob.DataCounts.ProcessedFieldCount.Should().Be(0);
			firstJob.DataCounts.ProcessedRecordCount.Should().Be(0);
			firstJob.DataCounts.SparseBucketCount.Should().Be(0);

			firstJob.ModelSizeStats.Should().NotBeNull();
			firstJob.ModelSizeStats.JobId.Should().Be(CallIsolatedValue);
			firstJob.ModelSizeStats.BucketAllocationFailuresCount.Should().Be(0);
			firstJob.ModelSizeStats.LogTime.Should().BeAfter(new DateTime(2017, 9, 1));
			firstJob.ModelSizeStats.MemoryStatus.Should().Be(MemoryStatus.Ok);
			firstJob.ModelSizeStats.ModelBytes.Should().Be(0);
			firstJob.ModelSizeStats.ResultType.Should().Be("model_size_stats");
			firstJob.ModelSizeStats.TotalByFieldCount.Should().Be(0);
			firstJob.ModelSizeStats.TotalOverFieldCount.Should().Be(0);
			firstJob.ModelSizeStats.TotalPartitionFieldCount.Should().Be(0);
		}
	}
}
