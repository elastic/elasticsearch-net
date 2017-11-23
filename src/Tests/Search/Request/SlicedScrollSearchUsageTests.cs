using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Search.Request
{
	public class SlicedScrollSearchUsageTests : SearchUsageTestBase
	{

		public SlicedScrollSearchUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string UrlPath => "/project/doc/_search?scroll=1m";

		protected override object ExpectJson =>
			new { slice = new { id = 0, max = 5 } };

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>()
			{
				Scroll = "1m",
				Slice = new SlicedScroll {  Id = 0, Max = 5 }
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Scroll("1m")
			.Slice(ss=>ss.Id(0).Max(5));
	}
}
