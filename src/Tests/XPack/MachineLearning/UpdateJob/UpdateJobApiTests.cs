using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.UpdateJob
{
	public class UpdateJobApiTests : MachineLearningIntegrationTestBase<IUpdateJobResponse, IUpdateJobRequest, UpdateJobDescriptor<Metric>, UpdateJobRequest>
	{
		public UpdateJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.UpdateJob(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.UpdateJobAsync(CallIsolatedValue, f),
			request: (client, r) => client.UpdateJob(r),
			requestAsync: (client, r) => client.UpdateJobAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				PutJob(client, callUniqueValue.Value);
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				DeleteJob(client, callUniqueValue.Value);
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_update";
		protected override bool SupportsDeserialization => false;
		protected override UpdateJobDescriptor<Metric> NewDescriptor() => new UpdateJobDescriptor<Metric>(CallIsolatedValue);

		protected override object ExpectJson => new
			{
				description = "Lab 1 - Simple example modified",
				background_persist_interval = "6h"
			};

		protected override Func<UpdateJobDescriptor<Metric>, IUpdateJobRequest> Fluent => f => f
			.Description("Lab 1 - Simple example modified")
			.BackgroundPersistInterval("6h");

		protected override UpdateJobRequest Initializer =>
			new UpdateJobRequest(CallIsolatedValue)
			{
				Description = "Lab 1 - Simple example modified",
				BackgroundPersistInterval = "6h"
			};

		protected override void ExpectResponse(IUpdateJobResponse response)
		{
			response.ShouldBeValid();
		}
	}
}
