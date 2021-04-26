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

namespace Examples.HowTo.Recipes
{
	public class StemmingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("how-to/recipes/stemming.asciidoc:11")]
		public void Line11()
		{
			// tag::397bdb40d0146102f1f4c6a35675e16a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::397bdb40d0146102f1f4c6a35675e16a[]

			response0.MatchesExample(@"PUT index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""english_exact"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""lowercase""
			          ]
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""body"": {
			        ""type"": ""text"",
			        ""analyzer"": ""english"",
			        ""fields"": {
			          ""exact"": {
			            ""type"": ""text"",
			            ""analyzer"": ""english_exact""
			          }
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT index/_doc/1
			{
			  ""body"": ""Ski resort""
			}");

			response2.MatchesExample(@"PUT index/_doc/2
			{
			  ""body"": ""A pair of skis""
			}");

			response3.MatchesExample(@"POST index/_refresh");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/recipes/stemming.asciidoc:58")]
		public void Line58()
		{
			// tag::bf2e6ea2bae621b9b2fee7003e891f86[]
			var response0 = new SearchResponse<object>();
			// end::bf2e6ea2bae621b9b2fee7003e891f86[]

			response0.MatchesExample(@"GET index/_search
			{
			  ""query"": {
			    ""simple_query_string"": {
			      ""fields"": [ ""body"" ],
			      ""query"": ""ski""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/recipes/stemming.asciidoc:116")]
		public void Line116()
		{
			// tag::3f94ed945ae6416a0eb372c2db14d7e0[]
			var response0 = new SearchResponse<object>();
			// end::3f94ed945ae6416a0eb372c2db14d7e0[]

			response0.MatchesExample(@"GET index/_search
			{
			  ""query"": {
			    ""simple_query_string"": {
			      ""fields"": [ ""body.exact"" ],
			      ""query"": ""ski""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/recipes/stemming.asciidoc:173")]
		public void Line173()
		{
			// tag::26abfc49c238c2b5d259983ac38dbcee[]
			var response0 = new SearchResponse<object>();
			// end::26abfc49c238c2b5d259983ac38dbcee[]

			response0.MatchesExample(@"GET index/_search
			{
			  ""query"": {
			    ""simple_query_string"": {
			      ""fields"": [ ""body"" ],
			      ""quote_field_suffix"": "".exact"",
			      ""query"": ""\""ski\""""
			    }
			  }
			}");
		}
	}
}
