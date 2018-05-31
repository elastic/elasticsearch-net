using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.License.StartTrialLicense
{
	[SkipVersion("<6.1.0", "Only exists in Elasticsearch 6.1.0+")]
	public class StartTrialLicenseApiTests : ApiIntegrationTestBase<XPackCluster, IStartTrialLicenseResponse, IStartTrialLicenseRequest, StartTrialLicenseDescriptor, StartTrialLicenseRequest>
	{
		public StartTrialLicenseApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.StartTrialLicense(f),
			fluentAsync: (client, f) => client.StartTrialLicenseAsync(f),
			request: (client, r) => client.StartTrialLicense(r),
			requestAsync: (client, r) => client.StartTrialLicenseAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 403;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_xpack/license/start_trial";
		protected override bool SupportsDeserialization => false;

		protected override void ExpectResponse(IStartTrialLicenseResponse response)
		{
			response.ShouldNotBeValid();
			// license already applied
			response.TrialWasStarted.Should().BeFalse();
			response.ErrorMessage.Should().Be("Operation failed: Trial was already activated.");
		}
	}
}
