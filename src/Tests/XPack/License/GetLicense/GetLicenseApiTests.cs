using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.License.GetLicense
{
	[Collection(IntegrationContext.ReadOnly)]
	[SkipVersion("<2.3.0", "")]
	public class GetLicenseApiTests : ApiIntegrationTestBase<IGetLicenseResponse, IGetLicenseRequest, GetLicenseDescriptor, GetLicenseRequest>
	{
		public GetLicenseApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetLicense(f),
			fluentAsync: (client, f) => client.GetLicenseAsync(f),
			request: (client, r) => client.GetLicense(r),
			requestAsync: (client, r) => client.GetLicenseAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => GET;

		protected override string UrlPath => $"/_license";

		protected override bool SupportsDeserialization => true;

		protected override GetLicenseRequest Initializer => new GetLicenseRequest();

		protected override void ExpectResponse(IGetLicenseResponse response)
		{
			var l = response.License;
			l.Should().NotBeNull();
			l.ExpiryDate.Should().BeAfter(DateTime.UtcNow.AddYears(-2));
			l.IssueDate.Should().BeAfter(DateTime.UtcNow.AddYears(-2));
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
