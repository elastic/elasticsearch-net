using System;
using Nest;

namespace Tests.Mapping.Types.Specialized.TokenCount
{
	public class TokenCountTest
	{
		[TokenCount(
			Index = NonStringIndexOption.No,
			Boost = 1.2,
			NullValue = 0,
			IncludeInAll = true,
			PrecisionStep = 3,
			IgnoreMalformed = true,
			Coerce = true)]
		public int Full { get; set; }

		[TokenCount]
		public int Minimal { get; set; }
	}

	public class TokenCountMappingTests : TypeMappingTestBase<TokenCountTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "token_count",
					index = "no",
					boost = 1.2,
					null_value = 0.0,
					include_in_all = true,
					precision_step = 3,
					ignore_malformed = true,
					coerce = true
				},
				minimal = new
				{
					type = "token_count"
				}
			}
		};

		protected override Func<PropertiesDescriptor<TokenCountTest>, IPromise<IProperties>> FluentProperties => p => p
			.TokenCount(t => t
				.Name(o => o.Full)
				.Index(NonStringIndexOption.No)
				.Boost(1.2)
				.NullValue(0)
				.IncludeInAll()
				.PrecisionStep(3)
				.IgnoreMalformed()
				.Coerce()
			)
			.TokenCount(t => t
				.Name(o => o.Minimal)
			)
		;
	}
}
