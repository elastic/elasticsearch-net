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
	public class TruncateTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/truncate-tokenfilter.asciidoc:24")]
		public void Line24()
		{
			// tag::ee2d97090d617ed8aa2a87ea33556dd7[]
			var response0 = new SearchResponse<object>();
			// end::ee2d97090d617ed8aa2a87ea33556dd7[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""whitespace"",
			  ""filter"" : [""truncate""],
			  ""text"" : ""the quinquennial extravaganza carried on""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/truncate-tokenfilter.asciidoc:93")]
		public void Line93()
		{
			// tag::f8651356ce2e7e93fa306c30f57ed588[]
			var response0 = new SearchResponse<object>();
			// end::f8651356ce2e7e93fa306c30f57ed588[]

			response0.MatchesExample(@"PUT custom_truncate_example
			{
			  ""settings"" : {
			    ""analysis"" : {
			      ""analyzer"" : {
			        ""standard_truncate"" : {
			        ""tokenizer"" : ""standard"",
			        ""filter"" : [""truncate""]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/truncate-tokenfilter.asciidoc:128")]
		public void Line128()
		{
			// tag::af84b3995564a7ca84360a526a4ac896[]
			var response0 = new SearchResponse<object>();
			// end::af84b3995564a7ca84360a526a4ac896[]

			response0.MatchesExample(@"PUT 5_char_words_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""lowercase_5_char"": {
			          ""tokenizer"": ""lowercase"",
			          ""filter"": [ ""5_char_trunc"" ]
			        }
			      },
			      ""filter"": {
			        ""5_char_trunc"": {
			          ""type"": ""truncate"",
			          ""length"": 5
			        }
			      }
			    }
			  }
			}");
		}
	}
}
