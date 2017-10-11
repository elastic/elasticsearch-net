using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.Mapping.Types.Core.Join
{
	public class JoinPropertyTests : PropertyTestsBase
	{
		public JoinPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "join",
					relations = new {
						project = "commits"
					}
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Join(pr => pr
				.Name(p => p.Name)
				.Relations(r => r.Join<Project, CommitActivity>())
			);


		protected override IProperties InitializerProperties => new Properties
		{

			{"name", new JoinProperty
			{
				Relations = new Relations
				{
					{Type<Project>(), Type<CommitActivity>()}
				}
			}}
		};
	}
	public class JoinPropertyComplexTests : PropertyTestsBase
	{
		public JoinPropertyComplexTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "join",
					relations = new {
						project = "commits",
						parent2 = new [] { "child2", "child3" }
					}
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Join(pr => pr
				.Name(p => p.Name)
				.Relations(r => r
					.Join<Project, CommitActivity>()
					.Join("parent2", "child2", "child3")
				)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{ "name", new JoinProperty
			{
				Relations = new Relations
				{
					{ Type<Project>(), Type<CommitActivity>() },
					{ "parent2", "child2", "child3" }
				}
			} }
		};
	}
}
