using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.PostJobData
{
	public class PostJobDataApiTests : MachineLearningIntegrationTestBase<IPostJobDataResponse, IPostJobDataRequest, PostJobDataDescriptor, PostJobDataRequest>
	{
		public PostJobDataApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				CloseJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PostJobData(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PostJobDataAsync(CallIsolatedValue, f),
			request: (client, r) => client.PostJobData(r),
			requestAsync: (client, r) => client.PostJobDataAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 202;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_data";
		protected override bool SupportsDeserialization => false;
		protected override PostJobDataDescriptor NewDescriptor() => new PostJobDataDescriptor(CallIsolatedValue);

		protected override object ExpectJson => new JObject
		{
			{ "@timestamp", new DateTime(2017, 9, 1) },
			{ "accept", 36320 },
			{ "deny", 4156 },
			{ "host", "server_2" },
			{ "response", 2.455821 },
			{ "service", "app_3" },
			{ "total", 40476 }
		};

		protected override Func<PostJobDataDescriptor, IPostJobDataRequest> Fluent => f => f.Data(new Metric
		{
			Timestamp = new DateTime(2017, 9, 1),
			Accept = 36320,
			Deny = 4156,
			Host = "server_2",
			Response = 2.455821f,
			Service = "app_3",
			Total  = 40476
		});

		protected override PostJobDataRequest Initializer => new PostJobDataRequest(CallIsolatedValue)
		{
			Data = new[]
			{
				new Metric
				{
					Timestamp = new DateTime(2017, 9, 1),
					Accept = 36320,
					Deny = 4156,
					Host = "server_2",
					Response = 2.4558210155f,
					Service = "app_3",
					Total  = 40476
				}
			}
		};

		protected override void ExpectResponse(IPostJobDataResponse response)
		{
			response.ShouldBeValid();
			response.JobId.Should().Be(CallIsolatedValue);
			response.ProcessedRecordCount.Should().Be(0);
			response.ProcessedFieldCount.Should().Be(0);
			response.InputBytes.Should().BeGreaterOrEqualTo(1);
			response.InputFieldCount.Should().Be(6);
			response.InvalidDateCount.Should().Be(1);
			response.MissingFieldCount.Should().Be(0);
			response.OutOfOrderTimestampCount.Should().Be(0);
			response.EmptyBucketCount.Should().Be(0);
			response.SparseBucketCount.Should().Be(0);
			response.BucketCount.Should().Be(0);
			response.LastDataTime.Should().BeAfter(new DateTime(2017, 9, 1));
			response.InputRecordCount.Should().Be(1);
		}
	}

	public class PostJobDataWithResetStartAndResetEndApiTests : MachineLearningIntegrationTestBase<IPostJobDataResponse, IPostJobDataRequest, PostJobDataDescriptor, PostJobDataRequest>
	{
		public PostJobDataWithResetStartAndResetEndApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				CloseJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PostJobData(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PostJobDataAsync(CallIsolatedValue, f),
			request: (client, r) => client.PostJobData(r),
			requestAsync: (client, r) => client.PostJobDataAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 202;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath =>
			$"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_data"+
			$"?reset_start={Uri.EscapeDataString(new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero).ToString("o"))}"+
			$"&reset_end={Uri.EscapeDataString(new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero).ToString("o"))}";

		protected override bool SupportsDeserialization => false;
		protected override PostJobDataDescriptor NewDescriptor() => new PostJobDataDescriptor(CallIsolatedValue);

		protected override object ExpectJson => new JObject
		{
			{ "@timestamp", new DateTime(2017, 9, 1) },
			{ "accept", 36320 },
			{ "deny", 4156 },
			{ "host", "server_2" },
			{ "response", 2.455821 },
			{ "service", "app_3" },
			{ "total", 40476 }
		};

		protected override Func<PostJobDataDescriptor, IPostJobDataRequest> Fluent => f => f
			.Data(new Metric
			{
				Timestamp = new DateTime(2017, 9, 1),
				Accept = 36320,
				Deny = 4156,
				Host = "server_2",
				Response = 2.455821f,
				Service = "app_3",
				Total  = 40476
			})
			.ResetStart(new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero))
			.ResetEnd(new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero));

		protected override PostJobDataRequest Initializer => new PostJobDataRequest(CallIsolatedValue)
		{
			Data = new[]
			{
				new Metric
				{
					Timestamp = new DateTime(2017, 9, 1),
					Accept = 36320,
					Deny = 4156,
					Host = "server_2",
					Response = 2.4558210155f,
					Service = "app_3",
					Total  = 40476
				}
			},
			ResetStart = new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero),
			ResetEnd = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero)
		};

		protected override void ExpectResponse(IPostJobDataResponse response)
		{
			response.ShouldBeValid();
			response.JobId.Should().Be(CallIsolatedValue);
			response.ProcessedRecordCount.Should().Be(0);
			response.ProcessedFieldCount.Should().Be(0);
			response.InputBytes.Should().BeGreaterOrEqualTo(1);
			response.InputFieldCount.Should().Be(6);
			response.InvalidDateCount.Should().Be(1);
			response.MissingFieldCount.Should().Be(0);
			response.OutOfOrderTimestampCount.Should().Be(0);
			response.EmptyBucketCount.Should().Be(0);
			response.SparseBucketCount.Should().Be(0);
			response.BucketCount.Should().Be(0);
			response.LastDataTime.Should().BeAfter(new DateTime(2017, 9, 1));
			response.InputRecordCount.Should().Be(1);
		}
	}
}
