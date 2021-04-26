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
	public class KstemTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/kstem-tokenfilter.asciidoc:29")]
		public void Line29()
		{
			// tag::68a891f609ca3a379d2d64e4914f3067[]
			var response0 = new SearchResponse<object>();
			// end::68a891f609ca3a379d2d64e4914f3067[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [ ""kstem"" ],
			  ""text"": ""the foxes jumping quickly""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/kstem-tokenfilter.asciidoc:98")]
		public void Line98()
		{
			// tag::e56ea22e3555d7c6de248e0327200b2e[]
			var response0 = new SearchResponse<object>();
			// end::e56ea22e3555d7c6de248e0327200b2e[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [
			            ""lowercase"",
			            ""kstem""
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}