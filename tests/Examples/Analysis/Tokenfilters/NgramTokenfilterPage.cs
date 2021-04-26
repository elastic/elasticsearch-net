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
	public class NgramTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/ngram-tokenfilter.asciidoc:30")]
		public void Line30()
		{
			// tag::f65abb38dd0cfedeb06e0cef206fbdab[]
			var response0 = new SearchResponse<object>();
			// end::f65abb38dd0cfedeb06e0cef206fbdab[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [ ""ngram"" ],
			  ""text"": ""Quick fox""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/ngram-tokenfilter.asciidoc:161")]
		public void Line161()
		{
			// tag::923aee95078219ee6eb321a252e1121b[]
			var response0 = new SearchResponse<object>();
			// end::923aee95078219ee6eb321a252e1121b[]

			response0.MatchesExample(@"PUT ngram_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""standard_ngram"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""ngram"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/ngram-tokenfilter.asciidoc:208")]
		public void Line208()
		{
			// tag::2793fa53b7d269852aa74f6bf57e34dc[]
			var response0 = new SearchResponse<object>();
			// end::2793fa53b7d269852aa74f6bf57e34dc[]

			response0.MatchesExample(@"PUT ngram_custom_example
			{
			  ""settings"": {
			    ""index"": {
			      ""max_ngram_diff"": 2
			    },
			    ""analysis"": {
			      ""analyzer"": {
			        ""default"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""3_5_grams"" ]
			        }
			      },
			      ""filter"": {
			        ""3_5_grams"": {
			          ""type"": ""ngram"",
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
