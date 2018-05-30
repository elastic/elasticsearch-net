using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetJobStats
{
	public class GetJobStatsApiTests : MachineLearningIntegrationTestBase<IGetJobStatsResponse, IGetJobStatsRequest, GetJobStatsDescriptor, GetJobStatsRequest>
	{
		public GetJobStatsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetJobStats(f),
			fluentAsync: (client, f) => client.GetJobStatsAsync(f),
			request: (client, r) => client.GetJobStats(r),
			requestAsync: (client, r) => client.GetJobStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/_stats";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetJobStatsDescriptor, IGetJobStatsRequest> Fluent => f => f;
		protected override GetJobStatsRequest Initializer => new GetJobStatsRequest();

		protected override void ExpectResponse(IGetJobStatsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);
			response.Jobs.Count.Should().BeGreaterOrEqualTo(1);

			var firstJob = response.Jobs.First();
			firstJob.State.Should().Be(JobState.Closed);
			firstJob.AssignmentExplanation.Should().BeNull();

			firstJob.DataCounts.Should().NotBeNull();
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

	public class GetJobStatsWithJobIdApiTests : MachineLearningIntegrationTestBase<IGetJobStatsResponse, IGetJobStatsRequest, GetJobStatsDescriptor, GetJobStatsRequest>
	{
		public GetJobStatsWithJobIdApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetJobStats(f),
			fluentAsync: (client, f) => client.GetJobStatsAsync(f),
			request: (client, r) => client.GetJobStats(r),
			requestAsync: (client, r) => client.GetJobStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_stats";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetJobStatsDescriptor, IGetJobStatsRequest> Fluent => f => f.JobId(CallIsolatedValue);
		protected override GetJobStatsRequest Initializer => new GetJobStatsRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetJobStatsResponse response)
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
