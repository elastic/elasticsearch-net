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
	public class StemmerTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/stemmer-tokenfilter.asciidoc:23")]
		public void Line23()
		{
			// tag::a4e510aa9145ccedae151c4a6634f0a4[]
			var response0 = new SearchResponse<object>();
			// end::a4e510aa9145ccedae151c4a6634f0a4[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [ ""stemmer"" ],
			  ""text"": ""the foxes jumping quickly""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/stemmer-tokenfilter.asciidoc:85")]
		public void Line85()
		{
			// tag::7bdb87054d1c363964e109dec74197d6[]
			var response0 = new SearchResponse<object>();
			// end::7bdb87054d1c363964e109dec74197d6[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""stemmer"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/stemmer-tokenfilter.asciidoc:258")]
		public void Line258()
		{
			// tag::cf215381af3df5eaa8c0cf3a2d42337a[]
			var response0 = new SearchResponse<object>();
			// end::cf215381af3df5eaa8c0cf3a2d42337a[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""my_stemmer""
			          ]
			        }
			      },
			      ""filter"": {
			        ""my_stemmer"": {
			          ""type"": ""stemmer"",
			          ""language"": ""light_german""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
