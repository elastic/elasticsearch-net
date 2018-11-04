using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;

#pragma warning disable 618 // Deprecated in 5.0.0-alpha2 - see https://github.com/elastic/elasticsearch/issues/12017

namespace Tests.QueryDsl.Compound.Indices
{
	public class IndicesNoMatchQueryUsageTests : QueryDslUsageTestsBase
	{
		public IndicesNoMatchQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IIndicesQuery>(p => p.Indices)
		{
			q => q.Indices = null,
			q => q.Query = null,
			q => q.Query = ConditionlessQuery
		};

		protected override NotConditionlessWhen NotConditionlessWhen => new NotConditionlessWhen<IIndicesQuery>(p => p.Indices)
		{
			q =>
			{
				q.Query = VerbatimQuery;
				q.NoMatchQuery = null;
			},
			q =>
			{
				q.Query = null;
				q.NoMatchQuery = VerbatimQuery;
			}
		};

		protected override QueryContainer QueryInitializer => new IndicesQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			Indices = Nest.Indices.All,
			Query = new MatchAllQuery(),
			NoMatchQuery = new NoMatchQueryContainer { Shortcut = NoMatchShortcut.All }
		};

		protected override object QueryJson => new
		{
			indices = new
			{
				_name = "named_query",
				boost = 1.1,
				indices = new[] { "_all" },
				no_match_query = "all",
				query = new
				{
					match_all = new { }
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Indices(c => c
				.Name("named_query")
				.Boost(1.1)
				.Indices(Nest.Indices.All)
				.Query(qq => qq.MatchAll())
				.NoMatchQuery(NoMatchShortcut.All)
			);
	}
}
