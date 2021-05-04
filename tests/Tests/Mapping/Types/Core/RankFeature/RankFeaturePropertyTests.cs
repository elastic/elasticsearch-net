// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Core.RankFeature
{
	public class RankFeaturePropertyTests : PropertyTestsBase
	{
		public RankFeaturePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "rank_feature",
					positive_score_impact = true
				}
			}
		};


		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.RankFeature(s => s
				.Name(p => p.Name)
				.PositiveScoreImpact()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new RankFeatureProperty
				{
					PositiveScoreImpact = true
				}
			}
		};
	}
}
