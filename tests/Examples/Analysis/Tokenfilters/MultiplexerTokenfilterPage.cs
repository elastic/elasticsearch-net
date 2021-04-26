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
	public class MultiplexerTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/multiplexer-tokenfilter.asciidoc:36")]
		public void Line36()
		{
			// tag::c306212babadc14fa124b88fd8c43a6b[]
			var response0 = new SearchResponse<object>();
			// end::c306212babadc14fa124b88fd8c43a6b[]

			response0.MatchesExample(@"PUT /multiplexer_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [ ""my_multiplexer"" ]
			                }
			            },
			            ""filter"" : {
			                ""my_multiplexer"" : {
			                    ""type"" : ""multiplexer"",
			                    ""filters"" : [ ""lowercase"", ""lowercase, porter_stem"" ]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/multiplexer-tokenfilter.asciidoc:61")]
		public void Line61()
		{
			// tag::fa9a3ef94470f3d9bd6500b65bf993d1[]
			var response0 = new SearchResponse<object>();
			// end::fa9a3ef94470f3d9bd6500b65bf993d1[]

			response0.MatchesExample(@"POST /multiplexer_example/_analyze
			{
			  ""analyzer"" : ""my_analyzer"",
			  ""text"" : ""Going HOME""
			}");
		}
	}
}
