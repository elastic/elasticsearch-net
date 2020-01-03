using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Core.RankFeatures
{
	public class RankFeaturesPropertyTests : PropertyTestsBase
	{
		public RankFeaturesPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "rank_features"
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.RankFeatures(s => s
				.Name(p => p.Name)
			);

		protected override IProperties InitializerProperties => new Properties
		{
			{ "name", new RankFeaturesProperty() }
		};
	}
}
