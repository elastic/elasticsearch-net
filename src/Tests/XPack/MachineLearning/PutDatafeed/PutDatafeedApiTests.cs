using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.PutDatafeed
{
	public class PutDatafeedApiTests : MachineLearningIntegrationTestBase<IPutDatafeedResponse,
		IPutDatafeedRequest, PutDatafeedDescriptor<Metric>, PutDatafeedRequest>
	{
		public PutDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutDatafeed(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutDatafeedAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutDatafeed(r),
			requestAsync: (client, r) => client.PutDatafeedAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
			}
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}";
		protected override bool SupportsDeserialization => false;
		protected override PutDatafeedDescriptor<Metric> NewDescriptor() => new PutDatafeedDescriptor<Metric>(CallIsolatedValue);

		protected override object ExpectJson => new
		{
			indices = new[] { "server-metrics" },
			job_id = CallIsolatedValue,
			query = new
			{
				match_all = new {}
			},
			types = new [] { "metric" }
		};

		protected override Func<PutDatafeedDescriptor<Metric>, IPutDatafeedRequest> Fluent => f => f
			.JobId(CallIsolatedValue)
			.Query(q => q.MatchAll());

		protected override PutDatafeedRequest Initializer =>
			new PutDatafeedRequest(CallIsolatedValue)
			{
				JobId = CallIsolatedValue,
				Indices = "server-metrics",
				Types = "metric",
				Query = new MatchAllQuery()
			};

		protected override void ExpectResponse(IPutDatafeedResponse response)
		{
			response.ShouldBeValid();

			response.DatafeedId.Should().NotBeNullOrWhiteSpace();
			response.JobId.Should().Be(CallIsolatedValue);

			response.QueryDelay.Should().NotBeNull("QueryDelay");
			response.QueryDelay.Should().BeGreaterThan(new Time("1nanos"));

			response.Indices.Should().NotBeNull("Indices");
			response.Indices.Should().Be(Nest.Indices.Parse("server-metrics"));

			response.Types.Should().NotBeNull("Types");
			response.Types.Should().Be(Types.Parse("metric"));

			response.ScrollSize.Should().Be(1000);

			response.ChunkingConfig.Should().NotBeNull();
			response.ChunkingConfig.Mode.Should().Be(ChunkingMode.Auto);

			response.Query.Should().NotBeNull();
		}
	}
}
