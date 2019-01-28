using System;
using System.Threading.Tasks;
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
	public class TrialLicenseCluster : ClientTestClusterBase { }

	[SkipVersion("<6.4.0", "Only exists in Elasticsearch 6.1.0+, expect x-pack to ship in default distribution")]
	public class StartTrialLicenseApiTests
		: ApiIntegrationTestBase<TrialLicenseCluster, IStartTrialLicenseResponse, IStartTrialLicenseRequest, StartTrialLicenseDescriptor,
			StartTrialLicenseRequest>
	{
		public StartTrialLicenseApiTests(TrialLicenseCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected bool BootstrappedWithLicense => !string.IsNullOrEmpty(Cluster.ClusterConfiguration.XPackLicenseJson);

		protected override bool ExpectIsValid => true;
		[I] public override async Task ReturnsExpectedIsValid() =>
			await AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(r.TrialWasStarted));

		protected override int ExpectStatusCode => 200;
		[I] public override async Task ReturnsExpectedStatusCode() =>
			await AssertOnAllResponses(r => r.ApiCall.HttpStatusCode.Should().Be(r.TrialWasStarted ? 200 : 403));

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_xpack/license/start_trial?acknowledge=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.StartTrialLicense(f),
			(client, f) => client.StartTrialLicenseAsync(f),
			(client, r) => client.StartTrialLicense(r),
			(client, r) => client.StartTrialLicenseAsync(r)
		);

		protected override StartTrialLicenseRequest Initializer => new StartTrialLicenseRequest { Acknowledge = true };
		protected override Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> Fluent => s => s.Acknowledge();

		protected override void ExpectResponse(IStartTrialLicenseResponse response)
		{
			response.Acknowledged.Should().BeTrue();
			if (!response.TrialWasStarted)
				response.ErrorMessage.Should().NotBeNullOrWhiteSpace().And.Contain("Trial was already activated");
		}
	}
}
