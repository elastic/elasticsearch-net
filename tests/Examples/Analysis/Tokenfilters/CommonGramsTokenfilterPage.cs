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
	public class CommonGramsTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/common-grams-tokenfilter.asciidoc:28")]
		public void Line28()
		{
			// tag::2fd0b3c132b46aa34cc9d92dd2d4bc85[]
			var response0 = new SearchResponse<object>();
			// end::2fd0b3c132b46aa34cc9d92dd2d4bc85[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""whitespace"",
			  ""filter"" : [
			    {
			      ""type"": ""common_grams"",
			      ""common_words"": [""is"", ""the""]
			    }
			  ],
			  ""text"" : ""the quick fox is brown""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/common-grams-tokenfilter.asciidoc:126")]
		public void Line126()
		{
			// tag::63de16d533d65708cf794eb50da02fbd[]
			var response0 = new SearchResponse<object>();
			// end::63de16d533d65708cf794eb50da02fbd[]

			response0.MatchesExample(@"PUT /common_grams_example
			{
			    ""settings"": {
			        ""analysis"": {
			            ""analyzer"": {
			              ""index_grams"": {
			                  ""tokenizer"": ""whitespace"",
			                  ""filter"": [""common_grams""]
			              }
			            },
			            ""filter"": {
			              ""common_grams"": {
			                  ""type"": ""common_grams"",
			                  ""common_words"": [""a"", ""is"", ""the""]
			              }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/common-grams-tokenfilter.asciidoc:206")]
		public void Line206()
		{
			// tag::d2d5a5fa4ab40787db87c85e1dd2bd06[]
			var response0 = new SearchResponse<object>();
			// end::d2d5a5fa4ab40787db87c85e1dd2bd06[]

			response0.MatchesExample(@"PUT /common_grams_example
			{
			    ""settings"": {
			        ""analysis"": {
			            ""analyzer"": {
			              ""index_grams"": {
			                  ""tokenizer"": ""whitespace"",
			                  ""filter"": [""common_grams_query""]
			              }
			            },
			            ""filter"": {
			              ""common_grams_query"": {
			                  ""type"": ""common_grams"",
			                  ""common_words"": [""a"", ""is"", ""the""],
			                  ""ignore_case"": true,
			                  ""query_mode"": true
			              }
			            }
			        }
			    }
			}");
		}
	}
}
