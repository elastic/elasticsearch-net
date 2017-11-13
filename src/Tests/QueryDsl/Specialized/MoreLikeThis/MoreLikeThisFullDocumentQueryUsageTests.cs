using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.Specialized.MoreLikeThis
{
	public class MoreLikeThisFullDocumentQueryUsageTests : QueryDslUsageTestsBase
	{
		public MoreLikeThisFullDocumentQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			more_like_this = new
			{
				fields = new[] { "name" },
				like = new object[] {
					new {
						_index = "project",
						_type = "doc",
						_id = Project.Instance.Name,
						doc = Project.InstanceAnonymous
					},
					"some long text"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new MoreLikeThisQuery
		{
			Fields = Fields<Project>(p=>p.Name),
			Like = new List<Like>
			{
				new LikeDocument<Project>(Project.Instance),
				"some long text"
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MoreLikeThis(sn => sn
				.Fields(f=>f.Field(p=>p.Name))
				.Like(l=>l
					.Document(d=>d.Document(Project.Instance))
					.Text("some long text")
				)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IMoreLikeThisQuery>(a => a.MoreLikeThis)
		{
			q => q.Like = null,
			q => q.Like = Enumerable.Empty<Like>(),
			q => q.Fields = null,
		};
	}
}
