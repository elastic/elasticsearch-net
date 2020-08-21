// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Specialized.MoreLikeThis
{
	public class MoreLikeThisFullDocumentQueryUsageTests : QueryDslUsageTestsBase
	{
		public MoreLikeThisFullDocumentQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override QueryContainer QueryInitializer => new MoreLikeThisQuery
		{
			Fields = Infer.Fields<Project>(
				f => f.Name,
				f => f.Description),
			Like = new List<Like>
			{
				new LikeDocument<Project>(Project.Instance) { Routing = Project.Instance.Name },
				"some long text"
			}
		};

		protected override object QueryJson => new
		{
			more_like_this = new
			{
				fields = new []
				{
					"name",
					"description"
				},
				like = new object[]
				{
					new
					{
						_index = "project",
						doc = Project.InstanceAnonymous,
						routing = Project.Instance.Name
					},
					"some long text"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MoreLikeThis(sn => sn
				.Fields(ff => ff
					.Field(f => f.Name)
					.Field(f => f.Description)
				)
				.Like(l => l
					.Document(d => d
						.Document(Project.Instance)
						.Routing(Project.Instance.Name)
					)
					.Text("some long text")
				)
			);
	}
}
