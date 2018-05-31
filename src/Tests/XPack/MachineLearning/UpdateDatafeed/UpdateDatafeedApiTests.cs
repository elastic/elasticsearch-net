using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.UpdateDatafeed
{
	public class UpdateDatafeedApiTests : MachineLearningIntegrationTestBase<IUpdateDatafeedResponse,
		IUpdateDatafeedRequest, UpdateDatafeedDescriptor<Metric>, UpdateDatafeedRequest>
	{
		public UpdateDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.UpdateDatafeed(CallIsolatedValue + "-datafeed", f),
			fluentAsync: (client, f) => client.UpdateDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			request: (client, r) => client.UpdateDatafeed(r),
			requestAsync: (client, r) => client.UpdateDatafeedAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}-datafeed/_update";
		protected override bool SupportsDeserialization => false;
		protected override UpdateDatafeedDescriptor<Metric> NewDescriptor() => new UpdateDatafeedDescriptor<Metric>(CallIsolatedValue);

		protected override object ExpectJson => new
		{
			indices = new [] { "server-metrics" },
			job_id = CallIsolatedValue,
			query = new
			{
				match_all = new
				{
					boost = 2.0
				}
			},
			types = new [] { "metric" }
		};

		protected override Func<UpdateDatafeedDescriptor<Metric>, IUpdateDatafeedRequest> Fluent => f => f
			.JobId(CallIsolatedValue)
			.Query(q => q
				.MatchAll(m => m.Boost(2))
			)
			;

		protected override UpdateDatafeedRequest Initializer =>
			new UpdateDatafeedRequest(CallIsolatedValue + "-datafeed")
			{
				JobId = CallIsolatedValue,
				Indices = "server-metrics",
				Types = "metric",
				Query = new MatchAllQuery
				{
					Boost = 2
				}
			};

		protected override void ExpectResponse(IUpdateDatafeedResponse response)
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
