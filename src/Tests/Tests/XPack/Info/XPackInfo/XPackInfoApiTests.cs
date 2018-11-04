using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Info.XPackInfo
{
	[SkipVersion("<5.4.0", "")]
	public class XPackInfoApiTests
		: ApiIntegrationTestBase<XPackCluster, IXPackInfoResponse, IXPackInfoRequest, XPackInfoDescriptor, XPackInfoRequest>
	{
		public XPackInfoApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override XPackInfoRequest Initializer => new XPackInfoRequest();

		protected override bool SupportsDeserialization => true;

		protected override string UrlPath => $"/_xpack";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.XPackInfo(f),
			(client, f) => client.XPackInfoAsync(f),
			(client, r) => client.XPackInfo(r),
			(client, r) => client.XPackInfoAsync(r)
		);

		protected override void ExpectResponse(IXPackInfoResponse response)
		{
			response.Tagline.Should().NotBeNullOrEmpty();
			response.Build.Should().NotBeNull();
			response.Build.Date.Should().BeAfter(new DateTime());
			response.Build.Hash.Should().NotBeNullOrWhiteSpace();

			response.Features.Should().NotBeNull();
			response.Features.Graph.Should().NotBeNull();
			response.Features.Graph.Available.Should().BeTrue();
			response.Features.Graph.Description.Should().NotBeNullOrEmpty();
			response.Features.MachineLearning.Should().NotBeNull();
			response.Features.MachineLearning.NativeCodeInformation.Should().NotBeNull();
			response.Features.MachineLearning.NativeCodeInformation.Version.Should().NotBeNullOrWhiteSpace();
			response.Features.MachineLearning.NativeCodeInformation.BuildHash.Should().NotBeNullOrWhiteSpace();

			response.License.Should().NotBeNull();
			response.License.ExpiryDateInMilliseconds.Should().BeGreaterThan(0);
			response.License.UID.Should().NotBeNullOrEmpty();
		}
	}
}
