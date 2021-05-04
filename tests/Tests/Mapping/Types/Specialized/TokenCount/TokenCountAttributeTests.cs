// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Mapping.Types.Specialized.TokenCount
{
	public class TokenCountTest
	{
		[TokenCount(
			Index = false,
			Analyzer = "standard",
			EnablePositionIncrements = false,
			NullValue = 0)]
		public int Full { get; set; }

		[TokenCount]
		public int Minimal { get; set; }
	}

	public class TokenCountAttributeTests : AttributeTestsBase<TokenCountTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "token_count",
					analyzer = "standard",
					enable_position_increments = false,
					index = false,
					null_value = 0.0,
				},
				minimal = new
				{
					type = "token_count"
				}
			}
		};
	}
}
