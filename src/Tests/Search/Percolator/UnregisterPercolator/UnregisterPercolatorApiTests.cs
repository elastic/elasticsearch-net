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
	[Collection(IntegrationContext.ReadOnly)]
	public class UnregisterPercolatorApiTests
		: ApiIntegrationTestBase<IUnregisterPercolateResponse, IUnregisterPercolatorRequest, UnregisterPercolatorDescriptor<Project>, UnregisterPercolatorRequest>
	{
		public UnregisterPercolatorApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		
		}

		protected override void OnBeforeCall(IElasticClient client)
		{
			var register = this.Client.RegisterPercolator<Project>(this.CallIsolatedValue, r => r.Query(q => q.MatchAll()));
			if (!register.IsValid)
				throw new Exception($"Setup: failed to first register percolator {this.CallIsolatedValue}");
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.UnregisterPercolator<Project>(this.CallIsolatedValue),
			fluentAsync: (c, f) => c.UnregisterPercolatorAsync<Project>(this.CallIsolatedValue),
			request: (c, r) => c.UnregisterPercolator(r),
			requestAsync: (c, r) => c.UnregisterPercolatorAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/project/.percolator/{this.CallIsolatedValue}";

		protected override void ExpectResponse(IUnregisterPercolateResponse response)
		{
			response.Found.Should().BeTrue();
			response.Index.Should().NotBeNullOrEmpty();
			response.Type.Should().NotBeNullOrEmpty();
			response.Version.Should().BeGreaterThan(0);
			response.Id.Should().NotBeNullOrEmpty();
		}

		protected override Func<UnregisterPercolatorDescriptor<Project>, IUnregisterPercolatorRequest> Fluent => null;

		protected override UnregisterPercolatorRequest Initializer => new UnregisterPercolatorRequest(typeof(Project), this.CallIsolatedValue);
	}
}
