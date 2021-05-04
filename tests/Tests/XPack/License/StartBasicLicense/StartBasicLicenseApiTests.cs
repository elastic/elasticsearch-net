// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Transport.HttpMethod;

namespace Tests.XPack.License.StartBasicLicense
{
	[SkipVersion("<6.5.0", "")]
	public class StartBasicLicenseInvalidApiTests
		: ApiIntegrationTestBase<XPackCluster, StartBasicLicenseResponse, IStartBasicLicenseRequest, StartBasicLicenseDescriptor,
			StartBasicLicenseRequest>
	{
		public StartBasicLicenseInvalidApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => POST;

		protected override StartBasicLicenseRequest Initializer => new StartBasicLicenseRequest();

		protected override string UrlPath => $"/_license/start_basic";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.License.StartBasic(f),
			(client, f) => client.License.StartBasicAsync(f),
			(client, r) => client.License.StartBasic(r),
			(client, r) => client.License.StartBasicAsync(r)
		);

		protected override void ExpectResponse(StartBasicLicenseResponse response)
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
