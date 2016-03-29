using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Search.Request
{
	public class MinScoreUsageTests : SearchUsageTestBase
	{
		public MinScoreUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			min_score = 0.5,
			query = new
			{
				term = new
				{
					name = new
					{
						value = "elasticsearch"
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.MinScore(0.5)
			.Query(q => q
				.Term(p => p.Name, "elasticsearch")
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				MinScore = 0.5,
				Query = new TermQuery
				{
					Field = "name",
					Value = "elasticsearch"
				}
			};
	}
}
