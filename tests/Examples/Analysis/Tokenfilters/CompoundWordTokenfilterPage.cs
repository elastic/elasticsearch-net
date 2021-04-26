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

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class CompoundWordTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line86()
		{
			// tag::349e77cfe54f857ccfdde0e47c2d7cd5[]
			var response0 = new SearchResponse<object>();
			// end::349e77cfe54f857ccfdde0e47c2d7cd5[]

			response0.MatchesExample(@"PUT /compound_word_example
			{
			    ""settings"": {
			        ""index"": {
			            ""analysis"": {
			                ""analyzer"": {
			                    ""my_analyzer"": {
			                        ""type"": ""custom"",
			                        ""tokenizer"": ""standard"",
			                        ""filter"": [""dictionary_decompounder"", ""hyphenation_decompounder""]
			                    }
			                },
			                ""filter"": {
			                    ""dictionary_decompounder"": {
			                        ""type"": ""dictionary_decompounder"",
			                        ""word_list"": [""one"", ""two"", ""three""]
			                    },
			                    ""hyphenation_decompounder"": {
			                        ""type"" : ""hyphenation_decompounder"",
			                        ""word_list_path"": ""analysis/example_word_list.txt"",
			                        ""hyphenation_patterns_path"": ""analysis/hyphenation_patterns.xml"",
			                        ""max_subword_size"": 22
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}
