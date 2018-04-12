using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.ForecastJob
{
	[SkipVersion("<6.1.0", "Only exists in Elasticsearch 6.1.0+")]
	public class ForecastJobApiTests : MachineLearningIntegrationTestBase<IForecastJobResponse, IForecastJobRequest, ForecastJobDescriptor, ForecastJobRequest>
	{
		public ForecastJobApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexAnomalyRecord(client, callUniqueValue.Value, new DateTimeOffset(2016, 6, 2, 00, 00, 00, TimeSpan.Zero));
				OpenJob(client, callUniqueValue.Value);
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				CloseJob(client, callUniqueValue.Value);
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ForecastJob(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.ForecastJobAsync(CallIsolatedValue, f),
			request: (client, r) => client.ForecastJob(r),
			requestAsync: (client, r) => client.ForecastJobAsync(r)
		);

		protected override ForecastJobDescriptor NewDescriptor() => new ForecastJobDescriptor(CallIsolatedValue);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_forecast";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<ForecastJobDescriptor, IForecastJobRequest> Fluent => f => f;
		protected override ForecastJobRequest Initializer => new ForecastJobRequest(CallIsolatedValue);

		protected override void ExpectResponse(IForecastJobResponse response)
		{
			response.ShouldBeValid();
			response.ForecastId.Should().NotBeNullOrEmpty();
		}
	}
}
