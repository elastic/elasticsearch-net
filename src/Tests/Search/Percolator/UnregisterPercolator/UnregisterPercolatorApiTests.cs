using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Search.Percolator.UnregisterPercolator
{
	[Collection(IntegrationContext.Indexing)]
	public class UnregisterPercolatorApiTests : ApiIntegrationTestBase<IUnregisterPercolatorResponse, IUnregisterPercolatorRequest, UnregisterPercolatorDescriptor<Project>, UnregisterPercolatorRequest>
	{
		public UnregisterPercolatorApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		
		}

		protected override void OnBeforeCall(IElasticClient client)
		{
			var createIndex = this.Client.CreateIndex(this.CallIsolatedValue + "-index");
			if (!createIndex.IsValid)
				throw new Exception($"Setup: failed to first register percolator {this.CallIsolatedValue}");
			var register = this.Client.RegisterPercolator<Project>(this.CallIsolatedValue, r => r.Query(q => q.MatchAll()).Index(this.CallIsolatedValue + "-index"));
			if (!register.IsValid)
				throw new Exception($"Setup: failed to first register percolator {this.CallIsolatedValue}");
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.UnregisterPercolator<Project>(this.CallIsolatedValue, f),
			fluentAsync: (c, f) => c.UnregisterPercolatorAsync<Project>(this.CallIsolatedValue, f),
			request: (c, r) => c.UnregisterPercolator(r),
			requestAsync: (c, r) => c.UnregisterPercolatorAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/{this.CallIsolatedValue}-index/.percolator/{this.CallIsolatedValue}";

		protected override void ExpectResponse(IUnregisterPercolatorResponse response)
		{
			response.Found.Should().BeTrue();
			response.Index.Should().NotBeNullOrEmpty();
			response.Type.Should().NotBeNullOrEmpty();
			response.Version.Should().BeGreaterThan(0);
			response.Id.Should().NotBeNullOrEmpty();
		}

		protected override UnregisterPercolatorDescriptor<Project> NewDescriptor() => new UnregisterPercolatorDescriptor<Project>(this.CallIsolatedValue);

		protected override Func<UnregisterPercolatorDescriptor<Project>, IUnregisterPercolatorRequest> Fluent => d=> d.Index(this.CallIsolatedValue + "-index");

		protected override UnregisterPercolatorRequest Initializer => new UnregisterPercolatorRequest(this.CallIsolatedValue + "-index", this.CallIsolatedValue);
	}
}
