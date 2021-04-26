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
	public class EdgengramTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/edgengram-tokenfilter.asciidoc:34")]
		public void Line34()
		{
			// tag::6dbfe5565a95508e65d304131847f9fc[]
			var response0 = new SearchResponse<object>();
			// end::6dbfe5565a95508e65d304131847f9fc[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [
			    { ""type"": ""edge_ngram"",
			      ""min_gram"": 1,
			      ""max_gram"": 2
			    }
			  ],
			  ""text"": ""the quick brown fox jumps""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/edgengram-tokenfilter.asciidoc:143")]
		public void Line143()
		{
			// tag::16351d99d0608789d04a0bb11a537098[]
			var response0 = new SearchResponse<object>();
			// end::16351d99d0608789d04a0bb11a537098[]

			response0.MatchesExample(@"PUT edge_ngram_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""standard_edge_ngram"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""edge_ngram"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/edgengram-tokenfilter.asciidoc:203")]
		public void Line203()
		{
			// tag::6f07152055e99416deb10e95b428b847[]
			var response0 = new SearchResponse<object>();
			// end::6f07152055e99416deb10e95b428b847[]

			response0.MatchesExample(@"PUT edge_ngram_custom_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""default"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""3_5_edgegrams"" ]
			        }
			      },
			      ""filter"": {
			        ""3_5_edgegrams"": {
			          ""type"": ""edge_ngram"",
			          ""min_gram"": 3,
			          ""max_gram"": 5
			        }
			      }
			    }
			  }
			}");
		}
	}
}
