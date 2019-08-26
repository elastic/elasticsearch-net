using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.PutFilter
{
	[SkipVersion("<6.4.0", "Filter functions for machine learning stabilised in 6.4.0")]
	public class PutFilterApiTests : MachineLearningIntegrationTestBase<IPutFilterResponse, IPutFilterRequest, PutFilterDescriptor, PutFilterRequest>
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
		protected override string UrlPath => $"_xpack/ml/filters/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.PutFilter(CallIsolatedValue, f),
			(client, f) => client.PutFilterAsync(CallIsolatedValue, f),
			(client, r) => client.PutFilter(r),
			(client, r) => client.PutFilterAsync(r)
		);

		protected override PutFilterDescriptor NewDescriptor() => new PutFilterDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IPutFilterResponse response)
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
