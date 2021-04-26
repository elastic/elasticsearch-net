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

namespace Tests.Mapping.Types.Core.Text
{
	public class TextTest
	{
		[Text(
			Analyzer = "myanalyzer",
			Boost = 1.2,
			EagerGlobalOrdinals = true,
			Fielddata = true,
			Index = true,
			IndexOptions = IndexOptions.Offsets,
			PositionIncrementGap = 5,
			SearchAnalyzer = "mysearchanalyzer",
			SearchQuoteAnalyzer = "mysearchquoteanalyzer",
			Similarity = "classic",
			Store = true,
			Norms = false,
			TermVector = TermVectorOption.WithPositionsOffsets)]
		public string Full { get; set; }

		public string Inferred { get; set; }

		[Text]
		public string Minimal { get; set; }
	}

	public class TextAttributeTests : AttributeTestsBase<TextTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "text",
					analyzer = "myanalyzer",
					boost = 1.2,
					eager_global_ordinals = true,
					fielddata = true,
					index = true,
					index_options = "offsets",
					position_increment_gap = 5,
					search_analyzer = "mysearchanalyzer",
					search_quote_analyzer = "mysearchquoteanalyzer",
					similarity = "classic",
					store = true,
					norms = false,
					term_vector = "with_positions_offsets"
				},
				minimal = new
				{
					type = "text"
				},
				inferred = new
				{
					type = "text",
					fields = new
					{
						keyword = new
						{
							type = "keyword",
							ignore_above = 256
						}
					}
				}
			}
		};
	}
}
