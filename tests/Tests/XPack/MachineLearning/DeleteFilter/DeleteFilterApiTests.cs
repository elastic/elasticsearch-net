// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.DeleteFilter
{
	[SkipVersion("<6.4.0", "Filter functions for machine learning stabilised in 6.4.0")]
	public class DeleteFilterApiTests : MachineLearningIntegrationTestBase<DeleteFilterResponse, IDeleteFilterRequest, DeleteFilterDescriptor, DeleteFilterRequest>
	{
		public DeleteFilterApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				PutFilter(client, callUniqueValue.Value);
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteFilterDescriptor, IDeleteFilterRequest> Fluent => f => f;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteFilterRequest Initializer => new DeleteFilterRequest(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/filters/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.DeleteFilter(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.DeleteFilterAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.DeleteFilter(r),
			(client, r) => client.MachineLearning.DeleteFilterAsync(r)
		);

		protected override DeleteFilterDescriptor NewDescriptor() => new DeleteFilterDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(DeleteFilterResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
		}
	}
}
