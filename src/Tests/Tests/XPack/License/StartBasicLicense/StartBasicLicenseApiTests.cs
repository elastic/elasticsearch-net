using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;
using Tests.Framework;
using Tests.Framework.Integration;
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.License.StartBasicLicense
{
	public class BasicLicenseCluster : ClientTestClusterBase { }

	// TODO: cluster starts with a basic license now, investigate further
	[SkipNonStructuralChange]
	[SkipVersion("<6.5.0", "")]
	public class StartBasicLicenseApiTests
		: ApiIntegrationTestBase<BasicLicenseCluster, IStartBasicLicenseResponse, IStartBasicLicenseRequest, StartBasicLicenseDescriptor, StartBasicLicenseRequest>
	{
		public StartBasicLicenseApiTests(BasicLicenseCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => POST;

		protected override StartBasicLicenseRequest Initializer => new StartBasicLicenseRequest();

		protected override string UrlPath => $"/_license/start_basic";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.StartBasicLicense(f),
			(client, f) => client.StartBasicLicenseAsync(f),
			(client, r) => client.StartBasicLicense(r),
			(client, r) => client.StartBasicLicenseAsync(r)
		);

		protected override void ExpectResponse(IStartBasicLicenseResponse response)
		{
			response.BasicWasStarted.Should().BeFalse();
			response.Acknowledged.Should().BeFalse();
			response.ErrorMessage.Should().NotBeNullOrWhiteSpace();
			var d = response.Acknowledge;
			d.Should().NotBeNull();
			d.Message.Should().NotBeNullOrWhiteSpace();
			d.Should()
				.NotBeEmpty()
				.And.ContainKey("ml")
				.And.ContainKey("security");
		}
	}
}
