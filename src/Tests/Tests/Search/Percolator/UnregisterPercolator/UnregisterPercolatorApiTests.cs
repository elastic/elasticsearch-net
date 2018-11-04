using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

#pragma warning disable 618 // testing deprecated percolate APIs

namespace Tests.Search.Percolator.UnregisterPercolator
{
	[SkipVersion(">5.0.0-alpha1", "deprecated")]
	public class UnregisterPercolatorApiTests
		: ApiIntegrationTestBase<WritableCluster, IUnregisterPercolatorResponse, IUnregisterPercolatorRequest, UnregisterPercolatorDescriptor<Project>
			, UnregisterPercolatorRequest>
	{
		public UnregisterPercolatorApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override int ExpectStatusCode => 200;

		protected override Func<UnregisterPercolatorDescriptor<Project>, IUnregisterPercolatorRequest> Fluent =>
			d => d.Index(CallIsolatedValue + "-index");

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override UnregisterPercolatorRequest Initializer =>
			new UnregisterPercolatorRequest(CallIsolatedValue + "-index", CallIsolatedValue);

		protected override string UrlPath => $"/{CallIsolatedValue}-index/.percolator/{CallIsolatedValue}";

		protected override void OnBeforeCall(IElasticClient client)
		{
			var createIndex = Client.CreateIndex(CallIsolatedValue + "-index");
			if (!createIndex.IsValid)
				throw new Exception($"Setup: failed to first register percolator {CallIsolatedValue}");

			var register = Client.RegisterPercolator<Project>(CallIsolatedValue, r => r.Query(q => q.MatchAll()).Index(CallIsolatedValue + "-index"));
			if (!register.IsValid)
				throw new Exception($"Setup: failed to first register percolator {CallIsolatedValue}");
		}

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.UnregisterPercolator<Project>(CallIsolatedValue, f),
			(c, f) => c.UnregisterPercolatorAsync<Project>(CallIsolatedValue, f),
			(c, r) => c.UnregisterPercolator(r),
			(c, r) => c.UnregisterPercolatorAsync(r)
		);

		protected override void ExpectResponse(IUnregisterPercolatorResponse response)
		{
			response.Found.Should().BeTrue();
			response.Index.Should().NotBeNullOrEmpty();
			response.Type.Should().NotBeNullOrEmpty();
			response.Version.Should().BeGreaterThan(0);
			response.Id.Should().NotBeNullOrEmpty();
		}

		protected override UnregisterPercolatorDescriptor<Project> NewDescriptor() => new UnregisterPercolatorDescriptor<Project>(CallIsolatedValue);
	}
}
