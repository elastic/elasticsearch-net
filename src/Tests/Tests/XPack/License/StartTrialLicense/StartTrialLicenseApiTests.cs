using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.License.StartTrialLicense
{
	[SkipVersion("<6.1.0", "Only exists in Elasticsearch 6.1.0+")]
	public class StartTrialLicenseApiTests
		: ApiIntegrationTestBase<XPackCluster, IStartTrialLicenseResponse, IStartTrialLicenseRequest, StartTrialLicenseDescriptor,
			StartTrialLicenseRequest>
	{
		public StartTrialLicenseApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected bool BootstrappedWithLicense => !string.IsNullOrEmpty(Cluster.ClusterConfiguration.XPackLicenseJson);

		protected override bool ExpectIsValid => BootstrappedWithLicense;
		protected override int ExpectStatusCode => BootstrappedWithLicense ? 200 : 403;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_xpack/license/start_trial";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.StartTrialLicense(f),
			(client, f) => client.StartTrialLicenseAsync(f),
			(client, r) => client.StartTrialLicense(r),
			(client, r) => client.StartTrialLicenseAsync(r)
		);

		protected override void ExpectResponse(IStartTrialLicenseResponse response)
		{
			response.ShouldNotBeValid();
            response.TrialWasStarted.Should().BeFalse();
			if (!BootstrappedWithLicense)
			{
                // license already applied
                response.ErrorMessage.Should().Be("Operation failed: Trial was already activated.");
			}
			else
			{
				// running with a license means you have to pass the acknowledge flag to forcefully go
				// into trial mode
				response.ErrorMessage.Should().Contain(" Needs acknowledgement");
			}
		}
	}
}
