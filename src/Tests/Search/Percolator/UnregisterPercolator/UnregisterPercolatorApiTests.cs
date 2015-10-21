using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using Tests.Framework.MockData;

namespace Tests.Search.Percolator.UnregisterPercolator
{
	[Collection(IntegrationContext.ReadOnly)]
	public class UnregisterPercolatorApiTests
		: ApiIntegrationTestBase<IUnregisterPercolateResponse, IUnregisterPercolatorRequest, UnregisterPercolatorDescriptor<Project>, UnregisterPercolatorRequest>
	{
		public UnregisterPercolatorApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.UnregisterPercolator<Project>(_name),
			fluentAsync: (c, f) => c.UnregisterPercolatorAsync<Project>(_name),
			request: (c, r) => c.UnregisterPercolator(r),
			requestAsync: (c, r) => c.UnregisterPercolatorAsync(r)
		);

		private string _name = "name-of-perc";

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/project/.percolator/{_name}";

		protected override Func<UnregisterPercolatorDescriptor<Project>, IUnregisterPercolatorRequest> Fluent => null;

		protected override UnregisterPercolatorRequest Initializer => new UnregisterPercolatorRequest(typeof(Project), _name);
	}
}
