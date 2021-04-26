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

namespace Tests.Mapping.Types.Core.SearchAsYouType
{
	public class SearchAsYouTypeTest
	{
		[SearchAsYouType(
			MaxShingleSize = 4,
			Analyzer = "myanalyzer",
			Index = true,
			IndexOptions = IndexOptions.Offsets,
			SearchAnalyzer = "mysearchanalyzer",
			SearchQuoteAnalyzer = "mysearchquoteanalyzer",
			Similarity = "classic",
			Store = true,
			Norms = false,
			TermVector = TermVectorOption.WithPositionsOffsets)]
		public string Full { get; set; }

		[SearchAsYouType(MaxShingleSize = 1)]
		public string MaxShingleSize { get; set; }

		[SearchAsYouType]
		public string Minimal { get; set; }
	}

	public class SearchAsYouTypeAttributeTests : AttributeTestsBase<SearchAsYouTypeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "search_as_you_type",
					max_shingle_size = 4,
					analyzer = "myanalyzer",
					index = true,
					index_options = "offsets",
					search_analyzer = "mysearchanalyzer",
					search_quote_analyzer = "mysearchquoteanalyzer",
					similarity = "classic",
					store = true,
					norms = false,
					term_vector = "with_positions_offsets"
				},
				maxShingleSize = new { type = "search_as_you_type", max_shingle_size = 1 },
				minimal = new { type = "search_as_you_type" }
			}
		};
	}
}
