using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Ssl.GetCertificates
{
	[SkipVersion("<6.5.0", "")]
	public class GetCertificatesApiTests
		: ApiIntegrationTestBase<XPackCluster, IGetCertificatesResponse, IGetCertificatesRequest, GetCertificatesDescriptor, GetCertificatesRequest>
	{
		public GetCertificatesApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetCertificatesRequest Initializer => new GetCertificatesRequest();

		protected override string UrlPath => $"/_ssl/certificates";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetCertificates(f),
			(client, f) => client.GetCertificatesAsync(f),
			(client, r) => client.GetCertificates(r),
			(client, r) => client.GetCertificatesAsync(r)
		);

		protected override void ExpectResponse(IGetCertificatesResponse response)
		{
			response.Certificates.Should().NotBeEmpty();
			foreach (var c in response.Certificates)
			{
				c.Path.Should().NotBeNullOrWhiteSpace();
				c.Format.Should().NotBeNullOrWhiteSpace();
				c.SubjectDomainName.Should().NotBeNullOrWhiteSpace();
				c.SerialNumber.Should().NotBeNullOrWhiteSpace();
				c.Expiry.Should().BeAfter(DateTime.UtcNow.AddYears(-2));
			}
		}
	}
}
