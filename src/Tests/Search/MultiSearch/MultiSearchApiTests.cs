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

namespace Tests.Search.MultiSearch
{
	[Collection(IntegrationContext.ReadOnly)]
	public class MultiSearchApiTests
		: ApiIntegrationTestBase<IMultiSearchResponse, IMultiSearchRequest, MultiSearchDescriptor, MultiSearchRequest>
	{
		public MultiSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.MultiSearch(f),
			fluentAsync: (c, f) => c.MultiSearchAsync(f),
			request: (c, r) => c.MultiSearch(r),
			requestAsync: (c, r) => c.MultiSearchAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_msearch";

		protected override bool SupportsDeserialization => false;
	
		protected override object ExpectJson => new object[]
		{
			new { index = "project" },
			new { query = new { match_all = new { } }, from = "0", size = "10" },
			new { index = "otherindex" },
			new { query = new { match = new { name = new { query = "nest" } } } },
			new { index = "otherindex", type = "othertype", search_type = "count" },
			new { query = new { match_all = new { } } }
		};

		protected override Func<MultiSearchDescriptor, IMultiSearchRequest> Fluent => ms => ms
			.Search<Project>(s => s.Query(q => q.MatchAll()))
			.Search<Project>(s => s.Index("otherindex").Query(q => q.Match(m => m.OnField(p => p.Name).Query("nest"))))
			.Search<Project>(s => s.Index("otherindex").Type("othertype").SearchType(SearchType.Count).MatchAll());

		protected override MultiSearchRequest Initializer => new MultiSearchRequest(typeof(Project), typeof(Project))
		{
		};
	}
}
