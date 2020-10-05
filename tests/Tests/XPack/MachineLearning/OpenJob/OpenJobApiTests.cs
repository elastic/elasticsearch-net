// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.OpenJob
{
	public class OpenJobApiTests : MachineLearningIntegrationTestBase<OpenJobResponse, IOpenJobRequest, OpenJobDescriptor, OpenJobRequest>
	{
		public OpenJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<OpenJobDescriptor, IOpenJobRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override OpenJobRequest Initializer => new OpenJobRequest(CallIsolatedValue);
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}/_open";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) PutJob(client, callUniqueValue.Value);
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
			(client, f) => client.MachineLearning.OpenJob(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.OpenJobAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.OpenJob(r),
			(client, r) => client.MachineLearning.OpenJobAsync(r)
		);

		protected override OpenJobDescriptor NewDescriptor() => new OpenJobDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(OpenJobResponse response)
		{
			response.Opened.Should().BeTrue();

			if (TestClient.Configuration.InRange(">=7.8.0"))
				response.Node.Should().NotBeNullOrEmpty();
		}
	}
}
