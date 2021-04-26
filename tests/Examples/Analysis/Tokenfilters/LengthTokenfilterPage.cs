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
	public class LengthTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/length-tokenfilter.asciidoc:27")]
		public void Line27()
		{
			// tag::1659420311d907d9fc024b96f4150216[]
			var response0 = new SearchResponse<object>();
			// end::1659420311d907d9fc024b96f4150216[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    {
			      ""type"": ""length"",
			      ""min"": 0,
			      ""max"": 4
			    }
			  ],
			  ""text"": ""the quick brown fox jumps over the lazy dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/length-tokenfilter.asciidoc:109")]
		public void Line109()
		{
			// tag::ea690283f301c6ce957efad93d7d5c5d[]
			var response0 = new SearchResponse<object>();
			// end::ea690283f301c6ce957efad93d7d5c5d[]

			response0.MatchesExample(@"PUT length_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""standard_length"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""length"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/length-tokenfilter.asciidoc:149")]
		public void Line149()
		{
			// tag::d88f883ed2fb8be35cd3e72ddffcf4ef[]
			var response0 = new SearchResponse<object>();
			// end::d88f883ed2fb8be35cd3e72ddffcf4ef[]

			response0.MatchesExample(@"PUT length_custom_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""whitespace_length_2_to_10_char"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""length_2_to_10_char"" ]
			        }
			      },
			      ""filter"": {
			        ""length_2_to_10_char"": {
			          ""type"": ""length"",
			          ""min"": 2,
			          ""max"": 10
			        }
			      }
			    }
			  }
			}");
		}
	}
}
