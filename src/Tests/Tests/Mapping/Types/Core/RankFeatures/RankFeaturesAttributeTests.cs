using System.Collections.Generic;
using Nest;

namespace Tests.Mapping.Types.Core.RankFeatures
{
	public class RankFeaturesTest
	{
		[RankFeatures]
		public Dictionary<string, int> RankFeatures { get; set; }
	}

	public class RankFeaturesAttributeTests : AttributeTestsBase<RankFeaturesTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				rankFeatures = new
				{
					type = "rank_features"
				}
			}
		};
	}
}
