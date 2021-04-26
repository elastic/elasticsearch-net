/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
