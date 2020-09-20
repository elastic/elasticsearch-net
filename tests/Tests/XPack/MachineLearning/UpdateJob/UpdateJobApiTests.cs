// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.Extensions;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.UpdateJob
{
	public class UpdateJobApiTests
		: MachineLearningIntegrationTestBase<UpdateJobResponse, IUpdateJobRequest, UpdateJobDescriptor<Metric>, UpdateJobRequest>
	{
		public UpdateJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			description = "Lab 1 - Simple example modified",
			background_persist_interval = "6h"
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<UpdateJobDescriptor<Metric>, IUpdateJobRequest> Fluent => f => f
			.Description("Lab 1 - Simple example modified")
			.BackgroundPersistInterval("6h");

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override UpdateJobRequest Initializer =>
			new UpdateJobRequest(CallIsolatedValue)
			{
				Description = "Lab 1 - Simple example modified",
				BackgroundPersistInterval = "6h"
			};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}/_update";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.UpdateJob(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.UpdateJobAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.UpdateJob(r),
			(client, r) => client.MachineLearning.UpdateJobAsync(r)
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

		protected override UpdateJobDescriptor<Metric> NewDescriptor() => new UpdateJobDescriptor<Metric>(CallIsolatedValue);

		protected override void ExpectResponse(UpdateJobResponse response) => response.ShouldBeValid();
	}
}
