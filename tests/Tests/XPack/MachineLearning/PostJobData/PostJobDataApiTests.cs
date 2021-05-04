// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.PostJobData
{
	public class PostJobDataApiTests
		: MachineLearningIntegrationTestBase<PostJobDataResponse, IPostJobDataRequest, PostJobDataDescriptor, PostJobDataRequest>
	{
		public PostJobDataApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new Dictionary<string, object>
		{
			{ "@timestamp", new DateTime(2017, 9, 1) },
			{ "accept", 36320 },
			{ "deny", 4156 },
			{ "host", "server_2" },
			{ "response", 2.455821 },
			{ "service", "app_3" },
			{ "total", 40476 }
		};

		protected override int ExpectStatusCode => 202;

		protected override Func<PostJobDataDescriptor, IPostJobDataRequest> Fluent => f => f.Data(new Metric
		{
			Timestamp = new DateTime(2017, 9, 1),
			Accept = 36320,
			Deny = 4156,
			Host = "server_2",
			Response = 2.455821f,
			Service = "app_3",
			Total = 40476
		});

		protected override HttpMethod HttpMethod => HttpMethod.POST;

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
					Total = 40476
				}
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_ml/anomaly_detectors/{CallIsolatedValue}/_data";

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
				DeleteJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.PostJobData(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.PostJobDataAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.PostJobData(r),
			(client, r) => client.MachineLearning.PostJobDataAsync(r)
		);

		protected override PostJobDataDescriptor NewDescriptor() => new PostJobDataDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(PostJobDataResponse response)
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
			response.LastDataTime.Should().BeBefore(DateTimeOffset.UtcNow);
			response.InputRecordCount.Should().Be(1);
		}
	}

	public class PostJobDataWithResetStartAndResetEndApiTests
		: MachineLearningIntegrationTestBase<PostJobDataResponse, IPostJobDataRequest, PostJobDataDescriptor, PostJobDataRequest>
	{
		public PostJobDataWithResetStartAndResetEndApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new Dictionary<string, object>
		{
			{ "@timestamp", new DateTime(2017, 9, 1) },
			{ "accept", 36320 },
			{ "deny", 4156 },
			{ "host", "server_2" },
			{ "response", 2.455821 },
			{ "service", "app_3" },
			{ "total", 40476 }
		};

		protected override int ExpectStatusCode => 202;

		protected override Func<PostJobDataDescriptor, IPostJobDataRequest> Fluent => f => f
			.Data(new Metric
			{
				Timestamp = new DateTime(2017, 9, 1),
				Accept = 36320,
				Deny = 4156,
				Host = "server_2",
				Response = 2.455821f,
				Service = "app_3",
				Total = 40476
			})
			.ResetStart(new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero))
			.ResetEnd(new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

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
					Total = 40476
				}
			},
			ResetStart = new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero),
			ResetEnd = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero)
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath =>
			$"/_ml/anomaly_detectors/{CallIsolatedValue}/_data" +
			$"?reset_start={Uri.EscapeDataString(new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero).ToString("o"))}" +
			$"&reset_end={Uri.EscapeDataString(new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero).ToString("o"))}";

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
			foreach (var callUniqueValue in values) CloseJob(client, callUniqueValue.Value);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.PostJobData(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.PostJobDataAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.PostJobData(r),
			(client, r) => client.MachineLearning.PostJobDataAsync(r)
		);

		protected override PostJobDataDescriptor NewDescriptor() => new PostJobDataDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(PostJobDataResponse response)
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
