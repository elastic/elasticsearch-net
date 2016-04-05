using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Search.Request
{
	public class FromAndSizeUsageTests : SearchUsageTestBase
	{
		/**
		 * Pagination of results can be done by using the `from` and `size` parameters.
		 *
		 * `from` parameter:: defines the offset from the first result you want to fetch.
		 * `size` parameter:: allows you to configure the maximum amount of hits to be returned.
		 */

		public FromAndSizeUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new { from = 10, size = 12 };

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>()
			{
				From = 10,
				Size = 12
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.From(10)
				.Size(12);
	}
}
