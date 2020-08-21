// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

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

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IParentIdQuery>(a => a.ParentId)
		{
			q => q.Id = null,
			q => q.Type = null,
		};

		protected override QueryContainer QueryInitializer => new ParentIdQuery
		{
			Name = "named_query",
			Type = Infer.Relation<CommitActivity>(),
			Id = Project.Instance.Name
		};

		protected override object QueryJson => new
		{
			parent_id = new
			{
				_name = "named_query",
				type = "commits",
				id = Project.Instance.Name
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.ParentId(p => p
				.Name("named_query")
				.Type<CommitActivity>()
				.Id(Project.Instance.Name)
			);
	}
}
