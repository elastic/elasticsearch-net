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
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class RemoveDuplicatesTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/remove-duplicates-tokenfilter.asciidoc:24")]
		public void Line24()
		{
			// tag::15d948d593d2624ac5e2b155052048f0[]
			var response0 = new SearchResponse<object>();
			// end::15d948d593d2624ac5e2b155052048f0[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    ""keyword_repeat"",
			    ""stemmer""
			  ],
			  ""text"": ""jumping dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/remove-duplicates-tokenfilter.asciidoc:79")]
		public void Line79()
		{
			// tag::bab4c3b22c1768fcc7153345e4096dfb[]
			var response0 = new SearchResponse<object>();
			// end::bab4c3b22c1768fcc7153345e4096dfb[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    ""keyword_repeat"",
			    ""stemmer"",
			    ""remove_duplicates""
			  ],
			  ""text"": ""jumping dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/remove-duplicates-tokenfilter.asciidoc:136")]
		public void Line136()
		{
			// tag::198d39435b00b938cc2fa8f98c92e135[]
			var response0 = new SearchResponse<object>();
			// end::198d39435b00b938cc2fa8f98c92e135[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_custom_analyzer"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""keyword_repeat"",
			            ""stemmer"",
			            ""remove_duplicates""
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
