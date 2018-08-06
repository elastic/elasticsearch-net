using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Joining.ParentId
{
	/**
	 * The `parent_id` query can be used to find child documents which belong to a particular parent.
	 *
	 * See the Elasticsearch documentation on {ref_current}/query-dsl-parent-id-query.html[parent_id query] for more details.
	 */
	public class ParentIdQueryUsageTests : QueryDslUsageTestsBase
	{
		public ParentIdQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			parent_id = new
			{
				_name = "named_query",
				type = "commits",
				id = Project.Instance.Name
			}
		};

		protected override QueryContainer QueryInitializer => new ParentIdQuery
		{
			Name = "named_query",
			Type = Infer.Relation<CommitActivity>(),
			Id = Project.Instance.Name
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.ParentId(p => p
				.Name("named_query")
				.Type<CommitActivity>()
				.Id(Project.Instance.Name)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IParentIdQuery>(a => a.ParentId)
		{
			q =>  q.Id = null,
			q =>  q.Type = null,
		};
	}
}
