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
	public class LimitTokenCountTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/limit-token-count-tokenfilter.asciidoc:43")]
		public void Line43()
		{
			// tag::5a3855f1b3e37d89ab7cbcc4f7ae1dd3[]
			var response0 = new SearchResponse<object>();
			// end::5a3855f1b3e37d89ab7cbcc4f7ae1dd3[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""standard"",
			    ""filter"": [
			    {
			      ""type"": ""limit"",
			      ""max_token_count"": 2
			    }
			  ],
			  ""text"": ""quick fox jumps over lazy dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/limit-token-count-tokenfilter.asciidoc:96")]
		public void Line96()
		{
			// tag::b96f465abb658fe32889c3d183f159a3[]
			var response0 = new SearchResponse<object>();
			// end::b96f465abb658fe32889c3d183f159a3[]

			response0.MatchesExample(@"PUT limit_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""standard_one_token_limit"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""limit"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/limit-token-count-tokenfilter.asciidoc:123")]
		public void Line123()
		{
			// tag::63521e0089c631d6668c44a0a9d7fdcc[]
			var response0 = new SearchResponse<object>();
			// end::63521e0089c631d6668c44a0a9d7fdcc[]

			response0.MatchesExample(@"PUT custom_limit_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""whitespace_five_token_limit"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""five_token_limit"" ]
			        }
			      },
			      ""filter"": {
			        ""five_token_limit"": {
			          ""type"": ""limit"",
			          ""max_token_count"": 5
			        }
			      }
			    }
			  }
			}");
		}
	}
}
