using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.License.GetLicense
{
	[SkipVersion("<2.3.0", "")]
	public class GetLicenseApiTests
		: ApiIntegrationTestBase<XPackCluster, IGetLicenseResponse, IGetLicenseRequest, GetLicenseDescriptor, GetLicenseRequest>
	{
		public GetLicenseApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => GET;

		protected override GetLicenseRequest Initializer => new GetLicenseRequest();

		protected override string UrlPath => $"/_license";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetLicense(f),
			(client, f) => client.GetLicenseAsync(f),
			(client, r) => client.GetLicense(r),
			(client, r) => client.GetLicenseAsync(r)
		);

		protected override void ExpectResponse(IGetLicenseResponse response)
		{
			var l = response.License;
			l.Should().NotBeNull();
			l.ExpiryDate.Should().BeAfter(DateTime.UtcNow.AddYears(-2));
			l.IssueDate.Should().BeAfter(DateTime.UtcNow.AddYears(-30));
			l.IssueDateInMilliseconds.Should().BeGreaterThan(0);
			l.ExpiryDateInMilliseconds.Should().BeGreaterThan(0);
			l.IssuedTo.Should().NotBeNullOrWhiteSpace();
			l.Issuer.Should().NotBeNullOrWhiteSpace();
			l.MaxNodes.Should().BeGreaterThan(0);
			l.Status.Should().Be(LicenseStatus.Active);
			l.UID.Should().NotBeNullOrWhiteSpace();
		}
	}
}
