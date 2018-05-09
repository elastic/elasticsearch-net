using System;
using System.Linq;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.GetDatafeeds
{
	public class GetDatafeedsApiTests : MachineLearningIntegrationTestBase<IGetDatafeedsResponse, IGetDatafeedsRequest, GetDatafeedsDescriptor, GetDatafeedsRequest>
	{
		public GetDatafeedsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetDatafeeds(f),
			fluentAsync: (client, f) => client.GetDatafeedsAsync(f),
			request: (client, r) => client.GetDatafeeds(r),
			requestAsync: (client, r) => client.GetDatafeedsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"_xpack/ml/datafeeds";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> Fluent => f => f;

		protected override void ExpectResponse(IGetDatafeedsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);

			var firstDatafeed = response.Datafeeds.FirstOrDefault();

			firstDatafeed.Should().NotBeNull();
			firstDatafeed.DatafeedId.Should().NotBeNullOrWhiteSpace();
			firstDatafeed.JobId.Should().NotBeNullOrWhiteSpace();

			firstDatafeed.QueryDelay.Should().NotBeNull("QueryDelay");
			firstDatafeed.QueryDelay.Should().BeGreaterThan(new Time("1nanos"));

			firstDatafeed.Indices.Should().NotBeNull("Indices");
			firstDatafeed.Indices.Should().Be(Nest.Indices.Parse("server-metrics"));

			firstDatafeed.Types.Should().NotBeNull("Types");
			firstDatafeed.Types.Should().Be(Types.Parse("metric"));

			firstDatafeed.ScrollSize.Should().Be(1000);

			firstDatafeed.ChunkingConfig.Should().NotBeNull();
			firstDatafeed.ChunkingConfig.Mode.Should().Be(ChunkingMode.Auto);

			firstDatafeed.Query.Should().NotBeNull();

			response.ShouldBeValid();
		}
	}

	public class GetDatafeedsWithDatafeedIdApiTests : MachineLearningIntegrationTestBase<IGetDatafeedsResponse, IGetDatafeedsRequest, GetDatafeedsDescriptor, GetDatafeedsRequest>
	{
		public GetDatafeedsWithDatafeedIdApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetDatafeeds(f),
			fluentAsync: (client, f) => client.GetDatafeedsAsync(f),
			request: (client, r) => client.GetDatafeeds(r),
			requestAsync: (client, r) => client.GetDatafeedsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}-datafeed";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> Fluent => f => f.DatafeedId(CallIsolatedValue + "-datafeed");
		protected override GetDatafeedsRequest Initializer => new GetDatafeedsRequest(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(IGetDatafeedsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);

			var firstDatafeed = response.Datafeeds.FirstOrDefault();

			firstDatafeed.Should().NotBeNull();
			firstDatafeed.DatafeedId.Should().NotBeNullOrWhiteSpace();
			firstDatafeed.JobId.Should().NotBeNullOrWhiteSpace();

			firstDatafeed.QueryDelay.Should().NotBeNull("QueryDelay");
			firstDatafeed.QueryDelay.Should().BeGreaterThan(new Time("1nanos"));

			firstDatafeed.Indices.Should().NotBeNull("Indices");
			firstDatafeed.Indices.Should().Be(Nest.Indices.Parse("server-metrics"));

			firstDatafeed.Types.Should().NotBeNull("Types");
			firstDatafeed.Types.Should().Be(Types.Parse("metric"));

			firstDatafeed.ScrollSize.Should().Be(1000);

			firstDatafeed.ChunkingConfig.Should().NotBeNull();
			firstDatafeed.ChunkingConfig.Mode.Should().Be(ChunkingMode.Auto);

			firstDatafeed.Query.Should().NotBeNull();

			response.ShouldBeValid();
		}
	}
}
