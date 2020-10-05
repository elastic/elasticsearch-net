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
using static Elastic.Transport.HttpMethod;

namespace Tests.XPack.License.GetLicense
{
	[SkipVersion("<2.3.0", "")]
	public class GetLicenseApiTests
		: ApiIntegrationTestBase<XPackCluster, GetLicenseResponse, IGetLicenseRequest, GetLicenseDescriptor, GetLicenseRequest>
	{
		public GetLicenseApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => GET;

		protected override GetLicenseRequest Initializer => new GetLicenseRequest();

		protected override string UrlPath => $"/_license";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.License.Get(f),
			(client, f) => client.License.GetAsync(f),
			(client, r) => client.License.Get(r),
			(client, r) => client.License.GetAsync(r)
		);

		protected override void ExpectResponse(GetLicenseResponse response)
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
