using System;
using Nest;

namespace Tests.Mapping.Types.Specialized.Completion
{
	public class CompletionTest
	{
		[Completion(
			SearchAnalyzer = "mysearchanalyzer",
			Analyzer = "myanalyzer",
			PreserveSeparators = true,
			PreservePositionIncrements = true,
			MaxInputLength = 20)]
		public CompletionField Full { get; set; }

		[Completion]
		public CompletionField Minimal { get; set; }

        public CompletionField Inferred { get; set; }
	}

	public class CompletionAttributeTests : AttributeTestsBase<CompletionTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "completion",
					search_analyzer = "mysearchanalyzer",
					analyzer = "myanalyzer",
					preserve_separators = true,
					preserve_position_increments = true,
					max_input_length = 20
				},
				minimal = new
				{
					type = "completion"
				},
                inferred = new
                {
                    type = "completion"
                }
            }
		};
	}
}
