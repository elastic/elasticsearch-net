// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Ssl.GetCertificates
{
	[SkipVersion(">=8.0.0-SNAPSHOT", "TODO investigate")]
	public class GetCertificatesApiTests
		: ApiIntegrationTestBase<XPackCluster, GetCertificatesResponse, IGetCertificatesRequest, GetCertificatesDescriptor, GetCertificatesRequest>
	{
		public GetCertificatesApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetCertificatesRequest Initializer => new GetCertificatesRequest();

		protected override string UrlPath => $"/_ssl/certificates";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Security.GetCertificates(f),
			(client, f) => client.Security.GetCertificatesAsync(f),
			(client, r) => client.Security.GetCertificates(r),
			(client, r) => client.Security.GetCertificatesAsync(r)
		);

		protected override void ExpectResponse(GetCertificatesResponse response)
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
