// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Mapping.Types.Core.RankFeature
{
	public class RankFeatureTest
	{
		[RankFeature(PositiveScoreImpact = true)]
		public int RankFeature { get; set; }
	}

	public class RankFeatureAttributeTests : AttributeTestsBase<RankFeatureTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				rankFeature = new
				{
					type = "rank_feature",
					positive_score_impact = true
				}
			}
		};
	}
}
