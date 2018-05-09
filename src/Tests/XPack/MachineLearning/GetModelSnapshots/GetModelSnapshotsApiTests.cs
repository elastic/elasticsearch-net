using System;
using System.Linq;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetModelSnapshots
{
	public class GetModelSnapshotsApiTests : MachineLearningIntegrationTestBase<IGetModelSnapshotsResponse, IGetModelSnapshotsRequest, GetModelSnapshotsDescriptor, GetModelSnapshotsRequest>
	{
		public GetModelSnapshotsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexSnapshot(client, callUniqueValue.Value, "1");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetModelSnapshots(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetModelSnapshotsAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetModelSnapshots(r),
			requestAsync: (client, r) => client.GetModelSnapshotsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/model_snapshots";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> Fluent => f => f;
		protected override GetModelSnapshotsDescriptor NewDescriptor() => new GetModelSnapshotsDescriptor(CallIsolatedValue);
		protected override GetModelSnapshotsRequest Initializer => new GetModelSnapshotsRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetModelSnapshotsResponse response)
		{
			response.ShouldBeValid();
			response.ModelSnapshots.Should().HaveCount(1);
			response.Count.Should().Be(1);

			var modelSnapshot = response.ModelSnapshots.First();
			modelSnapshot.JobId.Should().Be(CallIsolatedValue);
			modelSnapshot.Timestamp.Should().Be(new DateTimeOffset(2016, 6, 2, 00, 00, 00, TimeSpan.Zero));
			modelSnapshot.SnapshotId.Should().Be("1");
			modelSnapshot.SnapshotDocCount.Should().Be(1);
			modelSnapshot.Retain.Should().Be(false);
		}
	}

	public class GetModelSnapshotsWithSnapshotIdApiTests : MachineLearningIntegrationTestBase<IGetModelSnapshotsResponse, IGetModelSnapshotsRequest, GetModelSnapshotsDescriptor, GetModelSnapshotsRequest>
	{
		public GetModelSnapshotsWithSnapshotIdApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexSnapshot(client, callUniqueValue.Value, "1");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetModelSnapshots(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetModelSnapshotsAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetModelSnapshots(r),
			requestAsync: (client, r) => client.GetModelSnapshotsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/model_snapshots/1";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> Fluent => f => f.SnapshotId(1);
		protected override GetModelSnapshotsDescriptor NewDescriptor() => new GetModelSnapshotsDescriptor(CallIsolatedValue);
		protected override GetModelSnapshotsRequest Initializer => new GetModelSnapshotsRequest(CallIsolatedValue, 1);

		protected override void ExpectResponse(IGetModelSnapshotsResponse response)
		{
			response.ShouldBeValid();
			response.ModelSnapshots.Should().HaveCount(1);
			response.Count.Should().Be(1);

			response.ModelSnapshots.First().JobId.Should().Be(CallIsolatedValue);
			response.ModelSnapshots.First().Timestamp.Should().Be(new DateTimeOffset(2016, 6, 2, 00, 00, 00, TimeSpan.Zero));
			response.ModelSnapshots.First().SnapshotId.Should().Be("1");
			response.ModelSnapshots.First().SnapshotDocCount.Should().Be(1);
			response.ModelSnapshots.First().Retain.Should().Be(false);
		}
	}
}
