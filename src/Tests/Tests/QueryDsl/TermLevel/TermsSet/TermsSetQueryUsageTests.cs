using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.QueryDsl.TermLevel.TermsSet
{
	/**
	* Returns any documents that match with at least one or more of the provided terms. The terms are not
	* analyzed and thus must match exactly. The number of terms that must match varies per document and
	* is either controlled by a minimum should match field or computed per document in a minimum should match script.
	*
	* Be sure to read the Elasticsearch documentation on {ref_current}/query-dsl-terms-set-query.html[Terms Set query] for more information.
	*
	* [float]
	*=== Minimum should match with field
	*
	* The field that controls the number of required terms that must match must be a number field
	*/
	public class TermsSetQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermsSetQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			terms_set = new
			{
				branches = new
				{
					_name = "named_query",
					boost = 1.1,
					terms = new [] { "master", "dev" },
					minimum_should_match_field = "requiredBranches"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new TermsSetQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.Branches),
			Terms = new [] { "master", "dev" },
			MinimumShouldMatchField = Infer.Field<Project>(p => p.RequiredBranches)
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.TermsSet(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Branches)
				.Terms("master", "dev")
				.MinimumShouldMatchField(p => p.RequiredBranches)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITermsSetQuery>(a => a.TermsSet)
		{
			q => q.Field = null,
			q => q.Terms = null,
			q => q.Terms = Enumerable.Empty<object>(),
			q => q.Terms = new [] { "" },
			q =>
			{
				q.MinimumShouldMatchField = null;
				q.MinimumShouldMatchScript = null;
			}
		};
	}

	/**[float]
	*=== Minimum should match with script
	*
	* Scripts can also be used to control how many terms are required to match in a more dynamic way.
	*
	* The `params.num_terms` parameter is available in the script to indicate the number of
	* terms that have been specified in the query.
	*/
	public class TermsSetScriptQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermsSetScriptQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			terms_set = new
			{
				branches = new
				{
					_name = "named_query",
					boost = 1.1,
					terms = new [] { "master", "dev" },
					minimum_should_match_script = new
					{
						source = "Math.min(params.num_terms, doc['requiredBranches'].value)"
					}
				}
			}
		};

		protected override QueryContainer QueryInitializer => new TermsSetQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.Branches),
			Terms = new [] { "master", "dev" },
			MinimumShouldMatchScript = new InlineScript("Math.min(params.num_terms, doc['requiredBranches'].value)")
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.TermsSet(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Branches)
				.Terms("master", "dev")
				.MinimumShouldMatchScript(s => s
					.Source("Math.min(params.num_terms, doc['requiredBranches'].value)")
				)
			);
	}
}
