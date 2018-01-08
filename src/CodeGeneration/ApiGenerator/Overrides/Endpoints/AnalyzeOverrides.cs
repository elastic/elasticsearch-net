using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class AnalyzeOverrides : EndpointOverridesBase
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
