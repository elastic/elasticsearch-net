// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.DeleteJob
{
	public class DeleteJobApiTests : MachineLearningIntegrationTestBase<DeleteJobResponse, IDeleteJobRequest, DeleteJobDescriptor, DeleteJobRequest>
	{
		public DeleteJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<DeleteJobDescriptor, IDeleteJobRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteJobRequest Initializer => new DeleteJobRequest(CallIsolatedValue);
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) PutJob(client, callUniqueValue.Value);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.DeleteJob(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.DeleteJobAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.DeleteJob(r),
			(client, r) => client.MachineLearning.DeleteJobAsync(r)
		);

		protected override DeleteJobDescriptor NewDescriptor() => new DeleteJobDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(DeleteJobResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
