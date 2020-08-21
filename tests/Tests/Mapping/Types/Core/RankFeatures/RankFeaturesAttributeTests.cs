// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
