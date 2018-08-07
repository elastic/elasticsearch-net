using System;
using Nest;

namespace Tests.Mapping.Types.Specialized.TokenCount
{
	public class TokenCountTest
	{
		[TokenCount(
			Index = false,
			Analyzer = "standard",
			Boost = 1.2,
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
					index = false,
					boost = 1.2,
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
