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

namespace Tests.XPack.MachineLearning.PutFilter
{
	[SkipVersion("<6.4.0", "Filter functions for machine learning stabilised in 6.4.0")]
	public class PutFilterApiTests : MachineLearningIntegrationTestBase<PutFilterResponse, IPutFilterRequest, PutFilterDescriptor, PutFilterRequest>
	{
		public PutFilterApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				DeleteFilter(client, callUniqueValue.Value);
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			description = "A list of safe domains",
			items = new[] { "*.google.com", "wikipedia.org" }
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<PutFilterDescriptor, IPutFilterRequest> Fluent => f => f
			.Description("A list of safe domains")
			.Items("*.google.com", "wikipedia.org");

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutFilterRequest Initializer =>
			new PutFilterRequest(CallIsolatedValue)
			{
				Description = "A list of safe domains",
				Items = new [] { "*.google.com", "wikipedia.org" }
			};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/filters/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.PutFilter(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.PutFilterAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.PutFilter(r),
			(client, r) => client.MachineLearning.PutFilterAsync(r)
		);

		protected override PutFilterDescriptor NewDescriptor() => new PutFilterDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(PutFilterResponse response)
		{
			response.ShouldBeValid();
			response.FilterId.Should().Be(CallIsolatedValue);
			response.Items.Should().NotBeNull()
				.And.HaveCount(2)
				.And.Contain("*.google.com")
				.And.Contain("wikipedia.org");

			response.Description.Should().Be("A list of safe domains");
		}
	}
}
