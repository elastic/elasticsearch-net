using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class AnalyzeDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"analyzer",
			"char_filters",
			"char_filter",
			"field",
			"filters",
			"filter",
			"text",
			"tokenizer"
		};
	}
}
