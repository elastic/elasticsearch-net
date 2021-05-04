// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

		public CompletionField Inferred { get; set; }

		[Completion]
		public CompletionField Minimal { get; set; }
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
