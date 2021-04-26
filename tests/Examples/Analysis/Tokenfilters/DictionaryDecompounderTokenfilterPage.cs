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
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class DictionaryDecompounderTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/dictionary-decompounder-tokenfilter.asciidoc:32")]
		public void Line32()
		{
			// tag::3fecd5c6d0c172566da4a54320e1cff3[]
			var response0 = new SearchResponse<object>();
			// end::3fecd5c6d0c172566da4a54320e1cff3[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [
			    {
			      ""type"": ""dictionary_decompounder"",
			      ""word_list"": [""Donau"", ""dampf"", ""meer"", ""schiff""]
			    }
			  ],
			  ""text"": ""Donaudampfschiff""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/dictionary-decompounder-tokenfilter.asciidoc:152")]
		public void Line152()
		{
			// tag::a5a5fb129de2f492e8fd33043a73439c[]
			var response0 = new SearchResponse<object>();
			// end::a5a5fb129de2f492e8fd33043a73439c[]

			response0.MatchesExample(@"PUT dictionary_decompound_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""standard_dictionary_decompound"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""22_char_dictionary_decompound"" ]
			        }
			      },
			      ""filter"": {
			        ""22_char_dictionary_decompound"": {
			          ""type"": ""dictionary_decompounder"",
			          ""word_list_path"": ""analysis/example_word_list.txt"",
			          ""max_subword_size"": 22
			        }
			      }
			    }
			  }
			}");
		}
	}
}
