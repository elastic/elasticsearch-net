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
